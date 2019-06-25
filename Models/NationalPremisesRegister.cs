using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hidMy.Models
{
     [Table("NationalPremisesRegister")]
    public class NationalPremisesRegister
    {

        
        
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public NationalPremisesRegister()
            {
                InspectionDetails = new HashSet<InspectionDetails>();
            }

            [StringLength(255)]
            public string Area { get; set; }

            [StringLength(255)]
            public string SubArea { get; set; }

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Reference { get; set; }

            [StringLength(255)]
            public string Name { get; set; }

            [StringLength(255)]
            public string Telephone { get; set; }

            [StringLength(255)]
            public string SubBuildingNumber { get; set; }

            [StringLength(255)]
            public string BuildingName { get; set; }

            [StringLength(255)]
            public string BuildingNumber { get; set; }

            [StringLength(255)]
            public string StreetName { get; set; }

            [StringLength(255)]
            public string Townland { get; set; }

            [StringLength(255)]
            public string Locality { get; set; }

            [StringLength(255)]
            public string CityTown { get; set; }

            [StringLength(255)]
            public string County { get; set; }

            [StringLength(255)]
            public string Postcode { get; set; }

            [StringLength(255)]
            public string FoodBusinessOperators { get; set; }

            [Column("852Registered")]
            [StringLength(255)]
            public string C852Registered { get; set; }

            [Column("852Unregistered")]
            [StringLength(255)]
            public string C852Unregistered { get; set; }

            [Column("853Approved")]
            [StringLength(255)]
            public string C853Approved { get; set; }

            [StringLength(255)]
            public string BusinessCategory { get; set; }

            [StringLength(255)]
            public string BusinessType { get; set; }

            public int? RiskCategory { get; set; }

            public DateTime? LastInspection { get; set; }

            public DateTime? LastPlannedInspection { get; set; }

            public DateTime? LastPlannedSurveillanceInspection { get; set; }

            [Column("NextPlanned Planned Surveillance")]
            public DateTime? NextPlanned_Planned_Surveillance { get; set; }

            [StringLength(255)]
            public string NFL { get; set; }

            [StringLength(255)]
            public string OOJ { get; set; }

            [StringLength(255)]
            public string SRM { get; set; }

            [StringLength(255)]
            public string MLR { get; set; }

            [StringLength(255)]
            public string FoodOfficer { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<InspectionDetails> InspectionDetails { get; set; }
        

    }
}