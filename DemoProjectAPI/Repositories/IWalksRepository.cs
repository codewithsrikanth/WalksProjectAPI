using DemoProjectAPI.Models.Domain;

namespace DemoProjectAPI.Repositories
{
    public interface IWalksRepository
    {
        Task<Walks> CreateAsync(Walks walks);
        Task<List<Walks>> GetAllAsync();
        Task<Walks?> GetById(Guid id);

        Task<Walks?> UpdateAsync(Guid id,Walks walk);

        Task<Walks?>  DeleteAsync(Guid id);
    }
}
