using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GICApp.ApplicationCore.Domain;
using GICApp.ApplicationCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GICApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {

        //public AppDbContext()
        //{


        //}

        public AppDbContext( )  
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GIC;Trusted_Connection=True;");
        }


        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<InterestRule> InterestRules { get; set; }


        public DbSet<TransactionHistory> TransactionHistories { get; set; }



    }
}
