using System;

namespace GildedRose
{
    public static class ItemFactory
    {
        public static IItem Create(Item item)
        {
            return item.Name switch
            {
                "Aged Brie" => new AgedBrie(item),
                "Sulfuras, Hand of Ragnaros" => new Sulfuras(item),
                _ => throw new NotImplementedException($"Unknown item: {item.Name}"),
            };
        }
    }
}
