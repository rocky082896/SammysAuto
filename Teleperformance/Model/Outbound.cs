using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teleperformance.Model
{
    [Table("tbl_outbound")]
    public class Outbound
    {
        [Key]
        [Column("outbound_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OutboundId { get; set; }

        [Column("inbound_id")]
        public int InboundId { get; set; }

        [Column("date_time")]
        public string DatenTime { get; set; }

        [Column("status")]
        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
