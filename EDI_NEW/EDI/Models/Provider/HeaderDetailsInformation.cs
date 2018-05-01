using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EDI.Structure;
using System.Threading.Tasks;
using System.Collections;

namespace EDI.Provider
{
    public class HeaderDetailsInformation
    {
        SqlHelper SqlHelper = new SqlHelper();

        public DataSet GetHeaderDetailInformation()
        {
            DataSet ds = new DataSet();
            ds=SqlHelper.ExecuteProcudere("SPO_GetHeaderDetailInformation", null);
            return ds;

        }
        public DataSet GetOutboxlInformation()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteProcudere("SPO_getOutboxItems", null);
            return ds;

        }
        public DataSet GeTransactionInboxDetails(int TId, int HeaderKey)
        {
            DataSet ds = new DataSet();
            Hashtable parms = new Hashtable();
            parms.Add("@TId", TId);
            parms.Add("@HeaderKey", HeaderKey);
            ds = SqlHelper.ExecuteProcudere("SPO_TransactionInboxDetail", parms);
            return ds;

        }
        public DataSet transactionInboxDetailsGetItems(int HeaderKey)
        {
            DataSet ds = new DataSet();
            Hashtable parms = new Hashtable();
            parms.Add("@HeaderKey", HeaderKey);
            ds = SqlHelper.ExecuteProcudere("SPO_transactionInboxDetailsGetItems", parms);
            return ds;

        }
    }
}