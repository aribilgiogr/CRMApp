namespace Utilities.Extensions
{
    public static class AsyncLinqExtensions
    {
        public static async Task<IEnumerable<T>> ToEnumerableAsync<T>(this IAsyncEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var data = new List<T>();

            await foreach (var item in source)
            {
                data.Add(item);
            }

            return data;
        }
    }
}
