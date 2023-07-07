using Microsoft.EntityFrameworkCore;
using NZWalksApi.DataAcessLayer.Models.Domains;

namespace NZWalksApi.DataAcessLayer.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        // Theese three proporties create a new data base with three tables
        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }
    }
}
