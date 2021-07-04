using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_invoice")]
    public class Invoice
    {
        [Key]
        [Required, StringLength(8)]
        public string invoiceNo { get; set; }

        [Required]
        public string poNumber { get; set; }

        public string customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual Customer customer { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public int terms { get; set; }

        [Required]
        public DateTime issuanceDate { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }

    }
}
