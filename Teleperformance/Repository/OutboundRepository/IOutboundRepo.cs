using System;
using Teleperformance.Model;

namespace Teleperformance.Repository.OutboundRepository
{
    public interface IOutboundRepo : IDisposable
    {
        bool AddOutboundTags(Outbound outbound);
        void UpdateOutbound(Outbound outbound);
    }
}
