using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_print")]
    public class Print
    {
        [Key]
        public string control_no { get; set; }

        [Required, StringLength(25)]
        public string item_code { get; set; }

        [Required]
        public int quantity { get; set; }

        [Timestamp]
        public byte[] date_printed { get; set; }
    }
}
