namespace FeatureFlags.Application.Models
{
    public class EnvironmentStateDto
    {
        public string Environment { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int RolloutPercentage { get; set; }
    }
}