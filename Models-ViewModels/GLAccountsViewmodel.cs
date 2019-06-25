using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hidMy.Models_ViewModels
{
    public class GLAccountsViewmodel
    {

        public long Account { get; set; }
        public string Name { get; set; }
        public byte[] hid { get; set; }
        public string ClauseText { get; set; }
      //  public string NonComplianceText { get; set; }
      //  public string RemedialActionText { get; set; }

    }
}