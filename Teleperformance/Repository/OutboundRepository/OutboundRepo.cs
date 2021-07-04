using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using Teleperformance.Data;
using Teleperformance.Model;

namespace Teleperformance.Repository.OutboundRepository
{
    public class OutboundRepo : IOutboundRepo
    {
        internal TeleContext context;
        internal Outbound outbound;

        public OutboundRepo()
        {
            context = new TeleContext();
        }
        public bool AddOutboundTags(Outbound _outbound)
        {
            outbound = _outbound;
            context.Outbounds.Add(outbound);
            return context.SaveChanges() > 0;
        }

        public void UpdateOutbound(Outbound outbound)
        {
            try
            {
                context.Outbounds.AddOrUpdate(outbound);
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
