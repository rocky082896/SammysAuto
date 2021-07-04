using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_category")]
    public class Category
    {
        [Key]
        [Column("cat_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required, MaxLength(100)]
        public string cat_desc { get; set; }

        [Timestamp]
        public byte[] date_created { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }

        public virtual ICollection<ItemMaster> ItemsMaster { get; set; }


    }
}
