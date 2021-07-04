using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_outgoing_header")]
    public class Outgoing
    {
        [Key]
        [Column("REC_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int REC_ID { get; set; }

        [Column("OUTGOING_NO")]
        public string Outgoing_No { get; set; }

        [Column("OUTGOING_STATUS")]
        public string Outgoing_Status { get; set; }

        [Column("TOTAL_ITEM_AMOUNT")]
        public double Total_Item_Amount { get; set; }

        [Column("CURRENCY")]
        [StringLength(5)]
        public string Currency { get; set; }

        [Column("COMPLETION_DATE")]
        public DateTime? Completion_Date { get; set; }

    }
}
