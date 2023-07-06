using NZWalksApi.DataAcessLayer.Models.Domains;

namespace NZWalksApi.DataAcessLayer.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();
        Task<Region?> GetByIdAsync(Guid id);


    }
}
