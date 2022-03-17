using Microsoft.EntityFrameworkCore;
using System;

namespace SmartAC.Context.Sql
{
    public class SmartACDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("ConnectionString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
