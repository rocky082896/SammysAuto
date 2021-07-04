using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblSentMessages")]
    public class SentMessagesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SentId { get; set; }

        public string MobileNumber { get; set; }
        [ForeignKey("MobileNumber")]
        public virtual StudentsModel Students { get; set; }

        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
