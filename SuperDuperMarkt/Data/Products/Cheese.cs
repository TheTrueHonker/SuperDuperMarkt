using System;

namespace SuperDuperMarkt.Data.Products
{
    public class Cheese: Product
    {
        private const int DAILY_QUALITY_MODIFIER = 1;
        private const int QUALITY_THRESHOLD = 30;

        public Cheese(string description, float fixPrice, int quality, int dueInDays, DateTime dateCreated) 
            : base(description, quality, dueInDays, fixPrice, DAILY_QUALITY_MODIFIER, dateCreated)
        {
        }

        public Cheese(string description, float fixPrice, int quality, int dueInDays) 
            : base(description, quality, dueInDays, fixPrice, DAILY_QUALITY_MODIFIER)
        {
        }

        public override bool IsQualityGood()
        {
            if (Quality < QUALITY_THRESHOLD)
                return false;
            return true;
        }

        public override void CheckProduct()
        {
            if (DueInDays < 50 || DueInDays > 100)
                throw new Exception("Due In Days Parameter must be between 50 and 100 inclusive");
        }
    }
}
