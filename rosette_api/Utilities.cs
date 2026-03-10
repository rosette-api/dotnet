namespace Rosette.Api.Client;

public static class Utilities
{
    /// <summary>
    /// Compares two dictionaries for equality
    /// </summary>
    public static bool DictionaryEquals<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
    {
        if (first == null && second == null)
        {
            return true;
        }
        if (first == null || second == null)
        {
            return false;
        }
        if (first.Count != second.Count)
        {
            return false;
        }
        foreach (var kvp in first)
        {
            if (!second.TryGetValue(kvp.Key, out TValue secondValue))
            {
                return false;
            }
            if (!EqualityComparer<TValue>.Default.Equals(kvp.Value, secondValue))
            {
                return false;
            }
        }
        return true;
    }
}
