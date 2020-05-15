namespace YoutubeDlGui.Extensions
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// Defines the <see cref="DictionaryExtensions" />
    /// </summary>
    public static class DictionaryExtensions
    {
        #region Methods

        /// <summary>
        /// The Get
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary<see cref="Dictionary{TKey, TValue}"/></param>
        /// <param name="key">The key<see cref="TKey"/></param>
        /// <param name="defaultValue">The defaultValue<see cref="TValue"/></param>
        /// <returns>The <see cref="TValue"/></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, defaultValue);
            }

            return dictionary[key];
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary<see cref="OrderedDictionary"/></param>
        /// <param name="key">The key<see cref="object"/></param>
        /// <param name="defaultValue">The defaultValue<see cref="object"/></param>
        /// <returns>The <see cref="TValue"/></returns>
        public static TValue Get<TValue>(this OrderedDictionary dictionary, object key, object defaultValue)
        {
            if (!dictionary.Contains(key))
            {
                dictionary.Add(key, defaultValue);
            }

            return (TValue)dictionary[key];
        }

        /// <summary>
        /// The Put
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary<see cref="Dictionary{TKey, TValue}"/></param>
        /// <param name="key">The key<see cref="TKey"/></param>
        /// <param name="value">The value<see cref="TValue"/></param>
        public static void Put<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// The Put
        /// </summary>
        /// <param name="dictionary">The dictionary<see cref="OrderedDictionary"/></param>
        /// <param name="key">The key<see cref="object"/></param>
        /// <param name="value">The value<see cref="object"/></param>
        public static void Put(this OrderedDictionary dictionary, object key, object value)
        {
            if (dictionary.Contains(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        #endregion Methods
    }
}