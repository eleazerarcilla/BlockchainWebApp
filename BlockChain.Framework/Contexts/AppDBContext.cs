using BlockChain.Framework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChain.Presentation.MVC.Contexts
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<CommonItems> CommonItems { get; set; }
        public DbSet<PermittedAdditive> PermittedAdditives { get; set; }
        //Model DBsets
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CommonItems>();
            builder.Entity<PermittedAdditive>();
        }
    }
}
