using System;


namespace SatoImsV1._1.Data.Dtos
{
    public class OutgoingDataDto
    {

        public string outgoingSeries { get; set; }
        //public int status_header { get; set; }
        public int outgoingItemQty { get; set; }
        public string currency { get; set; }
        //public string ponumber { get; set; }
        public string itemnumber { get; set; }
        public string receivingid { get; set; }
        public double price { get; set; }
        public double itemamount { get; set; }
        public string customer { get; set; }
        //public int status_details { get; set; }
        public DateTime? deliverydate { get; set; }

    }
}
