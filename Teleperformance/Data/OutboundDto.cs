namespace Teleperformance.Data
{
    public class OutboundDto
    {
        public int OutboundId { get; set; }
        public int InboundId { get; set; }
        public string TagId { get; set; }
        public string AssetNumber { get; set; }
        public string AssetDescription { get; set; }
        public string DatenTime { get; set; }
    }
}
