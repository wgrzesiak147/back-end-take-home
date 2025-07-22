using FeatureFlags.Application.Models;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlags.Infrastructure.Repositories
{
    public class FeatureFlagRepository : IFeatureFlagRepository
    {
        private readonly AppDbContext _context;

        public FeatureFlagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FeatureFlag flag)
        {
            await _context.FeatureFlags.AddAsync(flag);
        }

        public async Task DeleteAsync(FeatureFlag flag)
        {
            _context.FeatureFlags.Remove(flag);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.FeatureFlags.AnyAsync(f => f.Name == name);
        }

        public async Task<IEnumerable<FeatureFlag>> GetAllAsync()
        {
            return await _context.FeatureFlags
                .Include(f => f.Environments)
                .ToListAsync();
        }

        public async Task<FeatureFlag?> GetByNameAsync(string name)
        {
            return await _context.FeatureFlags
                .Include(f => f.Environments)
                .FirstOrDefaultAsync(f => f.Name == name);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
