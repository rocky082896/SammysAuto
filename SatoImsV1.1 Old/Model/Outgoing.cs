using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_outgoing")]
    public class Outgoing
    {
        [Key]
        [Column("outgoing_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("outgoing_no")]
        public string OutgoingNum { get; set; }

        public int Status { get; set; }

        [Column("total_quantity")]
        public int TotalQuantity { get; set; }

        [Column("currency")]
        [StringLength(5)]
        public string Currency { get; set; }

        [Column("date_completion")]
        [Timestamp]
        public byte[] DateOfCompletion { get; set; }

    }
}
