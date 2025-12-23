namespace AdventOfCode;

public static class EnumerableExtensions
{

    extension<T>(IEnumerable<T> enumerable)
    {

        public IEnumerable<T> LazyForEach(Action<T> action)
        {
            return enumerable.Select(item =>
            {
                action(item);
                return item;
            });
        }

        public IEnumerable<T> LazyForEach(Action<T, int> action)
        {
            return enumerable.Select((item, index) =>
            {
                action(item, index);
                return item;
            });
        }

    }

    extension<T>(ParallelQuery<T> parallelQuery)
    {

        public ParallelQuery<T> LazyForAll(Action<T> action)
        {
            return parallelQuery.Select(item =>
            {
                action(item);
                return item;
            });
        }

        public ParallelQuery<T> LazyForAll(Action<T, int> action)
        {
            return parallelQuery.Select((item, index) =>
            {
                action(item, index);
                return item;
            });
        }

    }

}