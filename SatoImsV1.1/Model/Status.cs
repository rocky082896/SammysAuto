using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_status")]
    public class Status
    {
        [Key]
        [Column("status")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(12)]
        public string status_desc { get; set; }

    }
}
