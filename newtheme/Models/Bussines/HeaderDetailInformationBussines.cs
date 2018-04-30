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
                                                   Company = x.Field<string>("DocumentType"),
                                                   DocumentNumber = x.Field<string>("DocumentNumber"),
                                                   AlternateDocument = x.Field<string>("AltDocument"),
                                                   DocumentType = x.Field<string>("DocumentType"),
                                                   Amount = x.Field<decimal>("Amount"),
                                                   DateRecieved = x.Field<string>("DateRecieved"),
                                                   DateAcknowledgement = x.Field<string>("DateAcknowledgement") ,
                                                   StoreNumber = x.Field<string>("StoreNumber"),
                                                   TradingPartner = x.Field<string>("StoreNumber")
                                               }).ToList();


            }
            return ListHeader_Details_Information;
        }

        
    }
}