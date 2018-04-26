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
    class PetSmart860
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
        public PetSmart860(string sSefPathFile)
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
                            sSql = @"INSERT INTO [860_Header] ( FunctionalGroupKey, ST01_TranSetIdfrCode, ST02_TranSetControlNo, 
                                BCH01_TransactionSetPurposeCode, BCH02_PurchaseOrderTypeCode, BCH03_OriginalPO_No, BCH06_OriginalPO_Date, BCH11_OriginalPO_DateChange,
                                DTM02_DeliveryRequestedDate, DTM02_ShipNotBeforeDate, DTM02_ShipNoLaterDate, DTM02_RequestedPickupDate,
                                N104_ShipFromID, 
                                N401_ShipFromCity, N402_ShipFromState, N403_ShipFromZip, N404_ShipFromCountryCode,
                                CTT01_NumberOfPOCSegments ) 
                                values 
                                (@FunctionalGroupKey, @ST01_TranSetIdfrCode, @ST02_TranSetControlNo, 
                                @BCH01_TransactionSetPurposeCode, @BCH02_PurchaseOrderTypeCode, @BCH03_OriginalPO_No, @BCH06_OriginalPO_Date, @BCH11_OriginalPO_DateChange,
                                @DTM02_DeliveryRequestedDate, @DTM02_ShipNotBeforeDate, @DTM02_ShipNoLaterDate, @DTM02_RequestedPickupDate,
                                @N104_ShipFromID, 
                                @N401_ShipFromCity, @N402_ShipFromState, @N403_ShipFromZip, @N404_ShipFromCountryCode,
                                @CTT01_NumberOfPOCSegments );
                                SELECT scope_identity()";

                            oDaHeader.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@FunctionalGroupKey", nGroupkey);
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@ST01_TranSetIdfrCode", oSegment.get_DataElementValue(1, 0));
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@ST02_TranSetControlNo", oSegment.get_DataElementValue(2, 0));
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BCH01_TransactionSetPurposeCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BCH02_PurchaseOrderTypeCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BCH03_OriginalPO_No", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BCH06_OriginalPO_Date", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@BCH11_OriginalPO_DateChange", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@DTM02_DeliveryRequestedDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@DTM02_ShipNotBeforeDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@DTM02_ShipNoLaterDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@DTM02_RequestedPickupDate", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N104_ShipFromID", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N401_ShipFromCity", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N402_ShipFromState", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N403_ShipFromZip", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@N404_ShipFromCountryCode", "");
                            oDaHeader.InsertCommand.Parameters.AddWithValue("@CTT01_NumberOfPOCSegments", "");
                            nHeaderkey = (Int32)(decimal)oDaHeader.InsertCommand.ExecuteScalar();
                        }
                        else if (sSegmentID == "BCH")
                        {
                            sSql = @"UPDATE [860_Header] SET BCH01_TransactionSetPurposeCode = @BCH01_TransactionSetPurposeCode, BCH02_PurchaseOrderTypeCode = @BCH02_PurchaseOrderTypeCode,
                            BCH03_OriginalPO_No = @BCH03_OriginalPO_No, BCH06_OriginalPO_Date = @BCH06_OriginalPO_Date, BCH11_OriginalPO_DateChange = @BCH11_OriginalPO_DateChange
                            where Headerkey = @Headerkey";
                            oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BCH01_TransactionSetPurposeCode", oSegment.get_DataElementValue(1, 0));   // Transaction Set Purpose Code (353) 
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BCH02_PurchaseOrderTypeCode", oSegment.get_DataElementValue(2, 0));       // Purchase Order Type Code (92)  
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BCH03_OriginalPO_No", oSegment.get_DataElementValue(3, 0));               // Purchase Order Number (324) 
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BCH06_OriginalPO_Date", oSegment.get_DataElementValue(6, 0));             // Date (373)     
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@BCH11_OriginalPO_DateChange", oSegment.get_DataElementValue(11, 0));             // Date (373)     
                            oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                            oDaHeader.UpdateCommand.ExecuteNonQuery();
                        }
                        else if (sSegmentID == "DTM")
                        {
                            sQlfr = oSegment.get_DataElementValue(1, 0); 		// Date/Time Qualifier (374) 

                            if (sQlfr == "002") // Delivery Requested
                            {
                                sSql = @"UPDATE [860_Header] SET DTM02_DeliveryRequestedDate = @DTM02_DeliveryRequestedDate 
                                        where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@DTM02_DeliveryRequestedDate", oSegment.get_DataElementValue(2, 0));   // Date (373) 
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();

                            }
                            else if (sQlfr == "037") // Ship Not Before
                            {
                                sSql = @"UPDATE [860_Header] SET DTM02_ShipNotBeforeDate = @DTM02_ShipNotBeforeDate 
                                        where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@DTM02_ShipNotBeforeDate", oSegment.get_DataElementValue(2, 0));   // Date (373) 
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();

                            }
                            else if (sQlfr == "038") // Ship No Later
                            {
                                sSql = @"UPDATE [860_Header] SET DTM02_ShipNoLaterDate = @DTM02_ShipNoLaterDate 
                                        where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@DTM02_ShipNoLaterDate", oSegment.get_DataElementValue(2, 0));   // Date (373) 
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();

                            }
                            else if (sQlfr == "118") // Requested Pickup
                            {
                                sSql = @"UPDATE [860_Header] SET DTM02_RequestedPickupDate = @DTM02_RequestedPickupDate 
                                        where Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@DTM02_RequestedPickupDate", oSegment.get_DataElementValue(2, 0));   // Date (373) 
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();

                            }
                        }
                    }
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
                                sSql = @"UPDATE [860_Header] SET N104_ShipFromID = @N104_ShipFromID
                                        where  Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N104_ShipFromID", oSegment.get_DataElementValue(4, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }
                            else if (sSegmentID == "N4")
                            {
                                sSql = @"UPDATE [860_Header] SET N401_ShipFromCity = @N401_ShipFromCity, N402_ShipFromState = @N402_ShipFromState, 
                                        N403_ShipFromZip = @N403_ShipFromZip, N404_ShipFromCountryCode = @N404_ShipFromCountryCode
                                        where  Headerkey = @Headerkey";
                                oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N401_ShipFromCity", oSegment.get_DataElementValue(1, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N402_ShipFromState", oSegment.get_DataElementValue(2, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N403_ShipFromZip", oSegment.get_DataElementValue(3, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@N404_ShipFromCountryCode", oSegment.get_DataElementValue(4, 0));
                                oDaHeader.UpdateCommand.Parameters.AddWithValue("@Headerkey", nHeaderkey);
                                oDaHeader.UpdateCommand.ExecuteNonQuery();
                            }

                        } // sN1Qlfr 

                    }  // sLoopSection

                }
                else if (nArea == 2)
                {
                    if (sLoopSection == "POC")
                    {
                        if (sSegmentID == "POC")
                        {
                            sSql = @"INSERT INTO [860_Detail] ( HeaderKey, POC02_ChangeTypeCode, POC06_UnitPrice, POC09_GTIN_EAN_UCC, 
                                POC09_GTIN_UCC, POC11_BuyerItemNo, POC13_VendorItemNo, POC15_CommodityGroupingID, POC17_CaseID, 
                                PO406_WeightPerPack, PO407_WeightUnit, PO408_VolumePerPack, PO409_VolumeUnit, 
                                QTY02_QuantityAdjusted, QTY03_01_Unit )
                                values
                                (@HeaderKey, @POC02_ChangeTypeCode, @POC06_UnitPrice, @POC09_GTIN_EAN_UCC, 
                                @POC09_GTIN_UCC, @POC11_BuyerItemNo, @POC13_VendorItemNo, @POC15_CommodityGroupingID, @POC17_CaseID, 
                                @PO406_WeightPerPack, @PO407_WeightUnit, @PO408_VolumePerPack, @PO409_VolumeUnit, 
                                @QTY02_QuantityAdjusted, @QTY03_01_Unit );
                                SELECT scope_identity()";

                            oDaDetail.InsertCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@HeaderKey", nHeaderkey);
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@POC02_ChangeTypeCode", oSegment.get_DataElementValue(2, 0));          // Change or Response Type Code (670) 
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@POC06_UnitPrice", oSegment.get_DataElementValue(6, 0));               // Unit Price (212)

                            if (oSegment.get_DataElementValue(8, 0) == "EN")                                                                        // Product/Service ID Qualifier (235) 
                            {
                                oDaDetail.InsertCommand.Parameters.AddWithValue("@POC09_GTIN_EAN_UCC", oSegment.get_DataElementValue(9, 0));        // Product/Service ID (234) 
                                oDaDetail.InsertCommand.Parameters.AddWithValue("@POC09_GTIN_UCC", "");                                             // Product/Service ID (234) 
                            }
                            else
                            {
                                oDaDetail.InsertCommand.Parameters.AddWithValue("@POC09_GTIN_UCC", oSegment.get_DataElementValue(9, 0));            // Product/Service ID (234) 
                                oDaDetail.InsertCommand.Parameters.AddWithValue("@POC09_GTIN_EAN_UCC", "");
                            }
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@POC11_BuyerItemNo", oSegment.get_DataElementValue(11, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@POC13_VendorItemNo", oSegment.get_DataElementValue(13, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@POC15_CommodityGroupingID", oSegment.get_DataElementValue(15, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@POC17_CaseID", oSegment.get_DataElementValue(17, 0));
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO406_WeightPerPack", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO407_WeightUnit", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO408_VolumePerPack", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@PO409_VolumeUnit", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@QTY02_QuantityAdjusted", "");
                            oDaDetail.InsertCommand.Parameters.AddWithValue("@QTY03_01_Unit", "");
                            nDetailkey = (Int32)(decimal)oDaDetail.InsertCommand.ExecuteScalar();

                        }
                        else if (sSegmentID == "PO4")
                        {
                            sSql = @"UPDATE [860_Detail] SET PO406_WeightPerPack = @PO406_WeightPerPack, PO407_WeightUnit = @PO407_WeightUnit, 
                                        PO408_VolumePerPack = @PO408_VolumePerPack, PO409_VolumeUnit = @PO409_VolumeUnit
                                        where  DetailKey = @DetailKey";
                            oDaDetail.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@PO406_WeightPerPack", oSegment.get_DataElementValue(6, 0));   // Gross Weight per Pack (384) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@PO407_WeightUnit", oSegment.get_DataElementValue(7, 0));      // Unit or Basis for Measurement Code (355) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@PO408_VolumePerPack", oSegment.get_DataElementValue(8, 0));   // Gross Volume per Pack (385) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@PO409_VolumeUnit", oSegment.get_DataElementValue(9, 0));      // Unit or Basis for Measurement Code (355) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@DetailKey", nDetailkey);
                            oDaDetail.UpdateCommand.ExecuteNonQuery();

                        } // sSegmentID

                    }
                    else if (sLoopSection == "POC;QTY")
                    {
                        if (sSegmentID == "QTY")
                        {
                            sSql = @"UPDATE [860_Detail] SET QTY02_QuantityAdjusted = @QTY02_QuantityAdjusted, QTY03_01_Unit = @QTY03_01_Unit
                                        where  DetailKey = @DetailKey";
                            oDaDetail.UpdateCommand = new SqlCommand(sSql, oConnection);
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@QTY02_QuantityAdjusted", oSegment.get_DataElementValue(2, 0));   // Quantity (380) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@QTY03_01_Unit", oSegment.get_DataElementValue(3, 1));             // Unit or Basis for Measurement Code (355) 
                            oDaDetail.UpdateCommand.Parameters.AddWithValue("@DetailKey", nDetailkey);
                            oDaDetail.UpdateCommand.ExecuteNonQuery();
                        }
                    }

                }
                else if (nArea == 3)
                {
                    if (sSegmentID == "CTT")
                    {
                        sSql = @"UPDATE [860_Header] SET CTT01_NumberOfPOCSegments = @CTT01_NumberOfPOCSegments 
                            where Headerkey = @Headerkey";
                        oDaHeader.UpdateCommand = new SqlCommand(sSql, oConnection);
                        oDaHeader.UpdateCommand.Parameters.AddWithValue("@CTT01_NumberOfPOCSegments", oSegment.get_DataElementValue(1, 0));     // Number of Line Items (354) 
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
