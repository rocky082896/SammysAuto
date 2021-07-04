using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatoImsV1._1.Model
{
    [Table("tbl_outgoing_details")]
    public class OutgoingDetails
    {
        [Key]
        [Column("REC_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int REC_ID { get; set; }

        [Column("RECEIVING_ID")]
        public string Receiving_ID { get; set; }

        [Column("OUTGOING_NO")]
        public string Outgoing_No_Details { get; set; }

        [Column("ITEM_NO")]
        public string Item_No { get; set; }

        [Column("PRICE")]
        public Double Price { get; set; }

        [Column("ITEM_AMOUNT")]
        public double Item_Amount { get; set; }

        [Column("ITEM_QUANTITY")]
        public int Item_Qty { get; set; }

        [Column("CURRENT_QTY")]
        public int Current_Qty { get; set; }

        [Column("ITEM_STATUS")]
        public int Item_Status { get; set; }

        [Column("CUSTOMER")]
        public string customer { get; set; }

        [Column("DELIVERYDATE")]
        public DateTime? deliverydate { get; set; }
    }
}
