using DemoProjectAPI.Data;
using DemoProjectAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WalksDbContext _dbContext;
        public SQLRegionRepository(WalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
           return await _dbContext.Regions.ToListAsync();
        }
    }
}
