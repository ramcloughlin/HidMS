using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hidMy.Models
{
    public class BusinessPlanAdditionalControl
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]

        public int AdditionalControlID { get; set; }
        public Nullable<int> RiskCategory { get; set; }
        public string BusinessCategory { get; set; }
        public string BusinessType { get; set; }
        public int InspectionChecklistID { get; set; }
        public Nullable<System.DateTime> Year { get; set; }
        public string FoodsHandled { get; set; }
        public string SpecificInformation { get; set; }
        public Nullable<bool> Obsolete { get; set; }
    }
}