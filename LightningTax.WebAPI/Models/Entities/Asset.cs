using LightningTax.WebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightningTax.WebAPI.Models.Entities
{
    [Table("asset")]
    public class Asset
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Company))]
        public long CompanyId { get; set; }

        public Company Company { get; set; } = null!;

        public string? AssetCode { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        public string? AssetCategory { get; set; }
        public string? Schedule3Class { get; set; }

        public DateOnly PurchaseDate { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal OriginalCost { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal QualifyingExpenditure { get; set; }

        [Column(TypeName = "numeric(5,2)")]
        public decimal BusinessUsePct { get; set; } = 100;

        [Column(TypeName = "numeric(5,2)")]
        public decimal? IaRate { get; set; }

        [Column(TypeName = "numeric(5,2)")]
        public decimal? AaRate { get; set; }

        public PoolTypeEnum? PoolType { get; set; } // single_rate | multi_rate

        public AssetStatusEnum Status { get; set; } = AssetStatusEnum.active;

        public DateTime CreatedAt { get; set; }
    }
}
