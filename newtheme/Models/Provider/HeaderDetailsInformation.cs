using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EDI.Structure;
using System.Threading.Tasks;

namespace EDI.Provider
{
    public class HeaderDetailsInformation
    {
        SqlHelper SqlHelper = new SqlHelper();

        public DataSet GetHeaderDetailInformation()
        {
            DataSet ds = new DataSet();
            ds=SqlHelper.ExecuteProcudere("GetHeaderDetailInformation", null);
            return ds;

        }
    }
}