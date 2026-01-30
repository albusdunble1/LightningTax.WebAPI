using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightningTax.WebAPI.Models.Entities
{
    [Table("capital_allowance")]
    public class CapitalAllowance
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }
        public long CompanyYearId { get; set; }
        public long AssetId { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal OpeningTwdv { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal UnabsorbedCaBf { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal QeCurrentYear { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal IaAmount { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal AaAmount { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal RestrictedCa { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? DisposalValue { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? BalancingAdjustment { get; set; }

        public string? BalancingType { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal ClosingTwdv { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal UnabsorbedCaCf { get; set; }

        public DateTime ComputedAt { get; set; }
        public string ComputedBy { get; set; } = "system";
    }
}
