using LightningTax.WebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightningTax.WebAPI.Models.Entities
{
    [Table("capital_allowance_pool")]
    public class CapitalAllowancePool
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }
        public long CompanyYearId { get; set; }

        public PoolTypeEnum? PoolType { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? OpeningBalance { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? Additions { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? Disposals { get; set; }

        [Column(TypeName = "numeric(5,2)")]
        public decimal? AaRate { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? AaAmount { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? ClosingBalance { get; set; }
    }
}
