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
                "Backstage passes to a TAFKAL80ETC concert" => new BackstagePass(item),
                _ => throw new NotImplementedException($"Unknown item: {item.Name}"),
            };
        }
    }
}
