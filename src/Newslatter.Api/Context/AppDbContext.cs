using Microsoft.EntityFrameworkCore;
using Newslatter.Api.Models;
using Newslatter.Api.Saga;

namespace Newslatter.Api.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; } = null!;
        public DbSet<NewslatterOnboardingSagaData> SagaData { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewslatterOnboardingSagaData>().HasKey(x => x.CorrelationId);
        }
    }
}