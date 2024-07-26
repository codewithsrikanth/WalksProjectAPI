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

        public async Task<Walks?> DeleteAsync(Guid id)
        {
            var existingWalks = await _context.Walks.FirstOrDefaultAsync(x=>x.Id == id);
            if(existingWalks == null)
            {
                return null;
            }
            _context.Walks.Remove(existingWalks);
            await _context.SaveChangesAsync();
            return existingWalks;
        }

        public async Task<List<Walks>> GetAllAsync()
        {
            return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walks?> GetById(Guid id)
        {
            return await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks?> UpdateAsync(Guid id, Walks walk)
        {
            var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingWalk == null)
            {
                return null;
            }
            existingWalk.Difficulty = walk.Difficulty;
            existingWalk.Name = walk.Name;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.Description = walk.Description;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            await _context.SaveChangesAsync();
            return existingWalk;
        }
    }
}
