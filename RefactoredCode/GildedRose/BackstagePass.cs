using System;

namespace GildedRose
{
    public class BackstagePass : IItem
    {
        private readonly Item item;

        public BackstagePass(Item item)
        {
            this.item = item;
        }

        public void Update()
        {
            int qualityIncrease = 1;
            if ((item.SellIn <= 10) && (item.SellIn > 5))
            {
                qualityIncrease = 2;
            }
            else if ((item.SellIn <= 5) && (item.SellIn > 0))
            {
                qualityIncrease = 3;
            }
            else if (item.SellIn <= 0)
                qualityIncrease = -item.Quality;

            item.Quality = Math.Min(50, item.Quality + qualityIncrease);
            item.SellIn -= 1;
        }
    }
}
