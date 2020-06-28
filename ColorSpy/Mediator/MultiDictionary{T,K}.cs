using System;
using System.Collections.Generic;

namespace ColorSpy.Mediator
{
    /// <summary>
    /// A Helper class for the mediator.
    /// </summary>
    /// <typeparam name="T">The key.</typeparam>
    /// <typeparam name="K">The item.</typeparam>
    public class MultiDictionary<T, K> : Dictionary<T, List<K>>
    {
        /// <summary>
        /// Add a new item the dictionary.
        /// </summary>
        /// <param name="key">The key of the new item to add.</param>
        /// <param name="newItem">The new item.</param>
        public void AddValue(T key, K newItem)
        {
            EnsureKey(key);
            this[key].Add(newItem);
        }

        /// <summary>
        /// Add multiple items to the dictionary.
        /// </summary>
        /// <param name="key">The key to add the new items to.</param>
        /// <param name="newItems">The new item to add.</param>
        public void AddValues(T key, IEnumerable<K> newItems)
        {
            EnsureKey(key);
            this[key].AddRange(newItems);
        }

        /// <summary>
        /// Remove a value from the dictionary.
        /// </summary>
        /// <param name="key">The key the value exists at.</param>
        /// <param name="value">The value to remove.</param>
        /// <returns>True is remove all was successful false if not.</returns>
        public bool RemoveValue(T key, K value)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            this[key].Remove(value);

            if (this[key].Count == 0)
            {
                Remove(key);
            }

            return true;
        }

        /// <summary>
        /// Remove all of a specific value at a key.
        /// </summary>
        /// <param name="key">The key the values exist at.</param>
        /// <param name="match">The match to look for.</param>
        /// <returns>true if the remove was successful.</returns>
        public bool RemoveAllValue(T key, Predicate<K> match)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            this[key].RemoveAll(match);

            if (this[key].Count == 0)
            {
                Remove(key);
            }

            return true;
        }

        /// <summary>
        /// Ensure that the key has been added to the dictionary.
        /// </summary>
        /// <param name="key">The key to check.</param>
        private void EnsureKey(T key)
        {
            if (!ContainsKey(key))
            {
                this[key] = new List<K>(1);
            }
            else
            {
                if (this[key] == null)
                {
                    this[key] = new List<K>(1);
                }
            }
        }
    }
}