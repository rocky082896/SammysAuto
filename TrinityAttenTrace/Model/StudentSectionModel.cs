using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblStudentSection")]
    public class StudentSectionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentSectionId { get; set; }

        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public virtual SectionModel Sections { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual StudentsModel Students { get; set; }

        public int AdviserId { get; set; }
        [ForeignKey("AdviserId")]
        public virtual AdviserModel Advisers { get; set; }

        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
