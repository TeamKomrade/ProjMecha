using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProjMecha.DL;
using ProjMecha.DL.Company;
using ProjMecha.DL.People;
using ProjMecha.DL.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DAL
{
    public class MechanicDatabase : DbContext
    {
        public MechanicDatabase()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = "ec2-79-125-59-247.eu-west-1.compute.amazonaws.com",
                Port = 5432,
                Username = "gxrihaudxynlkr",
                Password = "d05b08f67d4def122cd5e58bc9dce1b1e49fbfc8ec61ce5a6f015a567bd9ee15",
                Database = "d7bdqfnlevg1tp",
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            optionsBuilder.UseNpgsql(builder.ToString());

            base.OnConfiguring(optionsBuilder);
        }
        #region People
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<PersonalDataPair> PersonalDataPairs { get; set; }
        #endregion

        #region Store
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AttributeStoreItemPair> AttributeStoreItemPairs { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        #endregion
        public DbSet<CompanyDetails> CompanyDetails { get; set; }


        public delegate void ActionsHandler(string info);
        public event ActionsHandler NotifyAboutAction;
        public void OnNotifyAboutAction(string info)
        {
            NotifyAboutAction?.Invoke(info);
        }

        public void FillDefaultInfo()
        {
            if (Database.EnsureCreated())
            {
                object creds = new Credentials()
                {
                    Login = "admin",
                    Password = "admin",
                    AccountType = AccountType.Admin,
                    FullName = "ADMIN"
                };
                this.TryAction(DatabaseActions.ADD, creds);

                object creds2 = new Credentials()
                {
                    Login = "admin",
                    Password = "employee",
                    AccountType = AccountType.Employee,
                    FullName = "EMPLOYEE"
                };
                this.TryAction(DatabaseActions.ADD, creds2);
                ((Credentials)creds2).Login = "test_update";
                this.TryAction(DatabaseActions.UPDATE, creds2);
            }
        }
    }
}
