using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hidMy.Models
{
    public class BusinessCategoryType
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]

        public int CatID { get; set; }
        public string BusinessType { get; set; }
        public string BusinessCategory { get; set; }
    }
}