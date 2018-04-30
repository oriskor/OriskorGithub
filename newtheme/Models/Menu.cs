using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDI.Models
{
    public class Menu
    {
        EDIEntities db = new EDIEntities();


        public List<tradingPartnerSetup> menuStructuredModel;


        public List<tradingPartnerSetup> MenuStructuredModel
        {
            get
            {
                return menuStructuredModel;
            }
            set
            {
                menuStructuredModel = value;
            }
        }
    }
}