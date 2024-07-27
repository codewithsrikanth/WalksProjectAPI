using DemoProjectAPI.Models.Domain;

namespace DemoProjectAPI.Repositories
{
    public interface IWalksRepository
    {
        Task<Walks> CreateAsync(Walks walks);
        Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 3);
        Task<Walks?> GetById(Guid id);

        Task<Walks?> UpdateAsync(Guid id, Walks walk);

        Task<Walks?> DeleteAsync(Guid id);
    }
}
