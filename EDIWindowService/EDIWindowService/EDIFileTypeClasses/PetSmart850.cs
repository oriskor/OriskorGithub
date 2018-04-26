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
    class PetSmart850
    {
        ediDocument oEdiDoc = null;
        ediDataSegment oSegment = null;
        ediSchemas oSchemas = null;

        SqlConnection oConnection;
        SqlDataAdapter oDaInterchange;
        SqlDataAdapter oDaFuncGroup;
        SqlDataAdapter oDaHeader;
        SqlDataAdapter oDaDetail;

        Int32 nInterkey;
        Int32 nGroupkey;
        Int32 nHeaderkey;
        Int32 nDetailkey;

        // Constructor takes SEF file name as argument.  Creates ediDocument object.
        public PetSmart850(string sSefPathFile)
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

        }


        public void Translate(string sEdiPathFile)
        {
            string sSegmentID;
            string sLoopSection;
            int nArea;
            string sQlfr = "";
            string sSql = "";
            string sN1Qlfr = "";

            nInterkey = 0;
            nGroupkey = 0;
            nHeaderkey = 0;
            nDetailkey = 0;

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
                } //nArea == 0
                else if (nArea == 1)
                {
                    if (sLoopSection == "")
                    {
                        if (sSegmentID == "ST")
                        {
                            sSql = @"INSERT INTO [850_Header] ( FunctionalGroupKey, ST01_TranSetIdfrCode, ST02_TranSetControlNo, 
                                BEG01_TransactionSetPurposeCode, BEG02_PurchaseOrderTypeCode, BEG03_PurchaseOrderNumber, BEG05_PODate, 
                                CUR02_CurrencyCode, REF02_FreeFormText, REF02_InternalVendorNo, REF02_ProductGroup,
                                PER02_ContactPersonName, DTM02_DeliveryRequestedDate, DTM02_RequestedPickupDate, 
                                N104_ShipFromID, N401_ShipFromCity, N402_ShipFromState, N403_ShipFromPostalCode, N404_ShipFromCountryCode,
                                N104_ShipToID, N401_ShipToCity, N402_ShipToState, N403_ShipToPostalCode, N404_ShipToCountryCode,
                                CTT01_NumberOfPO1Segments) 
                                values 
                                (@FunctionalGroupKey, @ST01_TranSetIdfrCode, @ST02_TranSetControlNo, 
                                @BEG01_TransactionSetPurposeCode, @BEG02_PurchaseOrderTypeCode, @BEG03_PurchaseOrderNumber, @BEG05_PODate, 
                                @CUR02_CurrencyCode, @REF02_FreeFormText, @REF02_InternalVendorNo, @REF02_ProductGroup,
                                @PER02_ContactPersonName, @DTM02_DeliveryRequestedDate, @DTM02_RequestedPickupDate, 
                                @N104_ShipFromID, @N401_ShipFromCity, @N402_ShipFromState, @N403_ShipFromPostalCode, @N404_ShipFromCountryCode,
                                @N104_ShipToID, @N401_ShipToCity, @N402_ShipToState, @N403_ShipToPostalCode, @N404_ShipToCountryCode,
                                @CTT01_NumberOfPO1Segments);
                                SELECT scope_identity()";

                            oDaHeader.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@FunctionalGroupKey", nGroupkey);
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@ST01_TranSetIdfrCode", oSegment.get_DataElementValue(1, 0));
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@ST02_TranSetControlNo", oSegment.get_DataElementValue(2, 0));
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BEG01_TransactionSetPurposeCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BEG02_PurchaseOrderTypeCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BEG03_PurchaseOrderNumber", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BEG05_PODate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@CUR02_CurrencyCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@REF02_FreeFormText", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@REF02_InternalVendorNo", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@REF02_ProductGroup", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@PER02_ContactPersonName", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@DTM02_DeliveryRequestedDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@DTM02_RequestedPickupDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N104_ShipFromID", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N401_ShipFromCity", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N402_ShipFromState", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N403_ShipFromPostalCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N404_ShipFromCountryCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N104_ShipToID", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N401_ShipToCity", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N402_ShipToState", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N403_ShipToPostalCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N404_ShipToCountryCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@CTT01_NumberOfPO1Segments", "");
                            nHeaderkey = (Int32)(decimal)oDaHeader.InsertCommand.ExecuteScalar();
                        }
                        else if (sSegmentID == "BEG")
                        {
                            sSql = @"UPDATE [850_Header] SET BEG01_TransactionSetPurposeCode = @BEG01_TransactionSetPurposeCode, BEG02_PurchaseOrderTypeCode = @BEG02_PurchaseOrderTypeCode,
                            BEG03_PurchaseOrderNumber = @BEG03_PurchaseOrderNumber, BEG05_PODate = @BEG05_PODate
                            where Headerkey = @Headerkey";
                            oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BEG01_TransactionSetPurposeCode", oSegment.get_DataElementValue(1, 0));
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BEG02_PurchaseOrderTypeCode", oSegment.get_DataElementValue(2, 0));
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BEG03_PurchaseOrderNumber", oSegment.get_DataElementValue(3, 0));
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BEG05_PODate", oSegment.get_DataElementValue(5, 0));
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                            oDaHeader.UpdateCommand.ExecuteNonQuery();
                        }
                        else if (sSegmentID == "CUR")
                        {
                            sSql = @"UPDATE [850_Header] SET CUR02_CurrencyCode = @CUR02_CurrencyCode 
                            where Headerkey = @Headerkey";
                            oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@CUR02_CurrencyCode", oSegment.get_DataElementValue(2, 0));
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                            oDaHeader.UpdateCommand.ExecuteNonQuery();
                        }
                        else if (sSegmentID == "REF")
                        {
                            sQlfr = oSegment.get_DataElementValue(1, 0);

                            if (sQlfr == "FI")
                            {
                                sSql = @"UPDATE [850_Header] SET REF02_FreeFormText = @REF02_FreeFormText
                                where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@REF02_FreeFormText", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }
                            else if (sQlfr == "IA")
                            {
                                sSql = @"UPDATE [850_Header] SET REF02_InternalVendorNo = @REF02_InternalVendorNo
                                where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@REF02_InternalVendorNo", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }
                            else if (sQlfr == "PG")
                            {
                                sSql = @"UPDATE [850_Header] SET REF02_ProductGroup = @REF02_ProductGroup
                                where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@REF02_ProductGroup", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }

                        }
                        else if (sSegmentID == "PER")
                        {
                            sSql = @"UPDATE [850_Header] SET PER02_ContactPersonName = @PER02_ContactPersonName 
                            where Headerkey = @Headerkey";
                            oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@PER02_ContactPersonName", oSegment.get_DataElementValue(2, 0));
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                            oDaHeader.UpdateCommand.ExecuteNonQuery();
                        }
                        else if (sSegmentID == "DTM")
                        {
                            sQlfr = oSegment.get_DataElementValue(1, 0);

                            if (sQlfr == "002")
                            {
                                sSql = @"UPDATE [850_Header] SET DTM02_DeliveryRequestedDate = @DTM02_DeliveryRequestedDate
                                where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@DTM02_DeliveryRequestedDate", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }
                            else if (sQlfr == "118")
                            {
                                sSql = @"UPDATE [850_Header] SET DTM02_RequestedPickupDate = @DTM02_RequestedPickupDate
                                where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@DTM02_RequestedPickupDate", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }
                        }
                    } //sLoopSection == ""

                    else if (sLoopSection == "N1")
                    {
                        //Get Qualifier
                        if (sSegmentID == "N1")
                        {
                            sN1Qlfr = oSegment.get_DataElementValue(1, 0);
                        }

                        if (sN1Qlfr == "SF") //Ship From information
                        {
                            if (sSegmentID == "N1")
                            {
                                sSql = @"UPDATE [850_Header] SET N104_ShipFromID = @N104_ShipFromID
                                        where  Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N104_ShipFromID", oSegment.get_DataElementValue(4, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }
                            else if (sSegmentID == "N4")
                            {
                                sSql = @"UPDATE [850_Header] SET N401_ShipFromCity = @N401_ShipFromCity, N402_ShipFromState = @N402_ShipFromState, 
                                        N403_ShipFromPostalCode = @N403_ShipFromPostalCode, N404_ShipFromCountryCode = @N404_ShipFromCountryCode
                                        where  Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N401_ShipFromCity", oSegment.get_DataElementValue(1, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N402_ShipFromState", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N403_ShipFromPostalCode", oSegment.get_DataElementValue(3, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N404_ShipFromCountryCode", oSegment.get_DataElementValue(4, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }

                        }
                        else if (sN1Qlfr == "ST") //Ship To information
                        {
                            if (sSegmentID == "N1")
                            {
                                sSql = @"UPDATE [850_Header] SET N104_ShipToID = @N104_ShipToID
                                        where  Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N104_ShipToID", oSegment.get_DataElementValue(4, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }
                            else if (sSegmentID == "N4")
                            {
                                sSql = @"UPDATE [850_Header] SET N401_ShipToCity = @N401_ShipToCity, N402_ShipToState = @N402_ShipToState, 
                                        N403_ShipToPostalCode = @N403_ShipToPostalCode, N404_ShipToCountryCode = @N404_ShipToCountryCode
                                        where  Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N401_ShipToCity", oSegment.get_DataElementValue(1, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N402_ShipToState", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N403_ShipToPostalCode", oSegment.get_DataElementValue(3, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N404_ShipToCountryCode", oSegment.get_DataElementValue(4, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }

                        } // sN1Qlfr 

                    }  // sLoopSection

                }
                else if (nArea == 2)
                {
                    if (sLoopSection == "PO1")
                    {
                        if (sSegmentID == "PO1")
                        {
                            sSql = @"INSERT INTO [850_Detail] ( HeaderKey, PO102_Quantity, PO103_UnitMeasurementCode, 
                                PO104_UnitPrice, PO107_ArticleNo, PO108_GTIN_Qlfr, PO109_GTIN_DigitDatatructure, PO111_VendorItemNo, PO113_Case,
                                SDQ01_UnitMeasurementCode, SDQ03_IdCode, SDQ04_Quantity, SDQ05_IdCode, SDQ06_Quantity, SDQ07_IdCode, SDQ08_Quantity, SDQ09_IdCode, SDQ10_Quantity,
                                SDQ11_IdCode, SDQ12_Quantity, SDQ13_IdCode, SDQ14_Quantity, SDQ15_IdCode, SDQ16_Quantity, SDQ17_IdCode, SDQ18_Quantity,
                                SDQ19_IdCode, SDQ20_Quantity, SDQ21_IdCode, SDQ22_Quantity)
                                values
                                (@HeaderKey, @PO102_Quantity, @PO103_UnitMeasurementCode, 
                                @PO104_UnitPrice, @PO107_ArticleNo, @PO108_GTIN_Qlfr, @PO109_GTIN_DigitDatatructure, @PO111_VendorItemNo, @PO113_Case,
                                @SDQ01_UnitMeasurementCode, @SDQ03_IdCode, @SDQ04_Quantity, @SDQ05_IdCode, @SDQ06_Quantity, @SDQ07_IdCode, @SDQ08_Quantity, @SDQ09_IdCode, @SDQ10_Quantity,
                                @SDQ11_IdCode, @SDQ12_Quantity, @SDQ13_IdCode, @SDQ14_Quantity, @SDQ15_IdCode, @SDQ16_Quantity, @SDQ17_IdCode, @SDQ18_Quantity,
                                @SDQ19_IdCode, @SDQ20_Quantity, @SDQ21_IdCode, @SDQ22_Quantity);
                                SELECT scope_identity()";

                            oDaDetail.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@HeaderKey", nHeaderkey);
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO102_Quantity", oSegment.get_DataElementValue(2, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO103_UnitMeasurementCode", oSegment.get_DataElementValue(3, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO104_UnitPrice", oSegment.get_DataElementValue(4, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO107_ArticleNo", oSegment.get_DataElementValue(7, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO108_GTIN_Qlfr", oSegment.get_DataElementValue(8, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO109_GTIN_DigitDatatructure", oSegment.get_DataElementValue(9, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO111_VendorItemNo", oSegment.get_DataElementValue(11, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO113_Case", oSegment.get_DataElementValue(13, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ01_UnitMeasurementCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ03_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ04_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ05_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ06_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ07_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ08_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ09_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ10_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ11_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ12_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ13_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ14_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ15_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ16_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ17_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ18_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ19_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ20_Quantity", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ21_IdCode", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@SDQ22_Quantity", "");
                            nDetailkey = (Int32)(decimal)oDaDetail.InsertCommand.ExecuteScalar();

                        }
                        else if (sSegmentID == "SDQ")
                        {
                            sSql = @"UPDATE [850_Detail] SET SDQ01_UnitMeasurementCode = @SDQ01_UnitMeasurementCode, SDQ03_IdCode = @SDQ03_IdCode, 
                                        SDQ04_Quantity = @SDQ04_Quantity, SDQ05_IdCode = @SDQ05_IdCode, SDQ06_Quantity = @SDQ06_Quantity, 
                                        SDQ07_IdCode = @SDQ07_IdCode, SDQ08_Quantity = @SDQ08_Quantity, SDQ09_IdCode = @SDQ09_IdCode, SDQ10_Quantity = @SDQ10_Quantity, 
                                        SDQ11_IdCode = @SDQ11_IdCode, SDQ12_Quantity = @SDQ12_Quantity, SDQ13_IdCode = @SDQ13_IdCode, SDQ14_Quantity = @SDQ14_Quantity, 
                                        SDQ15_IdCode = @SDQ15_IdCode, SDQ16_Quantity = @SDQ16_Quantity, SDQ17_IdCode = @SDQ17_IdCode, SDQ18_Quantity = @SDQ18_Quantity, 
                                        SDQ19_IdCode = @SDQ19_IdCode, SDQ20_Quantity = @SDQ20_Quantity, SDQ21_IdCode = @SDQ21_IdCode, SDQ22_Quantity = @SDQ22_Quantity
                                        where  DetailKey = @DetailKey";
                            oDaDetail.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ01_UnitMeasurementCode", oSegment.get_DataElementValue(1, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ03_IdCode", oSegment.get_DataElementValue(3, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ04_Quantity", oSegment.get_DataElementValue(4, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ05_IdCode", oSegment.get_DataElementValue(5, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ06_Quantity", oSegment.get_DataElementValue(6, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ07_IdCode", oSegment.get_DataElementValue(7, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ08_Quantity", oSegment.get_DataElementValue(8, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ09_IdCode", oSegment.get_DataElementValue(9, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ10_Quantity", oSegment.get_DataElementValue(10, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ11_IdCode", oSegment.get_DataElementValue(11, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ12_Quantity", oSegment.get_DataElementValue(12, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ13_IdCode", oSegment.get_DataElementValue(13, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ14_Quantity", oSegment.get_DataElementValue(14, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ15_IdCode", oSegment.get_DataElementValue(15, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ16_Quantity", oSegment.get_DataElementValue(16, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ17_IdCode", oSegment.get_DataElementValue(17, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ18_Quantity", oSegment.get_DataElementValue(18, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ19_IdCode", oSegment.get_DataElementValue(19, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ20_Quantity", oSegment.get_DataElementValue(20, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ21_IdCode", oSegment.get_DataElementValue(21, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@SDQ22_Quantity", oSegment.get_DataElementValue(22, 0));
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@DetailKey", nDetailkey);
                            oDaDetail.UpdateCommand.ExecuteNonQuery();

                        } // sSegmentID

                    } // sLoopSection

                }
                else if (nArea == 3)
                {
                    if (sSegmentID == "CTT")
                    {
                        sSql = @"UPDATE [850_Header] SET CTT01_NumberOfPO1Segments = @CTT01_NumberOfPO1Segments 
                            where Headerkey = @Headerkey";
                        oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                        oDaHeader.UpdateCommand.Parameters.AddWithValue("@CTT01_NumberOfPO1Segments", oSegment.get_DataElementValue(1, 0));
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
