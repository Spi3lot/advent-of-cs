namespace AdventOfCode;

public static class DictionaryExtensions
{

    public static void AddAll<TKey, TValue>(
        this Dictionary<TKey, TValue> dst,
        Dictionary<TKey, TValue> src,
        Func<TValue, TValue> valueTransformer
    ) where TKey : notnull
    {
        foreach (var srcPair in src)
        {
            if (dst.ContainsKey(srcPair.Key)) throw new ArgumentException($"dst already contains key {srcPair.Key}");
            dst[srcPair.Key] = valueTransformer(srcPair.Value);
        }
    }

    public static void MergeAll<TKey, TValue>(
        this Dictionary<TKey, TValue> dst,
        Dictionary<TKey, TValue> src,
        Func<TKey, TValue, TValue, TValue> keyMerger
    ) where TKey : notnull
    {
        foreach (var srcPair in src)
        {
            dst.Merge(srcPair.Key, srcPair.Value, keyMerger);
        }
    }

    public static void Merge<TKey, TValue>(
        this Dictionary<TKey, TValue> dst,
        TKey key,
        TValue value,
        Func<TKey, TValue, TValue, TValue> keyMerger
    ) where TKey : notnull
    {
        if (!dst.TryGetValue(key, out var dstValue)) dst[key] = value;
        else dst[key] = keyMerger(key, dstValue, value);
    }

}