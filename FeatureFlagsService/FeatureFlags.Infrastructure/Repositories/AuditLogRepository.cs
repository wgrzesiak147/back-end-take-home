using FeatureFlags.Application.Models;
using FeatureFlags.Domain.Entities;
using FeatureFlags.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlags.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FeatureFlagAudit auditEntry)
        {
            await _context.AuditLogs.AddAsync(auditEntry);
        }

        public async Task<IEnumerable<FeatureFlagAudit>> GetByFlagIdAsync(Guid flagId)
        {
            return await _context.AuditLogs
                .Where(a => a.FeatureFlagId == flagId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }
    }
}
