using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblAdviser")]
    public class AdviserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdviserId { get; set; }

        [Required, StringLength(35)]
        public string Firstname { get; set; }

        [Required, StringLength(35)]
        public string Lastname { get; set; }

        [Required, StringLength(35)]
        public string Middlename { get; set; }

        [Timestamp]
        public byte[] DateAdded { get; set; }

        [DefaultValue(1)]
        public int Status { get; set; }

        public ICollection<AdviserModel> Advisers { get; set; }
    }
}
