using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_customer")]
    public class Customer
    {
        [Key]
        public string customer_id { get; set; }

        [MaxLength(65)]
        [Index(IsUnique = true)]
        public string customer_name { get; set; }

        [Required]
        public string address { get; set; }

        [StringLength(15)]
        public string tin { get; set; }

        [Required, StringLength(14)]
        public string contact_1 { get; set; }

        [StringLength(14)]
        public string contact_2 { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [Timestamp]
        public byte[] date_created { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }

        public virtual ICollection<Dispatch> Dispatches { get; set; }
    }
}
