using Xunit;
using System.Collections.Generic;
using GildedRose;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Theory]
        [InlineData("RegularItem", 0, 1, -1, 0)]
        [InlineData("RegularItem", 0, 25, -1, 23)]
        [InlineData("RegularItem", 3, 25, 2, 24)]
        [InlineData("Aged Brie", 7, 25, 6, 26)]
        [InlineData("Aged Brie", -1, 25, -2, 27)] // Brie become more valueable (x2) after sellIn date
        [InlineData("Sulfuras, Hand of Ragnaros", 5, 80, 5, 80)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 20, 22, 19, 23)] // Backstage passes become more valuable
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 7, 9, 9)] // Backstage passes become more valuable (x2)
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 39, 4, 42)] // Backstage passes become more valuable (x3)
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 20, 0, 23)] // Backstage passes become more valuable (x3)
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 39, -1, 0)] // Backstage passes are worthless after sellIn
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 4, 49, 3, 50)] // Maximum quality of 50
        [InlineData("Conjured Mana Cake", 2, 4, 1, 2)] // Conjured decreases twice as fast in value
        [InlineData("Conjured Mana Cake", 0, 5, -1, 1)] // Conjured decreases twice as fast in value
        public void Item_unit_tests(string name, int sellIn, int quality, int sellInExpected, int qualityExpected)
        {
            var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
            var app = new GildedRose.GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(qualityExpected, items[0].Quality);
            Assert.Equal(sellInExpected, items[0].SellIn);
        }


        [Fact]
        public void Result_after_1_day()
        {
            var items = GetBasicItems();
            var app = new GildedRose.GildedRose(items);

            var expected = new List<Item>{
                 new Item {Name = "+5 Dexterity Vest", SellIn = 9, Quality = 19},
                 new Item {Name = "Aged Brie", SellIn = 1, Quality = 1},
                 new Item {Name = "Elixir of the Mongoose", SellIn = 4, Quality = 6},
                 new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                 new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = 14,
                     Quality = 21
                 },
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = 9,
                     Quality = 50
                 },
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = 4,
                     Quality = 50
                 },
                 new Item { Name = "Conjured Mana Cake", SellIn = 2, Quality = 4 }
             };

            app.UpdateQuality();

            Assert.True(CheckItems(items, expected));

        }

        [Fact]
        public void Result_after_30_days()
        {
            var items = GetBasicItems();
            var app = new GildedRose.GildedRose(items);

            var expected = new List<Item>{
                 new Item {Name = "+5 Dexterity Vest", SellIn = -20, Quality = 0},
                 new Item {Name = "Aged Brie", SellIn = -28, Quality = 50},
                 new Item {Name = "Elixir of the Mongoose", SellIn = -25, Quality = 0},
                 new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                 new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = -15,
                     Quality = 0
                 },
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = -20,
                     Quality = 0
                 },
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = -25,
                     Quality = 0
                 },
                 new Item { Name = "Conjured Mana Cake", SellIn = -27, Quality = 0 }
             };

            for (int i = 0; i < 30; i++)
            {
                app.UpdateQuality();
            }

            Assert.True(CheckItems(items, expected));
        }

        private bool CheckItems(IList<Item> obtained, IList<Item> expected)
        {
            for (int i = 0; i < expected.Count; i++)
            {
                var obtainedItem = obtained[i];
                var expectedItem = expected[i];

                Assert.Equal(expectedItem.Name, obtainedItem.Name);
                Assert.Equal(expectedItem.SellIn, obtainedItem.SellIn);
                Assert.Equal(expectedItem.Quality, obtainedItem.Quality);
            }
            return true;
        }

        private IList<Item> GetBasicItems() =>
            new List<Item>{
                 new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                 new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                 new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                 new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                 new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = 15,
                     Quality = 20
                 },
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = 10,
                     Quality = 49
                 },
                 new Item
                 {
                     Name = "Backstage passes to a TAFKAL80ETC concert",
                     SellIn = 5,
                     Quality = 49
                 },
                 new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };
    }
}
