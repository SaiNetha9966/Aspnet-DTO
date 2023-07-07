using NZWalksApi.DataAcessLayer.Models.Domains;

namespace NZWalksApi.DataAcessLayer.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();
        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id , Region region);

        Task<Region?> DeleteAsync(Guid id) ;
    }
}
