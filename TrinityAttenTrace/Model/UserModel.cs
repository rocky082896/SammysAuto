using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblUser")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required, StringLength(11)]
        public string UserName { get; set; }
        [Required, StringLength(11)]
        public string Password { get; set; }
        [Required, StringLength(15)]
        public string Firstname { get; set; }
        [Required, StringLength(15)]
        public string Lastname { get; set; }
        [DefaultValue(1)]
        public int Status { get; set; }
        [Required]
        public int AccessId { get; set; }
    }
}
