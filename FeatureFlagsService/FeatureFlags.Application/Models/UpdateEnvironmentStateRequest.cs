namespace FeatureFlags.Application.Models
{
    public class UpdateEnvironmentStateRequest
    {
        public bool IsActive { get; set; }
        public int RolloutPercentage { get; set; } // 0–100
        public string ChangedBy { get; set; }
    }
}