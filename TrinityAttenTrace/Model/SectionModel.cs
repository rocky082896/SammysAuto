using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblSection")]
    public class SectionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionId { get; set; }
        [Required, StringLength(35)]
        public string SectionName { get; set; }
        [Required]
        public int YearLevel { get; set; }
        [Required]
        public int SchoolYear { get; set; }
        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
