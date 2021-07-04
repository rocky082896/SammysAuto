using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblAnnouncement")]
    public class AnnouncementModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnnouncementId { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        [Timestamp]
        public byte[] DateCreated;

        [Required]
        public DateTime ValidActive { get; set; }

        [Required]
        public DateTime ValidUpto { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel Users { get; set; }
    }
}
