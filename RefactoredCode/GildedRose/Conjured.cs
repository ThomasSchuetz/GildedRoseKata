using System;

namespace GildedRose
{
    public class Conjured : IItem
    {
        private readonly Item item;

        public Conjured(Item item)
        {
            this.item = item;
        }

        public void Update()
        {
            int loss = 2;
            if (item.SellIn <= 0)
            {
                loss = 4;
            }

            item.SellIn -= 1;
            item.Quality = Math.Max(0, item.Quality - loss);
        }
    }
}
