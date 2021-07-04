using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_item_status")]
    public class ItemStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int item_stat_id { get; set; }

        public string item_no { get; set; }
        [ForeignKey("item_no")]
        public virtual ItemMaster itemMaster { get; set; }

        [Required]
        public int good_qty { get; set; }

        [Required]
        public int bad_qty { get; set; }

        [Timestamp]
        public byte[] date_updated { get; set; }
    }
}
