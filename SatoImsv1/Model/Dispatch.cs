using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_dispatch_ledger")]
    public class Dispatch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ledger_id { get; set; }

        public string item_no { get; set; }
        [ForeignKey("item_no")]
        public ItemMaster itemMaster { get; set; }

        public string customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual Customer customer { get; set; }

        [Required]
        public int dispatch_qty { get; set; }

        [Timestamp]
        public byte[] date_created { get; set; }

    }
}
