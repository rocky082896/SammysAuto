using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblStudentIn")]
    public class StudentInModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceInId { get; set; }

        public string RfidTag { get; set; }
        [ForeignKey("RfidTag")]
        public virtual StudentsModel Students { get; set; }

        [Timestamp]
        public byte[] TimeIn { get; set; }
    }
}
