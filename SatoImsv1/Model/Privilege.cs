using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsv1.Model
{
    [Table("tbl_privilege")]
    public class Privilege
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int priv_id { get; set; }

        public int admin_rights { get; set; }
        public int user_rights { get; set; }
        public int receiving_rights { get; set; }
    }
}
