using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Timers;
using System.Reflection;
using System.IO;
using System.Net;
using Edidev.FrameworkEDIx64;
using System.Collections;
using EDIWindowService.EDIFileTypeClasses;

namespace EDIWindowService
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer m_mainTimer;
        private bool m_timerTaskSuccess = true;

        //private Timer timer1 = null;
        //private bool IsJobComplted=false;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            try
            {
                // Create and start a timer.               
                m_mainTimer = new System.Timers.Timer();
                m_mainTimer.Interval = 30000;   // every 1/2 min
                m_mainTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.m_mainTimer_Elapsed);
                m_mainTimer.Start(); // Start
                m_mainTimer.Enabled = true;
            }
            catch (Exception ex)
            {
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
            }
        }

        void m_mainTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // do some work
                if (m_timerTaskSuccess == true)
                {
                    m_timerTaskSuccess = false;
                   System.Diagnostics.Debugger.Launch();
                    m_mainTimer.Enabled = false;
                    LogLibrary.WriteErrorLog("Test window service started");
                    //*******************************************************************************************
                    ProcessFile();
                }
            }
            catch (Exception ex)
            {
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
                m_mainTimer.Enabled = true;
                m_timerTaskSuccess = true;
                m_mainTimer.Start();

            }
            finally
            {
                if (m_timerTaskSuccess == false)
                {
                    m_mainTimer.Enabled = true;
                    m_timerTaskSuccess = true;
                    m_mainTimer.Start();
                }
            }
        }


        protected override void OnStop()
        {
            // timer1.Enabled = false;
            try
            {
                LogLibrary.WriteErrorLog("Test window service stopped");
                // Service stopped. Also stop the timer.
                m_mainTimer.Stop();
                m_mainTimer.Dispose();
                m_mainTimer = null;
            }
            catch (Exception ex)
            {
                // omitted
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
            }
        }

        private void InsertIntoDataBase()
        {
            try
            {
                string LocalConnection = System.Configuration.ConfigurationManager.ConnectionStrings["EdiDbConnectionString"].ConnectionString;
            }
            catch (Exception exception)
            {
                LogLibrary.WriteErrorLog("Error on during the Shed  " + exception);
            }
        }
        private void ProcessFile()
        {

            AcknowledgeInboundEDIFiles();// Step -1 Inbound file Process
            TranslateEdiFiles();// Step -2 Inbound file Process


        }
        protected void AcknowledgeInboundEDIFiles()
        {
            try
            {
                ediDocument oEdiDoc = null;
                ediDataSegment oSegment = null;
                ediAcknowledgment oAck = null;
                ediSchemas oSchemas = null;
                string sSegmentID;
                string sLoopSection;
                int nArea;
                string sPrevEdiFile = "";
                bool bInbound997True = false;
                //  string sPath = AppDomain.CurrentDomain.BaseDirectory;
                SafeAndEdiFilePath objSafeAndEdiFilePath = new SafeAndEdiFilePath();
                EdiFilesPaths EdiFilePath = objSafeAndEdiFilePath.GetFilesAndPaths();

                // string sPath = @"" + ConfigurationManager.AppSettings["AppPath"] + @"\";
                string sPath = EdiFilePath.sPath;
                SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EdiDb"].ConnectionString);

                string sInboundPath = EdiFilePath.sInboundPath;//sPath + ConfigurationManager.AppSettings["EDI_Inbound"] + @"\";
                string sOutboundPath = EdiFilePath.sOutboundPath;//sPath + ConfigurationManager.AppSettings["EDI_Outbound"] + @"\";
                string sAcceptPath = EdiFilePath.sAcceptPath;//sPath + ConfigurationManager.AppSettings["EDI_Accepted"] + @"\";
                string sRejectPath = EdiFilePath.sRejectPath;//sPath + ConfigurationManager.AppSettings["EDI_Rejected"] + @"\";
                string s997Path = EdiFilePath.s997Path;//sPath + ConfigurationManager.AppSettings["EDI_997"] + @"\";
                string sSefPath = EdiFilePath.sSefPath;//sPath + ConfigurationManager.AppSettings["Seffolder"] + @"\";
                string sEdiDonePath = EdiFilePath.sEdiDonePath;//sPath + ConfigurationManager.AppSettings["EDI_DONE"] + @"\";

                int nFileCount = 0;

                string[] sEdiPathFiles = Directory.GetFiles(sInboundPath);

                if (sEdiPathFiles.Length == 0)
                {
                    //MessageBox.Show("There are no files in the EDI_Inbound folder.");
                    LogLibrary.WriteErrorLog(" on AcknowledgeInboundEDIFiles" + "There are no files in the EDI_Inbound folder.");
                }
                else
                {
                    System.Windows.Forms.Cursor Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    oConnection.Open();

                    string sSql = "select * from [Config] ";
                    SqlDataAdapter oDaConfig = new SqlDataAdapter(sSql, oConnection);
                    DataSet oConfigDs = new DataSet("dsConfig");
                    oDaConfig.Fill(oConfigDs, "dsConfig");
                    DataRow oConfigRow = oConfigDs.Tables["dsConfig"].Rows[0];

                    string sSenderIdQlfr = oConfigRow["SenderIdQlfr"].ToString();
                    string sSenderId = oConfigRow["SenderId"].ToString();
                    string sReceiverIdQlfr = oConfigRow["ReceiverIdQlfr"].ToString();
                    string sReceiverId = oConfigRow["ReceiverId"].ToString();
                    string sControlNumber = oConfigRow["ControlNumber"].ToString();
                    string sAcknowledgmentRequested = oConfigRow["AcknowledgmentRequested"].ToString();
                    string sUsageIndicator = oConfigRow["UsageIndicator"].ToString();
                    string sComponentElementSeparator = oConfigRow["ComponentElementSeparator"].ToString();

                    Int32 nControlNumber = Convert.ToInt32(sControlNumber);

                    ediDocument.Set(ref oEdiDoc, new ediDocument());

                    //By default, FREDI uses the  universal coordinated time (UTC), however you can change it to local time
                    oEdiDoc.set_Option(DocumentOptionIDConstants.OptDocument_UseLocalTime, 1);

                    // Disabling the internal standard reference library to makes sure that 
                    // FREDI uses only the SEF file provided
                    ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                    oSchemas.EnableStandardReference = false;

                    // This makes certain that the EDI file must use the same version SEF file, otherwise the process will stop.
                    oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                    // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                    // improves performance when processing larger EDI files.
                    oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                    // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                    // property must be enabled before loading the EDI file.
                    oAck = (ediAcknowledgment)oEdiDoc.GetAcknowledgment();
                    oAck.EnableFunctionalAcknowledgment = true;

                    // Load all SEF files from SEF folder.  FREDI will automaticall select the appropriate one.
                    string[] sSefPathFiles = Directory.GetFiles(sSefPath);
                    foreach (string sSefPathFile in sSefPathFiles)
                    {
                        oEdiDoc.LoadSchema(sSefPathFile, 0);

                    } // foreach

                    string sDestTempFile = "dest_temp.txt";

                    using (Stream destStream = File.OpenWrite(sDestTempFile))
                    {
                        foreach (string sEdiPathFile in sEdiPathFiles)
                        {
                            nFileCount = nFileCount + 1;

                            string sEdiFile = Path.GetFileName(sEdiPathFile);
                            DateTime dtEdiFileCreation = File.GetCreationTime(sEdiPathFile);
                            string sEdiFileTranSetNo = "";
                            string sInbound997GroupContolNo = "";
                            string sInbound997TranSetNo = "";
                            string sInbound997Ack501 = "";
                            // Loads EDI file and the corresponding SEF file
                            oEdiDoc.LoadEdi(sEdiPathFile);
                            // This loop will generate a 997 EDI file if the inbound file is itslef not a 997
                            // Gets the first data segment in the EDI files
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment
                            // This loop iterates though the EDI file (850 or 997) a segment at a time
                            while (oSegment != null)
                            {
                                // A segment is identified by its Area number, Loop section and segment id.
                                sSegmentID = oSegment.ID;
                                sLoopSection = oSegment.LoopSection;
                                nArea = oSegment.Area;

                                if (nArea == 1)
                                {
                                    if (sLoopSection == "")
                                    {
                                        if (sSegmentID == "ST")
                                        {
                                            sEdiFileTranSetNo = oSegment.get_DataElementValue(1);

                                            if (sEdiFileTranSetNo == "997")
                                            {
                                                bInbound997True = true;
                                            }
                                            else
                                            {
                                                bInbound997True = false;
                                            }
                                        }
                                        else if (sSegmentID == "AK1")
                                        {
                                            sInbound997GroupContolNo = oSegment.get_DataElementValue(2);

                                        } // sSegmentID
                                    }
                                    else if (sLoopSection == "AK2")
                                    {
                                        if (sSegmentID == "AK2")
                                        {
                                            sInbound997TranSetNo = oSegment.get_DataElementValue(2);
                                        }
                                        else if (sSegmentID == "AK5")
                                        {
                                            sInbound997Ack501 = oSegment.get_DataElementValue(1);

                                        } // sSegmentID

                                    } // sLoopSection

                                } // nArea

                                //get next data segment
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());  //oSegment = (ediDataSegment) oSegment.Next();
                            }

                            // This reads the 997 object that was generated in the above loop to tell us 
                            // if the EDI file received should be accpeted or rejected.  (This does not translate an inbound 997 EDI file.)
                            if (!bInbound997True) // if not an inbound 997 file
                            {
                                // Checks the 997 acknowledgment file just created.
                                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                                // to translating any other EDI file.

                                // Gets the first segment of the 997 acknowledgment file
                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());    //oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();

                                bool bFileAccepted = true;

                                while (oSegment != null)
                                {
                                    nArea = oSegment.Area;
                                    sLoopSection = oSegment.LoopSection;
                                    sSegmentID = oSegment.ID;

                                    if (nArea == 1)
                                    {
                                        if (sLoopSection == "")
                                        {
                                            if (sSegmentID == "AK9")
                                            {
                                                if (oSegment.get_DataElementValue(1, 0) == "R")
                                                {
                                                    bFileAccepted = false;
                                                }
                                            }
                                        }   // sLoopSection == ""
                                    }   //nArea == 1

                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());  //oSegment = (ediDataSegment) oSegment.Next();
                                }   //oSegment != null

                                if (bFileAccepted)
                                {
                                    // All accepted EDI files are sent to the ACCEPTED folder and in their corresponding subfolders.
                                    File.Copy(sInboundPath + sEdiFile, sAcceptPath + "\\" + sEdiFileTranSetNo + "\\" + sEdiFileTranSetNo + "_" + dtEdiFileCreation.ToString("yyyyMMddHHmmss") + "_" + sEdiFile, true);

                                }
                                else
                                {
                                    File.Copy(sInboundPath + sEdiFile, sRejectPath + sEdiFileTranSetNo + "_" + dtEdiFileCreation.ToString("yyyyMMddHHmmss") + "_" + sEdiFile, true);
                                    InsertRejectedFile(sEdiFile, sRejectPath, "EDI_Rejected", sEdiFileTranSetNo, dtEdiFileCreation.ToString("yyyyMMddHHmmss"));
                                }

                                // Combine all EDI files.  This file will be read to create one 997 acknowledgment file for all incoming EDI files. 
                                using (Stream ediStream = File.OpenRead(sInboundPath + sEdiFile))
                                {
                                    ediStream.CopyTo(destStream);
                                }

                            } // if !bInbound997True 
                            else
                            {

                                File.Copy(sInboundPath + sEdiFile, s997Path + sEdiFileTranSetNo + "_" + dtEdiFileCreation.ToString("yyyyMMddHHmmss") + "_" + sEdiFile, true);

                            } // if bInbound997True 

                            if (File.Exists(sInboundPath + sPrevEdiFile))
                            {
                                File.Delete(sInboundPath + sPrevEdiFile);
                            }

                            sPrevEdiFile = sEdiFile;

                        } // foreach sEdiPathFile

                    } // using

                    // dispose old ack
                    oAck.Dispose();

                    // instantiate new ack for combined 997
                    oAck = (ediAcknowledgment)oEdiDoc.GetAcknowledgment();
                    oAck.EnableFunctionalAcknowledgment = true;

                    string sIsaControlNoBuff = "000000000" + sControlNumber.Trim();
                    string sIsaControlNo = sIsaControlNoBuff.Substring(sIsaControlNoBuff.Length - 9);

                    // Set the starting point of the control numbers in the acknowledgment
                    oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, sIsaControlNo);
                    oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, sControlNumber);
                    oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                    oAck.set_Option(AcknowledgmentOptionIDConstants.OptAcknowledgment_ReportToSingleInterchange, 1);
                    oAck.set_Option(AcknowledgmentOptionIDConstants.OptAcknowledgment_ShowReportingLevel, 0);

                    //create combined acknowledgment
                    oEdiDoc.LoadEdi(sDestTempFile);

                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc.FirstDataSegment);

                    while (oSegment != null)
                    {
                        ediDataSegment.Set(ref oSegment, oSegment.Next());

                    }
                    oAck.Save(sOutboundPath + "997_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".X12");

                    //increment Interchange control number
                    sControlNumber = (Convert.ToInt32(sControlNumber) + 1).ToString();
                    sSql = @"UPDATE [Config] SET ControlNumber = @ControlNumber";
                    oDaConfig.UpdateCommand = new SqlCommand(sSql, oConnection);
                    oDaConfig.UpdateCommand.Parameters.AddWithValue("@ControlNumber", sControlNumber);
                    oDaConfig.UpdateCommand.ExecuteNonQuery();

                    oConnection.Close();

                    //Close oEdiDoc to release EDI file
                    oEdiDoc.Close();

                    File.Delete(sDestTempFile);

                    Cursor = System.Windows.Forms.Cursors.Default;

                    if (File.Exists(sInboundPath + sPrevEdiFile))
                    {
                        File.Delete(sInboundPath + sPrevEdiFile);
                    }
                    //tblMessage.Style.Add("display", "block");
                    //lblMessage.Text = "Done. " + nFileCount.ToString() + " file(s) received.";
                } // if sEdiPathFiles.Length
            }
            catch (Exception ex)
            {
                throw ex;
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
            }
        }

        private void InsertRejectedFile(string fileNames, string FilePath, string StatusCode
                                      , string EdiFileTranSetNo, string CreatedDateTime)
        {
            try
            {
                using (SqlConnection oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EdiDb"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPO_insertEDIFilesDetails", oConnection);
                    oConnection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fileNames", fileNames);
                    cmd.Parameters.AddWithValue("@FilePath", FilePath);
                    cmd.Parameters.AddWithValue("@StatusCode", StatusCode);
                    cmd.Parameters.AddWithValue("@EdiFileTranSetNo", EdiFileTranSetNo);
                    cmd.Parameters.AddWithValue("@CreatedDateTime", CreatedDateTime);
                    cmd.ExecuteScalar();
                    //tblMessage.Style.Add("display", "block");
                    //lblMessage.Text = fileNames + " file  has been rejected.";
                }
            }
            catch (Exception ex)
            {
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
                throw ex;
            }
        }
        private List<FileStatus> GetFileStatus(int FileTypeStatusID)
        {
            DataTable dt = new DataTable();
            List<FileStatus> objFileStatus = new List<FileStatus>();
            try
            {
                SqlHelper objSqlHelper = new SqlHelper();
                Hashtable parms = new Hashtable();
                parms.Add("@FileTypeStatusID", FileTypeStatusID);
                DataSet ds = new DataSet();                
                ds=objSqlHelper.ExecuteProcudere("SPO_GetFileStatus", parms);
                dt = ds.Tables[0];
                if(dt.Rows.Count>0)
                {
                    objFileStatus = dt.AsEnumerable()
                                     .Select(x =>
                                     new FileStatus
                                     {
                                         ID = x.Field<int>("ID"),
                                         HeaderKey = x.Field<int>("HeaderKey"),
                                         docType = x.Field<string>("docType"),
                                         senderID = x.Field<string>("senderID"),
                                         recieverID = x.Field<string>("recieverID"),
                                         statusID = x.Field<int>("statusID"),
                                         ProcessDateTime = x.Field<DateTime>("ProcessDateTime"),
                                         isExportImport = x.Field<int>("ProcessDateTime"),
                                     }
                                     ).ToList();
                }

            }
            catch (Exception ex)
            {
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
                throw ex;
            }
            return objFileStatus;
        }
        protected void OutboundProcess()
        {
            DataTable dt=GetFileStatus(2);
            if(dt.)
            if(dt.)
        }

        protected void TranslateEdiFiles()
        {
            SafeAndEdiFilePath objSafeAndEdiFilePath = new SafeAndEdiFilePath();
            EdiFilesPaths EdiFilePath = objSafeAndEdiFilePath.GetFilesAndPaths();
            string[] DirectoryList = Directory.GetDirectories(EdiFilePath.sAcceptPath);
            Translator objTranslator = new Translator();
            foreach (var item in DirectoryList)
            {
                DirectoryInfo dir_info = new DirectoryInfo(item);
                switch (dir_info.Name)
                {
                    case "850":
                        objTranslator.TranslateEdi850File(EdiFilePath.sPath, EdiFilePath.sSefPath, EdiFilePath.sAcceptPath + @"\850\", EdiFilePath.sEdiDonePath);
                        break;
                    case "852":
                        objTranslator.TranslateEdi852File(EdiFilePath.sPath, EdiFilePath.sSefPath, EdiFilePath.sAcceptPath + @"\852\", EdiFilePath.sEdiDonePath);
                        break;
                    case "860":
                        objTranslator.TranslateEdi860File(EdiFilePath.sPath, EdiFilePath.sSefPath, EdiFilePath.sAcceptPath + @"\860\", EdiFilePath.sEdiDonePath);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
