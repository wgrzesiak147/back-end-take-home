namespace FeatureFlags.Domain.Entities
{
    public class FeatureFlag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<EnvironmentState> Environments { get; set; } = new();
    }
}
