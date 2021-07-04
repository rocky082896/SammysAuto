using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_receiving")]
    public class Receiving
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rec_id { get; set; }

        public string item_no { get; set; }
        [ForeignKey("item_no")]
        public virtual ItemMaster itemMaster { get; set; }

        [Required]
        public string serialNo { get; set; }

        public string supp_id { get; set; }
        [ForeignKey("supp_id")]
        public virtual Supplier supplier { get; set; }

        [Timestamp]
        public byte[] date_rec { get; set; }

        [Required]
        public int rec_qty { get; set; }

        [Required]
        public int current_qty { get; set; }

        public string user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual User user { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }

    }
}
