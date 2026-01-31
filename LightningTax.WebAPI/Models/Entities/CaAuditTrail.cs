using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LightningTax.WebAPI.Models.Entities
{
    [Table("ca_audit_trail")]
    public class CaAuditTrail
    {
        [Key]
        public long Id { get; set; }

        public long CompanyId { get; set; }
        public long CompanyYearId { get; set; }
        public long AssetId { get; set; }

        public string? RuleApplied { get; set; }

        //[Column(TypeName = "jsonb")]
        //public JsonDocument? Inputs { get; set; }

        [Column(TypeName = "jsonb")]
        public string Inputs { get; set; } = "{}";

        public string? Formula { get; set; }

        [Column(TypeName = "numeric(15,2)")]
        public decimal Output { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
