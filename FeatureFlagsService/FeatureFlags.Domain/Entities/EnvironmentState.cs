namespace FeatureFlags.Domain.Entities
{
    public class EnvironmentState
    {
        public Guid Id { get; set; }
        public string EnvironmentName { get; set; } = null!;
        public bool IsActive { get; set; }
        public int RolloutPercentage { get; set; }
        public DateTime LastUpdated { get; set; }

        public Guid FeatureFlagId { get; set; }
        public FeatureFlag FeatureFlag { get; set; } = null!;
    }
}
