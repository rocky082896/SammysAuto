using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_user")]
    public class User
    {
        [Key]
        [MaxLength(11)]
        public string user_id { get; set; }

        [Required, StringLength(35)]
        public string firstname { get; set; }

        [Required, StringLength(35)]
        public string lastname { get; set; }

        [Required, StringLength(25)]
        public string password { get; set; }

        public int priv_id { get; set; }
        [ForeignKey("priv_id")]
        public Privilege privilege { get; set; }

        [DefaultValue(1)]
        public int status { get; set; }

        public virtual ICollection<Receiving> Receivings { get; set; }
    }
}
