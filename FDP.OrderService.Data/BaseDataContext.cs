using System.Data.Common;
using System.Data.Entity;

namespace FDP.OrderService.Data
{
    
        public abstract class BaseDataContext : DbContext
        {
            protected class DatabaseLogger
            {
                public static void Log(string logInfo)
                {
                    System.Diagnostics.Debug.WriteLine(logInfo);
                }
            }

        public bool IsDisposed { get; private set; }
        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
        }

        public BaseDataContext(DbConnection dbConnection, bool contextOwnsConnection)
                : base(dbConnection, contextOwnsConnection)
            {
                //Database.Log = logInfo => DatabaseLogger.Log(logInfo);
            }

            public BaseDataContext(string connectionStringName)
                : base(connectionStringName)
            {
                //Database.Log = logInfo => DatabaseLogger.Log(logInfo);
            }

            public void InitializeDatabase()
            {
                this.Database.Initialize(false);
            }

            #region DbContext override

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                //
                // entity configuration samples by EF fluent API
                //

                /*// Order
                modelBuilder.Entity<Order>()
                   .HasRequired(q => q.ShippingCarrier);
                modelBuilder.Entity<Order>()
                    .HasRequired(q => q.Shop);
                modelBuilder.Entity<Order>()
                    .HasRequired(q => q.Software);*/
            }

            // 
            // validation rule samples
            //
            /*protected override DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
            {
                var result = base.ValidateEntity(entityEntry, items);

                // Order
                if (entityEntry.Entity is Order && (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified))
                {
                    Order order = entityEntry.Entity as Order;

                    if (order.ShippingCarrier == null)
                    {
                        result.ValidationErrors.Add(new DbValidationError("ShippingCarrier", "The ShippingCarrier field is required."));
                    }

                    if (order.Shop == null)
                    {
                        result.ValidationErrors.Add(new DbValidationError("Shop", "The Shop field is required."));
                    }

                    if (order.Software == null)
                    {
                        result.ValidationErrors.Add(new DbValidationError("Software", "The Software field is required."));
                    }
                }

                return result;
            }*/

            #endregion
        }
    
}
