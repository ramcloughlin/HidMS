using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hidMy.Models_ViewModels
{
    public class GLAspectsandElementLeglislation
    {
       public long AspectsElementsID { get; set; }

        public string Name { get; set; }
        public string ClauseText { get; set; }
        public long LeglislationLINK { get; set; }
        public string Comment { get; set; }

        public Nullable<int> CommentLINK { get; set; }

        public byte[] hid { get; set; }

        public short? Level { get; set; }

        public long? parent { get; set; }

        public string Clause { get; set; }

    }
}