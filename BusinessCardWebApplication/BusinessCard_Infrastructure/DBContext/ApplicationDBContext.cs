using BusinessCard_Core.Models.Entites;
using BusinessCard_Core.Models.EntityConfigurationes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard_Infrastructure.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BusinessCardEntityTypeConfiguration());
        }

        public virtual DbSet<BusinessCard> BusinessCards { get; set; }
    }
}
