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
            var existingWalks = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalks == null)
            {
                return null;
            }
            _context.Walks.Remove(existingWalks);
            await _context.SaveChangesAsync();
            return existingWalks;
        }

        public async Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 3)
        {
            var walks = _context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //walks = walks.OrderBy(x => x.Name);
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Description) : walks.OrderByDescending(x => x.Description);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize; // 6 -1 = 5 * 3 = 15
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walks?> GetById(Guid id)
        {
            return await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks?> UpdateAsync(Guid id, Walks walk)
        {
            var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
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
