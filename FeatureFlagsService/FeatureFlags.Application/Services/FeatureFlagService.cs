using FeatureFlags.Application.Models;
using FeatureFlags.Domain.Entities;

namespace FeatureFlags.Application.Services;

public class FeatureFlagService : IFeatureFlagService
{
    private readonly IFeatureFlagRepository _flagRepository;
    private readonly IAuditLogRepository _auditLogRepository;

    public FeatureFlagService(
        IFeatureFlagRepository flagRepository,
        IAuditLogRepository auditLogRepository)
    {
        _flagRepository = flagRepository;
        _auditLogRepository = auditLogRepository;
    }

    public async Task<bool> CreateFeatureFlagAsync(CreateFeatureFlagRequest request)
    {
        if (await _flagRepository.ExistsAsync(request.Name))
        {
            return false;
        }

        var flag = new FeatureFlag
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Environments = new List<EnvironmentState>
            {
                new() { EnvironmentName = "Development", IsActive = false, RolloutPercentage = 0 },
                new() { EnvironmentName = "Staging", IsActive = false, RolloutPercentage = 0 },
                new() { EnvironmentName = "Production", IsActive = false, RolloutPercentage = 0 }
            }
        };

        await _flagRepository.AddAsync(flag);
        await _flagRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<FeatureFlagDto>> GetAllFlagsAsync()
    {
        var flags = await _flagRepository.GetAllAsync();
        return flags.Select(f => new FeatureFlagDto
        {
            Name = f.Name,
            Environments = f.Environments.Select(e => new EnvironmentStateDto
            {
                Environment = e.EnvironmentName,
                IsActive = e.IsActive,
                RolloutPercentage = e.RolloutPercentage
            }).ToList()
        });
    }

    public async Task<FeatureFlagDto?> GetFlagAsync(string name)
    {
        var flag = await _flagRepository.GetByNameAsync(name);
        if (flag == null)
        {
            return null;
        }

        return new FeatureFlagDto
        {
            Name = flag.Name,
            Environments = flag.Environments.Select(e => new EnvironmentStateDto
            {
                Environment = e.EnvironmentName,
                IsActive = e.IsActive,
                RolloutPercentage = e.RolloutPercentage
            }).ToList()
        };
    }

    public async Task<bool> UpdateEnvironmentStateAsync(string name, string environment, UpdateEnvironmentStateRequest request)
    {
        var flag = await _flagRepository.GetByNameAsync(name);
        if (flag == null)
        {
            return false;
        }

        var env = flag.Environments.FirstOrDefault(e => e.EnvironmentName == environment);
        if (env == null)
        {
            return false;
        }

        env.IsActive = request.IsActive;
        env.RolloutPercentage = request.RolloutPercentage;

        var audit = new FeatureFlagAudit
        {
            Id = Guid.NewGuid(),
            FeatureFlagId = flag.Id,
            EnvironmentName = environment,
            ChangeType = "Update",
            Timestamp = DateTime.UtcNow,
            Details = $"Set IsActive={request.IsActive}, Rollout={request.RolloutPercentage}%"
        };

        await _auditLogRepository.AddAsync(audit);
        await _flagRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFlagAsync(string name)
    {
        var flag = await _flagRepository.GetByNameAsync(name);
        if (flag == null)
        {
            return false;
        }

        await _flagRepository.DeleteAsync(flag);

        var audit = new FeatureFlagAudit
        {
            Id = Guid.NewGuid(),
            FeatureFlagId = flag.Id,
            ChangeType = "Delete",
            Timestamp = DateTime.UtcNow,
            Details = "Feature flag deleted"
        };

        await _auditLogRepository.AddAsync(audit);
        await _flagRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<AuditLogEntryDto>> GetAuditLogAsync(string name)
    {
        var flag = await _flagRepository.GetByNameAsync(name);
        if (flag == null)
        {
            return Enumerable.Empty<AuditLogEntryDto>();
        }

        var entries = await _auditLogRepository.GetByFlagIdAsync(flag.Id);

        return entries.Select(e => new AuditLogEntryDto
        {
            Timestamp = e.Timestamp,
            ChangeType = e.ChangeType,
            Environment = e.EnvironmentName,
            Details = e.Details
        });
    }

    public async Task<bool> IsFeatureActiveAsync(string name, string environment, string userId)
    {
        var flag = await _flagRepository.GetByNameAsync(name);
        if (flag == null)
        {
            return false;
        }

        var env = flag.Environments.FirstOrDefault(e => e.EnvironmentName == environment);
        if (env == null || !env.IsActive)
        {
            return false;
        }

        if (env.RolloutPercentage <= 0)
        {
            return false;
        }

        if (env.RolloutPercentage >= 100)
        {
            return true;
        }

        // Simulate consistent rollout using hash
        int hash = Math.Abs(userId.GetHashCode());
        int bucket = hash % 100;

        return bucket < env.RolloutPercentage;
    }
}
