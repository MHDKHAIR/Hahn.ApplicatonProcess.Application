namespace Hahn.ApplicatonProcess.July2021.Domain.Common
{
    public static class AppExtentions
    {
        public static bool HasValue(this string value)
        {
            value = value?.Trim();
            return !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value);
        }
    }
}
