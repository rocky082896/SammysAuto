using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using Teleperformance.Data;
using Teleperformance.Model;
using Teleperformance.Repository.InboundRepo;

namespace Teleperformance.Repository.InboundRepository
{
    public class InboundRepo : IInboundRepo
    {
        internal TeleContext context;
        internal Inbound inbound;

        public InboundRepo()
        {
            context = new TeleContext();
        }

        public bool AddInboundTags(Inbound _inbound)
        {
            inbound = _inbound;
            context.Inbounds.Add(inbound);
            return context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void UpdateInbound(Inbound inbound)
        {
            try
            {
                context.Inbounds.AddOrUpdate(inbound);
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
    }
}
