using LightningTax.WebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightningTax.WebAPI.Models.Entities
{
    [Table("company")]
    public class Company
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string CompanyNumber { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        //public string? ResidentStatus { get; set; } // resident | non_resident
        public ResidentStatusEnum? ResidentStatus { get; set; } // resident | non_resident

        public string? BusinessType { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? PaidUpCapital { get; set; }

        public bool SmeEligible { get; set; }

        [Required]
        public DateOnly FinancialYearEnd { get; set; }

        [Required]
        public DateOnly CommencementDate { get; set; }

        public DateOnly? CessationDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
