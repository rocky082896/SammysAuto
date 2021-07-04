using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinityAttenTrace.Model
{
    [Table("tblStudent")]
    public class StudentsModel
    {
        [Key]
        [Required, StringLength(11)]
        public string StudentId { get; set; }
        [Required, StringLength(35)]
        public string Firstname { get; set; }
        [Required, StringLength(35)]
        public string Lastname { get; set; }
        [Required, StringLength(35)]
        public string Middlename { get; set; }
        [Required, StringLength(4)]
        public string Suffix { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required, StringLength(35)]
        public string Guardian { get; set; }
        [Required, StringLength(15)]
        public string MobileNumber { get; set; }
        [Timestamp]
        public byte[] DateAdded { get; set; }
        [StringLength(35)]
        public string Email { get; set; }
        [Required, StringLength(15)]
        public string Relationship { get; set; }
        [Required, StringLength(60)]
        public string Address { get; set; }
        [StringLength(25)]
        public string Religion { get; set; }
        [StringLength(25)]
        public string Nationality { get; set; }
        [Required, StringLength(15)]
        public string RfidTag { get; set; }
        [StringLength(200)]
        public string ImageSource { get; set; }
        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
