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

        public async Task<Region> CreateAsync(Region region)
        {
          await DbContext.regions.AddAsync(region);
            await DbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingDomainModel = await DbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDomainModel == null)
            {
                return null;
            }

            DbContext.regions.Remove(existingDomainModel);
            await DbContext.SaveChangesAsync();
            return existingDomainModel;
            
        }

        public async Task<List<Region>> GetAllRegionAsync()
        {
            return await DbContext.regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await DbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await DbContext.regions.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;  
            existingRegion.RegionImageUrl   = region.RegionImageUrl;

            await DbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
