using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_po_items")]
    public class PurchaseOrderItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int item_id { get; set; }

        public string item_no { get; set; }
        [ForeignKey("item_no")]
        public virtual ItemMaster itemMaster { get; set; }

        [Required]
        public string poNumber { get; set; }

        [Required]
        public string unit { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public double amount { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }


    }
}
