using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_print")]
    public class Print
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key, Column(Order = 1)]
        public string control_no { get; set; }

        [Required]
        public string item_code { get; set; }

        [Required]
        public int quantity { get; set; }

        [Timestamp]
        public byte[] date_printed { get; set; }
    }
}
