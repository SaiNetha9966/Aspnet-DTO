using Microsoft.EntityFrameworkCore;
using NZWalksApi.DataAcessLayer.Data;
using NZWalksApi.DataAcessLayer.Models.Domains;

namespace NZWalksApi.DataAcessLayer.Repositories
{
    public class DbRegionRepository : IRegionRepository
    {
        //private readonly NZWalksDbContext dbContext;

        public DbRegionRepository(NZWalksDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public NZWalksDbContext DbContext { get; }

        public async Task<List<Region>> GetAllRegionAsync()
        {
            return await DbContext.regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await DbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
