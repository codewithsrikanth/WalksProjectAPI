using DemoProjectAPI.Models.Domain;

namespace DemoProjectAPI.Repositories
{
    public interface IWalksRepository
    {
        Task<Walks> CreateAsync(Walks walks);
        Task<List<Walks>> GetAllAsync();
    }
}
