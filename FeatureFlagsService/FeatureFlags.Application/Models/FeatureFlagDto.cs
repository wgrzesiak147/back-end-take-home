namespace FeatureFlags.Application.Models
{
    public class FeatureFlagDto
    {
        public string Name { get; set; } = null!;
        public List<EnvironmentStateDto> Environments { get; set; } = new();
    }
}