using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_item_master")]
    public class ItemMaster
    {
        [Key]
        public string item_no { get; set; }

        [Required]
        public string item_desc { get; set; }

        public int cat_id { get; set; }
        [ForeignKey("cat_id")]
        public virtual Category category { get; set; }

        public int group_id { get; set; }
        [ForeignKey("group_id")]
        public virtual ItemGroup itemGroup { get; set; }

        [Required]
        public string uom { get; set; }

        [Required, StringLength(12)]
        public string currency { get; set; }

        [Required]
        public double price { get; set; }

        [StringLength(200)]
        public string image_src { get; set; }

        [Timestamp]
        public byte[] date_upload { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }
    }
}
