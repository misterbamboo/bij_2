using System;
using System.Collections.Generic;

namespace Assets.SharedKernel.Models
{
    public interface IHasRarity
    {
        int Rarity { get; }
    }

    public class RarityCollection<T> where T : IHasRarity
    {
        private class Element
        {
            public int AccumulatedRarity { get; set; }
            public T InnerElement { get; set; }
        }

        private readonly List<Element> _items;

        private readonly int _accumulatedRarity;

        private readonly Random _rand = new();

        public RarityCollection(IEnumerable<T> items)
        {
            var sortedItems = new List<T>(items);
            sortedItems.Sort((a, b) => a.Rarity - b.Rarity);

            _items = new List<Element>();

            _accumulatedRarity = 0;
            foreach (var item in sortedItems)
            {
                _accumulatedRarity += item.Rarity;
                _items.Add(new Element
                {
                    AccumulatedRarity = _accumulatedRarity,
                    InnerElement = item,
                });
            }
        }
        
        public T? PickRandom()
        {
            var pickedRarity = _rand.Next(_accumulatedRarity);

            foreach (var item in _items)
                if (item.AccumulatedRarity >= pickedRarity)
                    return item.InnerElement;

            return default(T);
        }
    }
}