using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblStudentOut")]
    public class StudentOutModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentOutId { get; set; }
        public string RfidTag { get; set; }
        [ForeignKey("RfidTag")]
        public virtual StudentsModel Students { get; set; }
        [Timestamp]
        public byte[] TimeOut { get; set; }
    }
}
