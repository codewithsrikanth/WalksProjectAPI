using DemoProjectAPI.Data;
using DemoProjectAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectAPI.Repositories
{
    public class SQLWalksRepository : IWalksRepository
    {
        private readonly WalksDbContext _context;
        public SQLWalksRepository(WalksDbContext _context)
        {
            this._context = _context;
        }
        public async Task<Walks> CreateAsync(Walks walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walks>> GetAllAsync()
        {
            return await _context.Walks.ToListAsync();
        }
    }
}
