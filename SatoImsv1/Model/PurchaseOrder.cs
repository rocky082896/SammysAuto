using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_po")]
    public class PurchaseOrder
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int poId { get; set; }

        [Key, Column(Order = 1)]
        public string poNumber { get; set; }

        [Required]
        public int creditTerms { get; set; }

        [Required]
        public int poValidity { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        [Required]
        public double totalAmount { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }
    }
}
