using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LightningTax.WebAPI.Models.Entities
{
    [Table("schedule3_output")]
    public class Schedule3Output
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }
        public long CompanyYearId { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? TotalQe { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? TotalIa { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? TotalAa { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal? TotalCa { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
