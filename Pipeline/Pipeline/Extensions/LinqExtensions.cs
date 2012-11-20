namespace Pipeline.Model.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LinqExtensions
    {
        public static IList<T> Materialise<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.ToList();
        }

        public static void Each<T>(this IEnumerable<T> source, Action<T> actOnEach)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (actOnEach == null)
                throw new ArgumentNullException("actOnEach");

            foreach (var item in source)
            {
                actOnEach(item);
            }
        }
    }
}