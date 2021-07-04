using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_privilege")]
    public class Privilege
    {
        [Key]
        [Column("priv_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int admin_rights { get; set; }
        public int user_rights { get; set; }
        public int receiving_rights { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
