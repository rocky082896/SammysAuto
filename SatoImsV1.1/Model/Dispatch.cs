using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_dispatch_ledger")]
    public class Dispatch
    {
        [Key]
        public int ledger_id { get; set; }

        public string customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual Customer Customer { get; set; }

        public string item_no { get; set; }
        [ForeignKey("item_no")]
        public virtual ItemMaster ItemMaster { get; set; }

        [Required]
        public int dispatch_qty { get; set; }

        [Timestamp]
        public byte[] date_created { get; set; }

    }
}
