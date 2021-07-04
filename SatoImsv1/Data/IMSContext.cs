using SatoImsv1.Model;
using System.Data.Entity;

namespace SatoImsv1.Data
{
    public class IMSContext : DbContext
    {
        public IMSContext() : base("default")
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dispatch> Dispatches { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ItemMaster> ItemMasters { get; set; }
        public DbSet<ItemStatus> ItemStatuses { get; set; }
        public DbSet<Print> Prints { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItems> PurchaseOrderItems { get; set; }
        public DbSet<Receiving> Receiving { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
