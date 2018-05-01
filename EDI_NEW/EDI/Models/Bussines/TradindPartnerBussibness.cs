using EDI.Class_Structure;
using EDI.Models.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EDI.Models.Bussines
{
    public class TradindPartnerBussibness
    {
        TradindPartnerProvider objTradindPartnerProvider = new TradindPartnerProvider();

        public List<TradingPartnerIdentifire> GetInboxDetails(string TradingPartnerId)
        {
            List<TradingPartnerIdentifire> LisTradingPartnerIdentifire = new List<TradingPartnerIdentifire>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = objTradindPartnerProvider.GetTradingPartnerIdentifiers(TradingPartnerId);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                LisTradingPartnerIdentifire = dt.AsEnumerable()
                                               .Select(x => new TradingPartnerIdentifire
                                               {
                                                   ID = x.Field<string>("ID"),
                                                   TPlugingName = x.Field<string>("TPlugingName"),
                                                   TPlugingVersion = x.Field<string>("TPlugingVersion"),
                                                   TPNumbering = x.Field<int>("TPNumbering"),
                                                   TPPECIdentifierName = x.Field<string>("TPPECIdentifierName"),
                                                   TPPECIdentifier_type = x.Field<string>("TPPECIdentifier_type"),
                                                   TPTECIdentifierName = x.Field<string>("TPTECIdentifierName"),
                                                   TPTECIdentifierType = x.Field<string>("TPTECIdentifierType")
                                               }).ToList();


            }
            return LisTradingPartnerIdentifire;
        }
    }
}