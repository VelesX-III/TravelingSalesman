using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman
{
    /// <summary>
    /// Contains extension methods.
    /// </summary>
    static class Extensions
    {
        /// <summary>
        /// The random number generator to use for permuting.
        /// </summary>
        public static readonly Random Random = new Random();
        /// <summary>
        /// Permutes the collection's elements randomly.
        /// </summary>
        /// <typeparam name="T">The generic type parameter of the collection.</typeparam>
        /// <param name="collection">The collection to permute.</param>
        /// <returns>A generic <see cref="System.Collections.Generic.List{T}"/> of type <typeparamref name="T"/> with its elements permuted.</returns>
        public static List<T> Permute<T>(this List<T> collection)
        {
            Dictionary<double, T> keyValuePairs = new System.Collections.Generic.Dictionary<double, T>();
            foreach (T item in collection)
            {
                keyValuePairs.Add(Random.NextDouble(), item);
            }
            List<T> results = new List<T>();
            foreach (double key in keyValuePairs.Keys.OrderBy(k => k))
            {
                results.Add(keyValuePairs[key]);
            }
            return results;
        }
        /// <summary>
        /// Permutes the collection's elements randomly.
        /// </summary>
        /// <typeparam name="T">The generic type parameter of the collection.</typeparam>
        /// <param name="collection">The collection to permute.</param>
        /// <returns>A generic <see cref="System.Collections.Generic.IEnumerable{T}"/> of type <typeparamref name="T"/> with its elements permuted.</returns>
        public static IEnumerable<T> Permute<T>(this IEnumerable<T> collection)
        {
            Dictionary<double, T> keyValuePairs = new System.Collections.Generic.Dictionary<double, T>();
            foreach (T item in collection)
            {
                keyValuePairs.Add(Random.NextDouble(), item);
            }
            T[] results = new T[collection.Count()];
            List<double> keys = keyValuePairs.Keys.OrderBy(k => k).ToList();
            for (int i = 0; i < collection.Count(); i++)
            {
                results[i] = keyValuePairs[keys[i]];
            }
            return results;
        }
        /// <summary>
        /// Permutes the elements between and including the start and end indices randomly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection to permute.</param>
        /// <param name="startIndex">The start of the range of elements to permute.</param>
        /// <param name="endIndex">The end of the range of elements to permute.</param>
        /// <returns>A generic <see cref="System.Collections.Generic.List{T}"/> of type <typeparamref name="T"/> with the specified range permuted.</returns>
        public static List<T> PermutePart<T>(this List<T> collection, int startIndex, int endIndex)
        {
            Dictionary<double, T> keyValuePairs = new System.Collections.Generic.Dictionary<double, T>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                keyValuePairs.Add(Random.NextDouble(), collection[i]);
            }
            List<T> results = new List<T>();
            for (int i = 0; i < startIndex; i++)
            {
                results.Add(collection[i]);
            }
            foreach (double key in keyValuePairs.Keys.OrderBy(k => k))
            {
                results.Add(keyValuePairs[key]);
            }
            for (int i = endIndex + 1; i < collection.Count; i++)
            {
                results.Add(collection[i]);
            }
            return results;
        }
        /// <summary>
        /// Permutes the elements between and including the start and end indices randomly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection to permute.</param>
        /// <param name="startIndex">The start of the range of elements to permute.</param>
        /// <param name="endIndex">The end of the range of elements to permute.</param>
        /// <returns>A generic <see cref="System.Collections.Generic.IEnumerable{T}"/> of type <typeparamref name="T"/> with the specified range permuted.</returns>
        public static IEnumerable<T> PermutePart<T>(this IEnumerable<T> collection, int startIndex, int endIndex)
        {
            Dictionary<double, T> keyValuePairs = new System.Collections.Generic.Dictionary<double, T>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                keyValuePairs.Add(Random.NextDouble(), collection.ElementAt(i));
            }
            T[] results = new T[collection.Count()];
            for (int i = 0; i < startIndex; i++)
            {
                results[i] = collection.ElementAt(i);
            }
            List<double> keys = keyValuePairs.Keys.OrderBy(k => k).ToList();
            for (int i = startIndex; i <= endIndex; i++)
            {
                results[i] = keyValuePairs[keys[i - startIndex]];
            }
            for (int i = endIndex + 1; i < collection.Count(); i++)
            {
                results[i] = collection.ElementAt(i);
            }
            return results;
        }
    }
}
