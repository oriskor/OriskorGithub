using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EDI.Models.Provider
{
    public class TradindPartnerProvider
    {
        SqlHelper SqlHelper = new SqlHelper();
        public DataSet GetTradingPartnerIdentifiers(string TradingPartnerId)
        {
            DataSet ds = new DataSet();
            Hashtable parms = new Hashtable();
            parms.Add("@TradingPartnerId", TradingPartnerId);
            ds = SqlHelper.ExecuteProcudere("SPO_GetTradingPartenerIdentifier", parms);
            return ds;

        }
    }
}