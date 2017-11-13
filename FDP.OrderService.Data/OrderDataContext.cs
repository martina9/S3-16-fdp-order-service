using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using FDP.OrderService.Data.Migrations;
using FDP.OrderService.Data.Model;
using FDP.OrderService.Data.Utilities;

namespace FDP.OrderService.Data
{
    public class OrderDataContext : BaseDataContext
        {
            public OrderDataContext(DbConnection dbConnection, bool contextOwnsConnection)
                : base(dbConnection, contextOwnsConnection)
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<OrderDataContext, FDP.OrderService.Data.Migrations.Configuration>(true));

                this.SessionID = Guid.NewGuid(); 
            }

            public OrderDataContext() : base("OrderServiceDataContext")
            {
                this.SessionID = Guid.NewGuid();

                this.Database.Connection.Open();

                //System.Diagnostics.Debug.WriteLine("New DocumentPrintServiceDataContext ... " + this.SessionID);
            }

            public Guid SessionID { get; private set; }

            public virtual DbSet<User> Users { get; set; }
            public virtual DbSet<Order> Orders { get; set; }
            public virtual DbSet<Restaurant> Restaurants { get; set; }
            public virtual DbSet<Product> Products { get; set; }

        #region DbContext override

      

        public override int SaveChanges()
            {
                try
                {
                    return base.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.Stringify());
                }
            }

            public override System.Threading.Tasks.Task<int> SaveChangesAsync()
            {
                try
                {
                    return base.SaveChangesAsync();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.Stringify());
                }
            }

            public override System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
            {
                try
                {
                    return base.SaveChangesAsync(cancellationToken);
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.Stringify());
                }
            }

            #endregion
        }
    }

