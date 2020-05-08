using System.Collections.Generic;

namespace PizzaGame.Extensions
{
    /// <summary>
    /// Provides helper functions to implementations of <c>IDictionary</c>
    /// objects
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Add a key, value to a dictionary of the key does not exist in the
        /// dictionary
        /// </summary>
        /// <typeparam name="TKey">type of value</typeparam>
        /// <typeparam name="TValue">type of key</typeparam>
        /// <param name="dictionary">the dictionary to operate on</param>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public static void TryAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Remove a key and its associated value if the key exists in the
        /// dictionary
        /// </summary>
        /// <typeparam name="TKey">the type of key</typeparam>
        /// <typeparam name="TValue">the type of interest</typeparam>
        /// <param name="dictionary">the dictionary to operate on</param>
        /// <param name="key">the key</param>
        public static void TryRemove<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }
        }
    }
}