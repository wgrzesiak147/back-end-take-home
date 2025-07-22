using FeatureFlags.Application.Models;

namespace FeatureFlags.Application.Services
{
    public interface IFeatureFlagService
    {
        Task<bool> CreateFeatureFlagAsync(CreateFeatureFlagRequest request);
        Task<IEnumerable<FeatureFlagDto>> GetAllFlagsAsync();
        Task<FeatureFlagDto?> GetFlagAsync(string name);
        Task<bool> DeleteFlagAsync(string name);
        Task<IEnumerable<AuditLogEntryDto>> GetAuditLogAsync(string name);
        Task<bool> UpdateEnvironmentStateAsync(string name, string environment, UpdateEnvironmentStateRequest request);
        Task<bool> IsFeatureActiveAsync(string name, string environment, string userId);
    }
}
