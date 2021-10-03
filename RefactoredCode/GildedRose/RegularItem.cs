using System;

namespace GildedRose
{
    public class RegularItem : IItem
    {
        private readonly Item item;

        public RegularItem(Item item)
        {
            this.item = item;
        }
        public void Update()
        {
            int loss = 1;
            if (item.SellIn <= 0)
            {
                loss = 2;
            }

            item.SellIn -= 1;
            item.Quality = Math.Max(0, item.Quality - loss);
        }
    }
}