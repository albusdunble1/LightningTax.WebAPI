using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightningTax.WebAPI.Models.Entities
{
    [Table("company_year")]
    public class CompanyYear
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Company))]
        public long CompanyId { get; set; }

        public Company Company { get; set; } = null!;

        public int YearOfAssessment { get; set; }

        public DateOnly BasisPeriodStart { get; set; }
        public DateOnly BasisPeriodEnd { get; set; }

        public string Status { get; set; } = "draft";

        [Required]
        public string RuleVersion { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
