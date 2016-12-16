using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using GR;

namespace SpecFlowPractice
{
    [Binding]
    public class GildedRoseSteps
    {
        private GildedRose _gildedRose;
        private List<Item> _originalItems;

        [Given(@"I have the following items in stock")]
        public void GivenIHaveTheFollowingItemsInStock(List<Item> items)
        {
            _originalItems = items;
            _gildedRose = new GildedRose(_originalItems);
        }

        [When(@"(.*) day\(s\) pass")]
        public void WhenDayPass(int daysNumber)
        {
            for (int i = 0; i < daysNumber; i++)
            {
                _gildedRose.UpdateQuality();
            }
        }

        [Then(@"I have the following items in the stock")]
        public void ThenIHaveTheFollowingItemsInTheStock(List<Item> items)
        {
            items
                .Zip(_originalItems, (expected, actual) => 
                    expected.Name == actual.Name 
                    && expected.Quality == actual.Quality 
                    && expected.SellIn == actual.SellIn)
                .All(t => t)
                .Should()
                .BeTrue();
        }

        private Item _currentItem;

        [Given(@"I have item (.*) with quality (.*) and Sell In days (.*)")]
        public void GivenIHaveItemItemWithQualityAndSellInDays(string name, int quality, int sellIn)
        {
            _currentItem = new Item { Name = name, Quality = quality, SellIn = sellIn};
            _gildedRose = new GildedRose(new List<Item> { _currentItem});
        }

        [Then(@"I get quality (.*)")]
        public void ThenIGetQuality(int resultQuality)
        {
            _currentItem.Quality.Should().Be(resultQuality);
        }


        [StepArgumentTransformation]
        public List<Item> GetItemsFromTable(Table items)
        {
            return items.CreateSet<Item>().ToList();
        }

    }
}
