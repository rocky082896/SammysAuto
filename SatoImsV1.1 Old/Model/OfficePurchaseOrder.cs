using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_office_po")]
    public class OfficePurchaseOrder
    {
        [Key, Column("officePoId", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Key, Column(Order = 1)]
        [MaxLength(50)]
        public string officePoNumber { get; set; }

        public string supp_id { get; set; }
        [ForeignKey("supp_id")]
        public virtual Supplier Supplier { get; set; }

        [Required]
        public int officeCreditTerms { get; set; }

        [Required]
        public int officePoValidity { get; set; }

        [StringLength(100)]
        public string officeRemarks { get; set; }

        [Required]
        public double officeTotalAmount { get; set; }

        [Timestamp]
        public byte[] officeDateCreated { get; set; }

        [Required]
        public DateTime? officeDeliveryDate { get; set; }

        public int status { get; set; }

    }
}
