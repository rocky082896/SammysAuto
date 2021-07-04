using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_office_po_items")]
    public class OfficePurchaseOrderItems
    {
        [Key]
        [Column("office_item_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string item_no { get; set; }
        [ForeignKey("item_no")]
        public virtual ItemMaster itemMaster { get; set; }

        [Required]
        public string officePoNumber { get; set; }

        [Required]
        public string officeUnit { get; set; }

        [Required]
        public int officeQuantity { get; set; }

        [Required]
        public double officePrice { get; set; }

        [Required]
        public double officeAmount { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }
    }
}
