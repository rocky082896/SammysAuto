using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_supplier")]
    public class Supplier
    {
        [Key]
        public string supp_id { get; set; }

        [Required, StringLength(65)]
        public string supplier_name { get; set; }

        [Required, StringLength(65)]
        public string address { get; set; }

        [Required, StringLength(15)]
        public string contact_1 { get; set; }

        public string contact_2 { get; set; }

        public string email { get; set; }

        [Timestamp]
        public byte[] date_created { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }

    }
}
