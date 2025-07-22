using FeatureFlags.Domain.Entities;

namespace FeatureFlags.Application.Models
{
    public interface IFeatureFlagRepository
    {
        Task<bool> ExistsAsync(string name);
        Task<FeatureFlag?> GetByNameAsync(string name);
        Task<IEnumerable<FeatureFlag>> GetAllAsync();
        Task AddAsync(FeatureFlag flag);
        Task DeleteAsync(FeatureFlag flag);
        Task SaveChangesAsync();
    }
}
