using NUnit.Framework;
using SuperDuperMarkt.Data.Products;
using System;

namespace SuperDuperMarktTests
{
    public class CheeseTests
    {
        private Cheese badCheese;
        private Cheese goodCheese;

        [SetUp]
        public void Setup()
        {
            badCheese = new Cheese("Cheddar", 2.5f, 29, 60);
            goodCheese = new Cheese("Cheddar", 2.5f, 30, 50, new DateTime(2022, 9, 1));
        }

        [Test]
        public void QualityTest()
        {
            Assert.IsFalse(badCheese.IsQualityGood());
            Assert.IsTrue(goodCheese.IsQualityGood());
            goodCheese.UpdateQuality(new DateTime(2022, 9, 2));
            Assert.IsFalse(goodCheese.IsQualityGood());
            Assert.AreEqual(29, goodCheese.Quality);
        }

        [Test]
        public void PriceTest()
        {
            Assert.AreEqual(MathF.Round(badCheese.FixPrice + 0.1f * badCheese.Quality, 2), badCheese.GetPrice());
        }

        [Test]
        public void ToStringTest()
        {
            Assert.AreEqual("Type: Cheese, Description: Cheddar, Quality: 30, Due Date: 21.10.2022, Price: 5,50€", goodCheese.ToString());
        }
    }
}