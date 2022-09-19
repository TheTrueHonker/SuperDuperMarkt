using NUnit.Framework;
using SuperDuperMarkt.Data.Products;
using System;

namespace SuperDuperMarktTests
{
    public class WineTests
    {
        Wine wine;

        [SetUp]
        public void Setup()
        {
            wine = new Wine("Red Wine", 30f, 40, new DateTime(2022,9,1));
        }

        [Test]
        public void QualityTest()
        {
            Assert.AreEqual(40, wine.Quality);
            wine.UpdateQuality(new DateTime(2022, 9, 21));
            Assert.AreEqual(42, wine.Quality);
            wine.UpdateQuality(new DateTime(2023, 9, 21));
            Assert.AreEqual(50, wine.Quality);

        }

        [Test]
        public void PriceTest()
        {
            var price = wine.FixPrice;
            Assert.AreEqual(price, wine.GetPrice());
            wine.UpdateQuality(new DateTime(2023, 9, 21));
            Assert.AreEqual(price, wine.GetPrice());
        }

        [Test]
        public void ToStringTest()
        {
            Assert.AreEqual("Type: Wine, Description: Red Wine, Quality: 40, Price: 30,00€", wine.ToString());
        }
    }
}