using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teleperformance.Model
{
    [Table("tbl_inbound")]
    public class Inbound
    {
        [Key]
        [Column("inbound_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InboundId { get; set; }

        [Column("tag_id")]
        [Required, MaxLength(100)]
        public string TagId { get; set; }

        [Column("asset_no")]
        [Required, MaxLength(100)]
        public string AssetNumber { get; set; }

        [Column("asset_desc")]
        [Required, MaxLength(100)]
        public string AssetDescription { get; set; }

        [Column("date_time")]
        public string DatenTime { get; set; }

        [Column("status")]
        [DefaultValue(1)]
        public int Status { get; set; }

    }
}
