using System;
using System.Collections.Generic;

namespace CylSDK.Utils
{
    /// <summary>
    /// This class implements a weighted raffle system.
    /// </summary>
    /// <typeparam name="T">The type of items to be raffled.</typeparam>
    public class Raffle<T>
    {
        private readonly Dictionary<T, int> _items = new ();
        
        /// <summary>
        /// The number of items currently in the raffle.
        /// </summary>
        public int Count => _items.Count;
        
        /// <summary>
        /// Adds an item to the raffle with a specified weight.
        /// </summary>
        /// <param name="item">The item to be added to the raffle.</param>
        /// <param name="weight">The weight of the item, which determines its chance of being selected.</param>
        public void AddItem(T item, int weight)
        {
            if (weight <= 0)
            {
                throw new ArgumentException("Weight must be greater than zero.", nameof(weight));
            }

            if (!_items.TryAdd(item, weight))
            {
                _items[item] += weight;
            }
        }

        /// <summary>
        /// Removes an item from the raffle.
        /// </summary>
        /// <param name="item">The item to be removed from the raffle.</param>
        /// <exception cref="ArgumentException">Thrown if the item is not found in the raffle.</exception>
        public void RemoveItem(T item)
        {
            if (!_items.Remove(item))
            {
                throw new ArgumentException("Item not found in the raffle.", nameof(item));
            }
        }
        
        /// <summary>
        /// Picks a random winner from the raffle based on the weights of the items.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T PickWinner(bool removeWinner = false)
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("No items in the raffle.");
            }

            var totalWeight = 0;
            foreach (var weight in _items.Values)
            {
                totalWeight += weight;
            }

            T winner = default(T);
            var hasWinner = false;
            var randomValue = UnityEngine.Random.Range(0, totalWeight);
            foreach (var kvp in _items)
            {
                if (randomValue < kvp.Value)
                {
                    winner = kvp.Key;
                    hasWinner = true;
                    break;
                }
                randomValue -= kvp.Value;
            }

            if (!hasWinner)
            {
                throw new InvalidOperationException("Failed to select a random item.");
            }
            
            if (removeWinner)
            {
                RemoveItem(winner);
            }
            
            return winner;
        }

        /// <summary>
        /// Check if the raffle contains a specific item.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns>True if the item is in the raffle, otherwise false.</returns>
        public bool Contains(T item)
        {
            return _items.ContainsKey(item);
        }
    }
}