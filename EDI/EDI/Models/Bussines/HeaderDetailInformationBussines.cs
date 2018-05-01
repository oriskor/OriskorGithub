using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EDI.Provider;
using EDI.Structure;


namespace EDI.Models.Bussines
{
    public class HeaderDetailInformationBussines 
    {
        HeaderDetailsInformation objHeaderDetailsInformation = new HeaderDetailsInformation();


        public List<HeaderDetailInformation> GetInboxDetails()
        {
            List<HeaderDetailInformation> ListHeader_Details_Information = new List<HeaderDetailInformation>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds= objHeaderDetailsInformation.GetHeaderDetailInformation(); 
            if(ds.Tables.Count>0)
            {
                dt = ds.Tables[0];
                ListHeader_Details_Information = dt.AsEnumerable()
                                               .Select(x => new HeaderDetailInformation
                                               {
                                                   HeaderKey= x.Field<int>("HeaderKey"),
                                                   Company = x.Field<string>("CompanyName"),
                                                   DocumentNumber = x.Field<string>("DocumentNumber"),
                                                   AlternateDocument = x.Field<string>("AltDocument"),
                                                   DocumentType = x.Field<string>("DocumentType"),
                                                   Amount = x.Field<string>("Amount"),
                                                   DateRecieved = x.Field<string>("DateRecieved"),
                                                   DateAcknowledgement = x.Field<string>("DateAcknowledgement") ,
                                                   StoreNumber = x.Field<string>("StoreNumber"),
                                                   TradingPartner = x.Field<string>("TradingPartner")
                                               }).ToList();


            }
            return ListHeader_Details_Information;
        }
        public List<HeaderDetailInformation> getOutboxItems()
        {
            List<HeaderDetailInformation> ListHeader_Details_Information = new List<HeaderDetailInformation>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = objHeaderDetailsInformation.GetOutboxlInformation();

            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                ListHeader_Details_Information = dt.AsEnumerable()
                                               .Select(x => new HeaderDetailInformation
                                               {
                                                   HeaderKey = x.Field<int>("HeaderKey"),
                                                   Company = x.Field<string>("CompanyName"),
                                                   DocumentNumber = x.Field<string>("DocumentNumber"),
                                                   AlternateDocument = x.Field<string>("AltDocument"),
                                                   DocumentType = x.Field<string>("DocumentType"),
                                                   Amount = x.Field<string>("Amount"),
                                                   DateRecieved = x.Field<string>("DateRecieved"),
                                                   DateAcknowledgement = x.Field<string>("DateAcknowledgement"),
                                                   StoreNumber = x.Field<string>("StoreNumber"),
                                                   TradingPartner = x.Field<string>("TradingPartner")
                                               }).ToList();


            }
            return ListHeader_Details_Information;
        }

        public TransactionInboxDetails transactionInboxDetails(int? id)
        {
            List<TransactionInboxDetails> ListTransactionInboxDetails = new List<TransactionInboxDetails>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = objHeaderDetailsInformation.GeTransactionInboxDetails(0,Convert.ToInt32(id));
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                ListTransactionInboxDetails = dt.AsEnumerable()
                                               .Select(x => new TransactionInboxDetails
                                               {
                                                   HeaderKey = x.Field<int>("HeaderKey"),
                                                   CompanyName = x.Field<string>("CompanyName"),
                                                   Purpose = x.Field<string>("Purpose"),
                                                   Type = x.Field<string>("Type"),
                                                   PO = x.Field<string>("PO"),
                                                   AltDocument = x.Field<string>("AltDocument"),
                                                   TotalOfLineItems = x.Field<decimal>("TotalOfLineItems"),
                                                   TotalAmount = x.Field<decimal>("TotalAmount"),
                                                   Othercharges = x.Field<string>("Othercharges"),

                                                    TradingPartner = x.Field<string>("TradingPartner"),
                                                   DateRecieved = x.Field<string>("DateRecieved"),
                                                   PODate = x.Field<string>("PODate"),
                                                   StoreNumber = x.Field<string>("StoreNumber"),
                                                   VendorItemNo = x.Field<string>("VendorItemNo"),
                                                   UOM = x.Field<string>("UOM"),
                                                   TransactionId = x.Field<string>("TransactionId"),
                                                   CodeShipToID = x.Field<string>("CodeShipToID"),
                                                   CodeShipFromID = x.Field<string>("CodeShipFromID"),
                                                   SentDate = x.Field<string>("SentDate"),
                                                   FunctionalControlNo = x.Field<string>("FunctionalControlNo"),
                                                   Price = x.Field<decimal>("Price")
                                               }).ToList();


            }
            return ListTransactionInboxDetails.FirstOrDefault();
        }
        public List<TransactionInboxDetailsItem> transactionInboxDetailsGetItems(int id)
        {
            List<TransactionInboxDetailsItem> ListTransactionInboxDetails = new List<TransactionInboxDetailsItem>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = objHeaderDetailsInformation.transactionInboxDetailsGetItems(Convert.ToInt32(id));
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                ListTransactionInboxDetails = dt.AsEnumerable()
                                               .Select(x => new TransactionInboxDetailsItem
                                               {

                                                   UOM = x.Field<string>("UOM"),
                                                   Qty = x.Field<string>("Qty"),
                                                   PriceBasis = x.Field<string>("PriceBasis"),
                                                   VendorItem = x.Field<string>("VendorItem"),
                                                   ItemPo = x.Field<string>("ItemPo"),
                                                   Price = x.Field<string>("Price")
                                               }).ToList();


            }
            return ListTransactionInboxDetails;
        }

    }
}