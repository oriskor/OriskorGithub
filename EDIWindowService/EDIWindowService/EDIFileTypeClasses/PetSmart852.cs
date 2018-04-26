using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edidev.FrameworkEDIx64;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace EDIWindowService
{
    class PetSmart852
    {
        ediDocument oEdiDoc = null;
        ediDataSegment oSegment = null;
        ediSchemas oSchemas = null;

        SqlConnection oConnection;
        SqlDataAdapter oDaInterchange;
        SqlDataAdapter oDaFuncGroup;
        SqlDataAdapter oDaHeader;
        SqlDataAdapter oDaDetail;
        SqlDataAdapter oProdActivity;

        Int32 nInterkey;
        Int32 nGroupkey;
        Int32 nHeaderkey;
        Int32 nDetailkey;
        Int32 nProdActivityKey;

        // Constructor takes SEF file name as argument.  Creates ediDocument object.
        public PetSmart852(string sSefPathFile)
        {
            // Create the top-level application object "ediDocument". 
            oEdiDoc = new ediDocument();

            // This makes certain that Framework EDI only uses the SEF file provided, and that it does not use its
            // built -in Standard Reference table to translate the EDI document 
            oSchemas = (ediSchemas)oEdiDoc.GetSchemas();
            oSchemas.EnableStandardReference = false;

            // The FORWARD-ONLY cursor increases the performance of processing the EDI document
            oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

            // Load SEF file 
            oEdiDoc.LoadSchema(sSefPathFile, SchemaTypeIDConstants.Schema_Standard_Exchange_Format);

            oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EdiDb"].ConnectionString);
            oConnection.Open();

            oDaInterchange = new SqlDataAdapter();
            oDaFuncGroup = new SqlDataAdapter();
            oDaHeader = new SqlDataAdapter();
            oDaDetail = new SqlDataAdapter();
            oProdActivity = new SqlDataAdapter();

        }



        public void Translate(string sEdiPathFile)
        {
            string sSegmentID;
            string sLoopSection;
            int nArea;
            string sSql = "";

            nInterkey = 0;
            nGroupkey = 0;
            nHeaderkey = 0;
            nDetailkey = 0;
            nProdActivityKey = 0;

            // Specify EDI document to read. 
            oEdiDoc.LoadEdi(sEdiPathFile);

            // Gets the first data segment in the EDI document. 
            ediDataSegment.Set(ref oSegment, oEdiDoc.FirstDataSegment);

            // Loop that will traverse through the EDI document from top to bottom. This 
            // is required for FORWARD-ONLY cursor. 
            while (oSegment != null)
            {
                sSegmentID = oSegment.ID;
                sLoopSection = oSegment.LoopSection;
                nArea = oSegment.Area;

                if (nArea == 0)
                {
                    if (sLoopSection == "")
                    {
                        if (sSegmentID == "ISA")
                        {
                            //insert a record into interchange table
                            sSql = @"INSERT INTO [InterchangeInbound] ( ISA01_AuthorizationInfoQlfr, ISA02_AuthorizationInfo, ISA03_SecurityInfoQlfr, 
                            ISA04_SecurityInfo, ISA05_SenderIdQlfr, ISA06_SenderId, ISA07_ReceiverIdQlfr, ISA08_ReceiverId, ISA09_Date, ISA10_Time, 
                            ISA11_RepetitionSeparator, ISA12_ControlVersionNumber, ISA13_ControlNumber, ISA14_AcknowledgmentRequested, ISA15_UsageIndicator, 
                            ISA16_ComponentElementSeparator) 
                            values 
                            (@ISA01_AuthorizationInfoQlfr, @ISA02_AuthorizationInfo, @ISA03_SecurityInfoQlfr, 
                            @ISA04_SecurityInfo, @ISA05_SenderIdQlfr, @ISA06_SenderId, @ISA07_ReceiverIdQlfr, @ISA08_ReceiverId, @ISA09_Date, @ISA10_Time, 
                            @ISA11_RepetitionSeparator, @ISA12_ControlVersionNumber, @ISA13_ControlNumber, @ISA14_AcknowledgmentRequested, @ISA15_UsageIndicator, 
                            @ISA16_ComponentElementSeparator);
                            SELECT scope_identity()";

                            oDaInterchange.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA01_AuthorizationInfoQlfr", oSegment.get_DataElementValue(1, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA02_AuthorizationInfo", oSegment.get_DataElementValue(2, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA03_SecurityInfoQlfr", oSegment.get_DataElementValue(3, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA04_SecurityInfo", oSegment.get_DataElementValue(4, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA05_SenderIdQlfr", oSegment.get_DataElementValue(5, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA06_SenderId", oSegment.get_DataElementValue(6, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA07_ReceiverIdQlfr", oSegment.get_DataElementValue(7, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA08_ReceiverId", oSegment.get_DataElementValue(8, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA09_Date", oSegment.get_DataElementValue(9, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA10_Time", oSegment.get_DataElementValue(10, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA11_RepetitionSeparator", oSegment.get_DataElementValue(11, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA12_ControlVersionNumber", oSegment.get_DataElementValue(12, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA13_ControlNumber", oSegment.get_DataElementValue(13, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA14_AcknowledgmentRequested", oSegment.get_DataElementValue(14, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA15_UsageIndicator", oSegment.get_DataElementValue(15, 0));
                            oDaInterchange.InsertCommand.Parameters.AddWithValue("@ISA16_ComponentElementSeparator", oSegment.get_DataElementValue(16, 0));
                            nInterkey = (Int32)(decimal)oDaInterchange.InsertCommand.ExecuteScalar();

                        }
                        else if (sSegmentID == "GS")
                        {
                            //insert a record into FunctionGrop table
                            sSql = @"INSERT INTO [FunctionalGroupInbound] (InterchangeKey, GS01_FunctionalIdfrCode, GS02_SendersCode, GS03_ReceiversCode, GS04_Date, GS05_Time, 
                            GS06_GroupControlNumber, GS07_ResponsibleAgencyCode, GS08_VersionReleaseCode)
                            values 
                            (@InterchangeKey, @GS01_FunctionalIdfrCode, @GS02_SendersCode, @GS03_ReceiversCode, @GS04_Date, @GS05_Time, 
                            @GS06_GroupControlNumber, @GS07_ResponsibleAgencyCode, @GS08_VersionReleaseCode);
                            SELECT scope_identity()";

                            oDaFuncGroup.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@InterchangeKey", nInterkey);
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS01_FunctionalIdfrCode", oSegment.get_DataElementValue(1, 0));     //Functional Identifier Code
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS02_SendersCode", oSegment.get_DataElementValue(2, 0));     //Application Sender's Code
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS03_ReceiversCode", oSegment.get_DataElementValue(3, 0));     //Application Receiver's Code
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS04_Date", oSegment.get_DataElementValue(4, 0));     //Date
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS05_Time", oSegment.get_DataElementValue(5, 0));     //Time
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS06_GroupControlNumber", oSegment.get_DataElementValue(6, 0));     //Group Control Number
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS07_ResponsibleAgencyCode", oSegment.get_DataElementValue(7, 0));     //Responsible Agency Code
                            oDaFuncGroup.InsertCommand.Parameters.AddWithValue("@GS08_VersionReleaseCode", oSegment.get_DataElementValue(8, 0));     //Version / Release / Industry Identifier Code
                            nGroupkey = (Int32)(decimal)oDaFuncGroup.InsertCommand.ExecuteScalar();
                        }

                    }
                }
                else if (nArea == 1)
                {
                    if (sLoopSection == "")
                    {
                        if (sSegmentID == "ST")
                        {
                            sSql = @"INSERT INTO [852_Header] ( FunctionalGroupKey, ST01_TranSetIdfrCode, ST02_TranSetControlNo, 
                                XQ02_ReportingDate, XQ03_ReportingEndDate, 
                                N902_InternalVendorNo, N903_FreeFormDesc,
                                CTT01_NumberOfLineItems) 
                                values 
                                (@FunctionalGroupKey, @ST01_TranSetIdfrCode, @ST02_TranSetControlNo, 
                                @XQ02_ReportingDate, @XQ03_ReportingEndDate, 
                                @N902_InternalVendorNo, @N903_FreeFormDesc,
                                @CTT01_NumberOfLineItems);
                                SELECT scope_identity()";

                            oDaHeader.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@FunctionalGroupKey", nGroupkey);
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@ST01_TranSetIdfrCode", oSegment.get_DataElementValue(1, 0));
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@ST02_TranSetControlNo", oSegment.get_DataElementValue(2, 0));
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@XQ02_ReportingDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@XQ03_ReportingEndDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N902_InternalVendorNo", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N903_FreeFormDesc", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@CTT01_NumberOfLineItems", "");
                            nHeaderkey = (Int32)(decimal)oDaHeader.InsertCommand.ExecuteScalar();
                        }
                        else if (sSegmentID == "XQ")
                        {
                            sSql = @"UPDATE [852_Header] SET XQ02_ReportingDate = @XQ02_ReportingDate, XQ03_ReportingEndDate = @XQ03_ReportingEndDate
                            where Headerkey = @Headerkey";
                            oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@XQ02_ReportingDate", oSegment.get_DataElementValue(2, 0));        // Date (373) 
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@XQ03_ReportingEndDate", oSegment.get_DataElementValue(3, 0));     // Date (373) 
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                            oDaHeader.UpdateCommand.ExecuteNonQuery();
                        }
                        else if (sSegmentID == "N9")
                        {
                            sSql = @"UPDATE [852_Header] SET N902_InternalVendorNo = @N902_InternalVendorNo, N903_FreeFormDesc = @N903_FreeFormDesc
                            where Headerkey = @Headerkey";
                            oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@N902_InternalVendorNo", oSegment.get_DataElementValue(2, 0));        // Reference Identification (127) 
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@N903_FreeFormDesc", oSegment.get_DataElementValue(3, 0));         // Free-form Description (369) 
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                            oDaHeader.UpdateCommand.ExecuteNonQuery();
                        }

                    } //sLoopSection == ""
                }
                else if (nArea == 2)
                {
                    if (sLoopSection == "LIN")
                    {
                        if (sSegmentID == "LIN")
                        {
                            sSql = @"INSERT INTO [852_Detail] ( HeaderKey, LIN03_GTIN_UCC_Code, LIN05_BuyerItemNo, LIN07_VendorItemNo,
                                CTP02_PriceCode, CTP03_UnitPrice, 
                                PO406_WeightPerPack, PO407_Unit )
                                values
                                (@HeaderKey, @LIN03_GTIN_UCC_Code, @LIN05_BuyerItemNo, @LIN07_VendorItemNo,
                                @CTP02_PriceCode, @CTP03_UnitPrice, 
                                @PO406_WeightPerPack, @PO407_Unit );
                                SELECT scope_identity()";

                            oDaDetail.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@HeaderKey", nHeaderkey);
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@LIN03_GTIN_UCC_Code", oSegment.get_DataElementValue(3, 0));   // Product/Service ID (234) 
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@LIN05_BuyerItemNo", oSegment.get_DataElementValue(5, 0));     // Product/Service ID (234) 
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@LIN07_VendorItemNo", oSegment.get_DataElementValue(7, 0));    // Product/Service ID (234) 
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@CTP02_PriceCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@CTP03_UnitPrice", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO406_WeightPerPack", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO407_Unit", "");
                            nDetailkey = (Int32)(decimal)oDaDetail.InsertCommand.ExecuteScalar();

                        }
                        else if (sSegmentID == "CTP")
                        {
                            sSql = @"UPDATE [852_Detail] SET CTP02_PriceCode = @CTP02_PriceCode, CTP03_UnitPrice = @CTP03_UnitPrice
                                        where  DetailKey = @DetailKey";
                            oDaDetail.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@CTP02_PriceCode", oSegment.get_DataElementValue(2, 0));   // Price Identifier Code (236) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@CTP03_UnitPrice", oSegment.get_DataElementValue(3, 0));   // Unit Price (212) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@DetailKey", nDetailkey);
                            oDaDetail.UpdateCommand.ExecuteNonQuery();

                        }
                        else if (sSegmentID == "PO4")       // Item Physical Details 
                        {
                            sSql = @"UPDATE [852_Detail] SET PO406_WeightPerPack = @PO406_WeightPerPack, PO407_Unit = @PO407_Unit
                                        where  DetailKey = @DetailKey";
                            oDaDetail.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@PO406_WeightPerPack", oSegment.get_DataElementValue(6, 0));   // Gross Weight per Pack (384) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@PO407_Unit", oSegment.get_DataElementValue(7, 0));            // Unit or Basis for Measurement Code (355) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@DetailKey", nDetailkey);
                            oDaDetail.UpdateCommand.ExecuteNonQuery();

                        } // sSegmentID

                    }
                    else if (sLoopSection == "LIN;ZA")
                    {
                        if (sSegmentID == "ZA")
                        {
                            sSql = @"INSERT INTO [852_ProdActivity] ( DetailKey, ZA01_ActivityCode, SDQ01_Unit, SDQ02_IdCodeQlfr,
                                SDQ03_IdCode, SDQ04_Quantity, SDQ05_IdCode, SDQ06_Quantity, SDQ07_IdCode, SDQ08_Quantity,
                                SDQ09_IdCode, SDQ10_Quantity, SDQ11_IdCode, SDQ12_Quantity, SDQ13_IdCode, SDQ14_Quantity,
                                SDQ15_IdCode, SDQ16_Quantity, SDQ17_IdCode, SDQ18_Quantity, SDQ19_IdCode, SDQ20_Quantity,
                                SDQ21_IdCode, SDQ22_Quantity )
                                values
                                (@DetailKey, @ZA01_ActivityCode, @SDQ01_Unit, @SDQ02_IdCodeQlfr,
                                @SDQ03_IdCode, @SDQ04_Quantity, @SDQ05_IdCode, @SDQ06_Quantity, @SDQ07_IdCode, @SDQ08_Quantity,
                                @SDQ09_IdCode, @SDQ10_Quantity, @SDQ11_IdCode, @SDQ12_Quantity, @SDQ13_IdCode, @SDQ14_Quantity,
                                @SDQ15_IdCode, @SDQ16_Quantity, @SDQ17_IdCode, @SDQ18_Quantity, @SDQ19_IdCode, @SDQ20_Quantity,
                                @SDQ21_IdCode, @SDQ22_Quantity );
                                SELECT scope_identity()";

                            oProdActivity.InsertCommand = new SqlCommand(sSql, oConnection);
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@DetailKey", nDetailkey);
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@ZA01_ActivityCode", oSegment.get_DataElementValue(1, 0));   // Activity Code (859)
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ01_Unit", "");                                         // Unit or Basis for Measurement Code (355) 
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ02_IdCodeQlfr", "");                                   // Identification Code Qualifier (66) 
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ03_IdCode", "");                                       // Identification Code (67) 
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ04_Quantity", "");                                     // Quantity (380) 
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ05_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ06_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ07_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ08_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ09_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ10_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ11_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ12_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ13_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ14_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ15_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ16_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ17_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ18_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ19_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ20_Quantity", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ21_IdCode", "");
                            oProdActivity.InsertCommand.Parameters.AddWithValue("@SDQ22_Quantity", "");
                            nProdActivityKey = (Int32)(decimal)oProdActivity.InsertCommand.ExecuteScalar();
                        }
                        else if (sSegmentID == "SDQ")
                        {
                            sSql = @"UPDATE [852_ProdActivity] SET SDQ01_Unit = @SDQ01_Unit, SDQ02_IdCodeQlfr = @SDQ02_IdCodeQlfr,
                                        SDQ03_IdCode = @SDQ03_IdCode, SDQ04_Quantity = @SDQ04_Quantity, SDQ05_IdCode = @SDQ05_IdCode, SDQ06_Quantity = @SDQ06_Quantity,
                                        SDQ07_IdCode = @SDQ07_IdCode, SDQ08_Quantity = @SDQ08_Quantity, SDQ09_IdCode = @SDQ09_IdCode, SDQ10_Quantity = @SDQ10_Quantity,
                                        SDQ11_IdCode = @SDQ11_IdCode, SDQ12_Quantity = @SDQ12_Quantity, SDQ13_IdCode = @SDQ13_IdCode, SDQ14_Quantity = @SDQ14_Quantity,
                                        SDQ15_IdCode = @SDQ15_IdCode, SDQ16_Quantity = @SDQ16_Quantity, SDQ17_IdCode = @SDQ17_IdCode, SDQ18_Quantity = @SDQ18_Quantity,
                                        SDQ19_IdCode = @SDQ19_IdCode, SDQ20_Quantity = @SDQ20_Quantity, SDQ21_IdCode = @SDQ21_IdCode, SDQ22_Quantity = @SDQ22_Quantity
                                        where ProdActivityKey = @ProdActivityKey";

                            oProdActivity.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ01_Unit", oSegment.get_DataElementValue(1, 0));            // Unit or Basis for Measurement Code (355)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ02_IdCodeQlfr", oSegment.get_DataElementValue(2, 0));      // Identification Code Qualifier (66)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ03_IdCode", oSegment.get_DataElementValue(3, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ04_Quantity", oSegment.get_DataElementValue(4, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ05_IdCode", oSegment.get_DataElementValue(5, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ06_Quantity", oSegment.get_DataElementValue(6, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ07_IdCode", oSegment.get_DataElementValue(7, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ08_Quantity", oSegment.get_DataElementValue(8, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ09_IdCode", oSegment.get_DataElementValue(9, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ10_Quantity", oSegment.get_DataElementValue(10, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ11_IdCode", oSegment.get_DataElementValue(11, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ12_Quantity", oSegment.get_DataElementValue(12, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ13_IdCode", oSegment.get_DataElementValue(13, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ14_Quantity", oSegment.get_DataElementValue(14, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ15_IdCode", oSegment.get_DataElementValue(15, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ16_Quantity", oSegment.get_DataElementValue(16, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ17_IdCode", oSegment.get_DataElementValue(17, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ18_Quantity", oSegment.get_DataElementValue(18, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ19_IdCode", oSegment.get_DataElementValue(19, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ20_Quantity", oSegment.get_DataElementValue(20, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ21_IdCode", oSegment.get_DataElementValue(21, 0));          // Identification Code (67)
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@SDQ22_Quantity", oSegment.get_DataElementValue(22, 0));        // Quantity (380) 
                            oProdActivity.UpdateCommand.Parameters.AddWithValue("@ProdActivityKey", nProdActivityKey);
                            oProdActivity.UpdateCommand.ExecuteNonQuery();
                        }

                    } // sLoopSection

                }
                else if (nArea == 3)
                {
                    if (sSegmentID == "CTT")
                    {
                        sSql = @"UPDATE [852_Header] SET CTT01_NumberOfLineItems = @CTT01_NumberOfLineItems 
                            where Headerkey = @Headerkey";
                        oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                        oDaHeader.UpdateCommand.Parameters.AddWithValue("@CTT01_NumberOfLineItems", oSegment.get_DataElementValue(1, 0));       // Number of Line Items (354) 
                        oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                        oDaHeader.UpdateCommand.ExecuteNonQuery();
                    }

                }

                ediDataSegment.Set(ref oSegment, oSegment.Next());

            } // oSegment != null

        } // Translate



        public void Close()
        {
            if (oSegment != null)
                oSegment.Dispose();

            if (oSchemas != null)
                oSchemas.Dispose();

            if (oEdiDoc != null)
                oEdiDoc.Dispose();

        }
    }
}
