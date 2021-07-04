using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_group")]
    public class ItemGroup
    {
        [Key]
        [Column("group_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required, MaxLength(100)]
        public string group_name { get; set; }

        public virtual ICollection<ItemMaster> ItemsMaster { get; set; }
    }
}
