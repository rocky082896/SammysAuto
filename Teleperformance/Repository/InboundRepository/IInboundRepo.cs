using System;
using Teleperformance.Model;

namespace Teleperformance.Repository.InboundRepo
{
    public interface IInboundRepo : IDisposable
    {
        bool AddInboundTags(Inbound inbound);
        void UpdateInbound(Inbound inbound);
    }
}
