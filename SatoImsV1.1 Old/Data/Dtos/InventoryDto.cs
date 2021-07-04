using System;

namespace SatoImsV1._1.Data.Dtos
{
    public class InventoryDto
    {
        public string poNumber { get; set; }
        public string item_no { get; set; }
        public string item_desc { get; set; }
        public string serial_no { get; set; }
        public string category { get; set; }
        public string group { get; set; }
        public string uom { get; set; }
        public string currency { get; set; }
        public string image_source { get; set; }
        public int qty_in { get; set; }
        public int current_qty { get; set; }
        public double amount { get; set; }
        public double total_amount { get; set; }
        public DateTime date_received { get; set; }
        public int aging { get; set; }

    }
}
