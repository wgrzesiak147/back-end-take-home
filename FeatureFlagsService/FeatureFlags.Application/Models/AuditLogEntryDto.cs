namespace FeatureFlags.Application.Models
{
    public class AuditLogEntryDto
    {
        public DateTime Timestamp { get; set; }
        public string ChangeType { get; set; } = string.Empty;
        public string? Environment { get; set; }
        public string Details { get; set; } = string.Empty;
    }
}