using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("item_no")]
    public class ItemMaster
    {
        [Key]
        public string item_no { get; set; }

        [Required, StringLength(65)]
        public string item_desc { get; set; }

        public int cat_id { get; set; }
        [ForeignKey("cat_id")]
        public virtual Category category { get; set; }

        [Required]
        public string uom { get; set; }

        [Required, StringLength(5)]
        public string currency { get; set; }

        [Required]
        public double price { get; set; }

        [StringLength(100)]
        public string image_src { get; set; }

        [Timestamp]
        public byte[] date_upload { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }
    }
}
