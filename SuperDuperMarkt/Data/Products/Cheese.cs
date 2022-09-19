using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDuperMarkt.Data.Products
{
    public class Cheese: Product
    {
        private const string TYPE = "Cheese";
        private const int DAILY_QUALITY_MODIFIER = 1;
        private const int QUALITY_THRESHOLD = 30;

        public Cheese(string description, float fixPrice, int quality, int dueInDays, DateTime dateCreated) 
            : base(TYPE, description, quality, dueInDays, fixPrice, DAILY_QUALITY_MODIFIER, dateCreated)
        {
        }

        public Cheese(string description, float fixPrice, int quality, int dueInDays) 
            : base(TYPE, description, quality, dueInDays, fixPrice, DAILY_QUALITY_MODIFIER)
        {
        }

        public override bool IsQualityGood()
        {
            if (Quality < QUALITY_THRESHOLD)
                return false;
            return true;
        }
    }
}
