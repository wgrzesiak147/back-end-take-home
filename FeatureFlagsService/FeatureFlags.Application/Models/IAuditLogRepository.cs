using FeatureFlags.Domain.Entities;

namespace FeatureFlags.Application.Models
{

    public interface IAuditLogRepository
    {
        Task AddAsync(FeatureFlagAudit auditEntry);
        Task<IEnumerable<FeatureFlagAudit>> GetByFlagIdAsync(Guid flagId);
    }
}
