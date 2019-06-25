using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace hidMy.Models
{
   

    public partial class InspectionDetails
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        
        public int InspectionNo { get; set; }

       [Required]
        public DateTime Date { get; set; }
       
        public int? PremisesRef { get; set; }
        [Required]
        [DisplayName("Supplying other Business with POAO")]
        public string MLRQ1 { get; set; }
        //[Required]
        [DisplayName("POAO supplied to other business is storage or transport only")]
        public string MLRQ2 { get; set; }
       // [Required]
        [DisplayName("Supplying composite product to other business only")]
        public string MLRQ3 { get; set; }
        //[Required]
        [DisplayName("Retail to Retail Supply of POAO")]
        public string MLRQ4{ get; set; }
       // [Required]
        [DisplayName("Supply within MLR Criteria")]
        public string MLRQ5 { get; set; }

        [StringLength(200)]
        public string officer { get; set; }

        [StringLength(200)]
        public string GeneralHygieneStatus { get; set; }

        public virtual NationalPremisesRegister NationalPremisesRegister { get; set; }
    }
}