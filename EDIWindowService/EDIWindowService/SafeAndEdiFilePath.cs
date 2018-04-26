using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EDIWindowService
{
    public class SafeAndEdiFilePath
    {

        public EdiFilesPaths GetFilesAndPaths()
        {
            EdiFilesPaths List = new EdiFilesPaths()
            {               
                     sPath =@"" + ConfigurationManager.AppSettings["AppPath"] + @"\"  ,
                     sInboundPath = @"" + ConfigurationManager.AppSettings["AppPath"]   + ConfigurationManager.AppSettings["EDI_Inbound"] + @"\",
                     sOutboundPath =  @"" + ConfigurationManager.AppSettings["AppPath"]   + ConfigurationManager.AppSettings["EDI_Outbound"] + @"\",
                     sSefPath =@"" + ConfigurationManager.AppSettings["AppPath"]  + ConfigurationManager.AppSettings["Seffolder"] + @"\",
                     sAcceptPath  = @"" + ConfigurationManager.AppSettings["AppPath"]  + ConfigurationManager.AppSettings["EDI_Accepted"] + @"\",
                     sEdiDonePath  = @"" + ConfigurationManager.AppSettings["AppPath"]  + ConfigurationManager.AppSettings["EDI_DONE"] + @"\" ,
                     sRejectPath =@"" + ConfigurationManager.AppSettings["AppPath"]  + ConfigurationManager.AppSettings["EDI_Rejected"] + @"\",
                     s997Path =@"" + ConfigurationManager.AppSettings["AppPath"]  +  ConfigurationManager.AppSettings["EDI_997"] + @"\"                  
            };

            return List;

        }
    }
}
