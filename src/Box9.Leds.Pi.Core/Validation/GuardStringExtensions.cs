namespace Box9.Leds.Pi.Core.Validation
{
    public static class GuardStringExtensions
    {
        public static GuardThis<string> AgainstNullOrEmpty(this GuardThis<string> guard, string errorMessage)
        {
            return guard.WithRule(val => !string.IsNullOrEmpty(val), errorMessage);
        }
    }
}
