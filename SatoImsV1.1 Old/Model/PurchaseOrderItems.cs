using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_po_items")]
    public class PurchaseOrderItems
    {
        [Key]
        [Column("item_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string item_no { get; set; }
        [ForeignKey("item_no")]
        public ItemMaster itemMaster { get; set; }

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
