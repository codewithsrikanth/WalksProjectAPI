using DemoProjectAPI.Models.Domain;

namespace DemoProjectAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}
