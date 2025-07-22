namespace FeatureFlags.Domain.Entities
{
    public class FeatureFlagAudit
    {
        public Guid Id { get; set; }
        public Guid FeatureFlagId { get; set; }
        public string ChangeType { get; set; } = string.Empty; // e.g. "Create", "Update", "Delete"
        public string? EnvironmentName { get; set; } // Optional for Delete
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public FeatureFlag FeatureFlag { get; set; } = null!;
    }
}
