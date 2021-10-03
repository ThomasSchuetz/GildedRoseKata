using System;

namespace GildedRose
{
    public class AgedBrie : IItem
    {
        private readonly Item item;

        public AgedBrie(Item item) => this.item = item;

        public void Update()
        {
            item.SellIn -= 1;

            int qualityIncrease = item.SellIn < 0 ? 2 : 1;
            item.Quality = Math.Min(50, item.Quality + qualityIncrease);
        }
    }
}
