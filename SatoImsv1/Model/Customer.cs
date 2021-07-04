using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_customer")]
    public class Customer
    {
        [Key]
        public string customer_id { get; set; }

        [StringLength(65)]
        [Index(IsUnique = true)]
        public string customer_name { get; set; }

        [Required, StringLength(65)]
        public string address { get; set; }

        [StringLength(15)]
        public string tin { get; set; }

        [Required, StringLength(14)]
        public string contact_1 { get; set; }

        [StringLength(14)]
        public string contact_2 { get; set; }

        [StringLength(35)]
        public string email { get; set; }

        [Timestamp]
        public byte[] date_created { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }
    }
}
