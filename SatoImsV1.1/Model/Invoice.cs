using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_invoice")]
    public class Invoice
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key, Column(Order = 1)]
        [Required, StringLength(8)]
        public string invoiceNo { get; set; }

        [Required]
        public string poNumber { get; set; }

        public string customer_id { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public int terms { get; set; }

        [Required]
        public DateTime issuanceDate { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }

        [ForeignKey("customer_id")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

    }
}
