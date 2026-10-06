namespace Rosette.Api.Client.Models;

internal static class FieldData
{
    public static List<T> Copy<T>(IEnumerable<T> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        var values = data.ToList();
        if (values.Count == 0 || values.Any(value => value is null))
        {
            throw new ArgumentException("Field data must be non-empty and contain no null elements.", nameof(data));
        }
        return values;
    }

    public static bool Equal<T>(List<T>? left, List<T>? right) =>
        ReferenceEquals(left, right) || (left is not null && right is not null && left.SequenceEqual(right));

    public static int Hash<T>(List<T>? data)
    {
        var hash = new HashCode();
        if (data is not null)
        {
            foreach (var value in data)
            {
                hash.Add(value);
            }
        }
        return hash.ToHashCode();
    }
}
