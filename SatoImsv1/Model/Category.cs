using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cat_id { get; set; }

        [Required, StringLength(35)]
        public string cat_desc { get; set; }

        [Timestamp]
        public byte[] date_created { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }
    }
}
