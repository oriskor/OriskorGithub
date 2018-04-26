using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDIWindowService
{
    public class Translator
    {

        public void TranslateEdi850File(string sPath, string sSefPath, string sAcceptPath, string sEdiDonePath)
        {
            try
            {

                string sPrevEdiFile = "";
                int nFileCount = 0;

                string[] sEdiPathFiles = Directory.GetFiles(sAcceptPath);

                if (sEdiPathFiles.Length == 0)
                {
                    //  MessageBox.Show("There are no files in the EDI_Accepted folder.");
                    LogLibrary.WriteErrorLog("in TranslateEdiFiles :" + "There are no files in the EDI_Accepted folder.");
                }
                else
                {
                    System.Windows.Forms.Cursor Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    PetSmart850 oPetSmart850 = new PetSmart850(sSefPath + "PetSmart_850_006010.EVAL30.SEF");

                    foreach (string sEdiPathFile in sEdiPathFiles)
                    {
                        oPetSmart850.Translate(sEdiPathFile);

                        string sEdiFile = Path.GetFileName(sEdiPathFile);

                        File.Copy(sAcceptPath + sEdiFile, sEdiDonePath + sEdiFile, true);

                        if (File.Exists(sEdiDonePath + sPrevEdiFile))
                        {
                            File.Delete(sAcceptPath + sPrevEdiFile);
                        }

                        sPrevEdiFile = sEdiFile;

                        nFileCount = nFileCount + 1;

                    } // foreach

                    oPetSmart850.Close();

                    if (File.Exists(sEdiDonePath + sPrevEdiFile))
                    {
                        File.Delete(sAcceptPath + sPrevEdiFile);
                    }
                    //tblMessage.Style.Add("display", "block");
                    // lblMessage.Text = "Done. " + nFileCount.ToString() + " file(s) translated.";
                    //  MessageBox.Show("Done. " + nFileCount.ToString() + " file(s) translated.");
                    Cursor = System.Windows.Forms.Cursors.Default;

                } // if sAcceptPath.Length
            }
            catch (Exception ex)
            {
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
                throw ex;

            }
        }

        public void TranslateEdi852File(string sPath, string sSefPath, string sAcceptPath, string sEdiDonePath)
        {
            try
            {
                string sPrevEdiFile = "";
                //string sPath = AppDomain.CurrentDomain.BaseDirectory;

                //string sSefPath = sPath + ConfigurationManager.AppSettings["Seffolder"] + @"\";
                //string sAcceptPath = sPath + ConfigurationManager.AppSettings["EDI_Accepted"] + @"\852\";
                //string sEdiDonePath = sPath + ConfigurationManager.AppSettings["EDI_DONE"] + @"\";

                int nFileCount = 0;

                string[] sEdiPathFiles = Directory.GetFiles(sAcceptPath);

                if (sEdiPathFiles.Length == 0)
                {
                    //MessageBox.Show("There are no files in the EDI_Accepted folder.");
                }
                else
                {
                    System.Windows.Forms.Cursor Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    PetSmart852 oPetSmart852 = new PetSmart852(sSefPath + "PetSmart_852_006010.EVAL30.SEF");

                    foreach (string sEdiPathFile in sEdiPathFiles)
                    {
                        oPetSmart852.Translate(sEdiPathFile);

                        string sEdiFile = Path.GetFileName(sEdiPathFile);

                        File.Copy(sAcceptPath + sEdiFile, sEdiDonePath + sEdiFile, true);

                        if (File.Exists(sEdiDonePath + sPrevEdiFile))
                        {
                            File.Delete(sAcceptPath + sPrevEdiFile);
                        }

                        sPrevEdiFile = sEdiFile;

                        nFileCount = nFileCount + 1;

                    } // foreach

                    oPetSmart852.Close();

                    if (File.Exists(sEdiDonePath + sPrevEdiFile))
                    {
                        File.Delete(sAcceptPath + sPrevEdiFile);
                    }

                    //MessageBox.Show("Done. " + nFileCount.ToString() + " file(s) translated.");

                    Cursor = System.Windows.Forms.Cursors.Default;

                }
            }
            catch (Exception ex)
            {
                LogLibrary.WriteErrorLog("Error = " + ex.Message);
                throw ex;
            }
        }

        public void TranslateEdi860File(string sPath, string sSefPath, string sAcceptPath, string sEdiDonePath)
        {
            try
            {
                string sPrevEdiFile = "";
                //string sPath = AppDomain.CurrentDomain.BaseDirectory;
                //string sSefPath = sPath + ConfigurationManager.AppSettings["Seffolder"] + @"\";
                //string sAcceptPath = sPath + ConfigurationManager.AppSettings["EDI_Accepted"] + @"\860\";
                //string sEdiDonePath = sPath + ConfigurationManager.AppSettings["EDI_DONE"] + @"\";

                int nFileCount = 0;

                string[] sEdiPathFiles = Directory.GetFiles(sAcceptPath);

                if (sEdiPathFiles.Length == 0)
                {
                    //MessageBox.Show("There are no files in the EDI_Accepted folder.");
                }
                else
                {
                    System.Windows.Forms.Cursor Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    PetSmart860 oPetSmart860 = new PetSmart860(sSefPath + "PetSmart_860_006010.EVAL30.SEF");

                    foreach (string sEdiPathFile in sEdiPathFiles)
                    {
                        oPetSmart860.Translate(sEdiPathFile);

                        string sEdiFile = Path.GetFileName(sEdiPathFile);

                        File.Copy(sAcceptPath + sEdiFile, sEdiDonePath + sEdiFile, true);

                        if (File.Exists(sEdiDonePath + sPrevEdiFile))
                        {
                            File.Delete(sAcceptPath + sPrevEdiFile);
                        }

                        sPrevEdiFile = sEdiFile;

                        nFileCount = nFileCount + 1;

                    } // foreach

                    oPetSmart860.Close();

                    if (File.Exists(sEdiDonePath + sPrevEdiFile))
                    {
                        File.Delete(sAcceptPath + sPrevEdiFile);
                    }

                    //MessageBox.Show("Done. " + nFileCount.ToString() + " file(s) translated.");

                    Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}

