namespace AdventOfCode;

public static class DictionaryExtensions
{

    extension<TKey, TValue>(Dictionary<TKey, TValue> dst) where TKey : notnull
    {

        public void AddAll(
            Dictionary<TKey, TValue> src,
            Func<TValue, TValue> valueTransformer
        )
        {
            foreach (var srcPair in src)
            {
                if (dst.ContainsKey(srcPair.Key)) throw new ArgumentException($"dst already contains key {srcPair.Key}");
                dst[srcPair.Key] = valueTransformer(srcPair.Value);
            }
        }

        public void MergeAll(
            Dictionary<TKey, TValue> src,
            TValue defaultValue,
            Func<TKey, TValue, TValue, TValue> keyMerger
        )
        {
            foreach (var srcPair in src)
            {
                dst.Merge(srcPair.Key, srcPair.Value, defaultValue, keyMerger);
            }
        }

        public void Merge(
            TKey key,
            TValue value,
            TValue defaultValue,
            Func<TKey, TValue, TValue, TValue> keyMerger
        )
        {
            dst[key] = keyMerger(key, dst.GetValueOrDefault(key, defaultValue), value);
        }

    }

}