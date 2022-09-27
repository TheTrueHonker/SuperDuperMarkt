using System;

namespace SuperDuperMarkt.Data.Products
{
    public class Bread: Product
    {
        private const int DAILY_QUALITY_MODIFIER = 2;

        public Bread(string description, float fixPrice, int quality, DateTime dueDate, DateTime dateCreated)
            :base(description, quality,dueDate,fixPrice,DAILY_QUALITY_MODIFIER,dateCreated)
        {
        }

        public Bread(string description, float fixPrice, int quality, DateTime dueDate)
            : base(description, quality, dueDate, fixPrice, DAILY_QUALITY_MODIFIER)
        {
        }

        public override bool IsQualityGood()
        {
            if (Quality > 0)
                return true;
            return false;
        }

        public override void CheckProduct()
        {
        }
    }
}
