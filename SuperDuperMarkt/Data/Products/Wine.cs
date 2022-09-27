using System;

namespace SuperDuperMarkt.Data.Products
{
    public class Wine: Product
    {
        private const int QUALITY_MODIFIER = 1;
        private const int MAX_QUALITY = 50;

        public Wine(string description, float fixPrice, int quality, DateTime dateCreated) 
            : base(description, quality, DateTime.MaxValue, fixPrice, QUALITY_MODIFIER, dateCreated)
        {
            CheckWine();
        }

        public Wine(string description, float fixPrice, int quality) 
            : base(description, quality, DateTime.MaxValue, fixPrice, QUALITY_MODIFIER)
        {
            CheckWine();
        }

        public override float GetPrice()
        {
            return FixPrice;
        }

        public override int UpdateQuality(DateTime currentDateTime)
        {
            if(currentDateTime > DateCreated)
            {
                var daysInBetween = currentDateTime.Subtract(DateCreated).Days;
                int timesQualityIncrease = daysInBetween / 10;
                Quality += timesQualityIncrease * QUALITY_MODIFIER;
                if (Quality > MAX_QUALITY)
                    Quality = MAX_QUALITY;
            }
            return Quality;
        }

        public override bool IsQualityGood()
        {
            return true;
        }

        private void CheckWine()
        {
            if (Quality < 0)
                throw new Exception("Quality for wine cannot be lower than 0!");
        }
    }
}
