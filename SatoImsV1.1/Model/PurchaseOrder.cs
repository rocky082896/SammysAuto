using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_po")]
    public class PurchaseOrder
    {
        [Key, Column("poId", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Key, Column(Order = 1)]
        [MaxLength(50)]
        public string poNumber { get; set; }

        [Required]
        public int creditTerms { get; set; }

        [Required]
        public int poValidity { get; set; }

        [StringLength(100)]
        public string remarks { get; set; }

        [Required]
        public double totalAmount { get; set; }

        [Timestamp]
        public byte[] dateCreated { get; set; }

        [Required]
        public DateTime? deliveryDate { get; set; }

        public int status { get; set; }
    }
}
