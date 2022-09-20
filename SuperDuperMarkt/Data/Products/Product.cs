using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDuperMarkt.Data.Products
{
    public abstract class Product
    {
        private const float QUALITY_PRICE_MODIFIER = 0.1f;

        public string Type { get; protected set; }
        public string Description { get; protected set; }
        public int Quality { get; protected set; }
        public DateTime DueDate { get; protected set; }
        public float FixPrice { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        private int dailyQualityModifier;
        private int startingQuality;
        

        protected Product(string type, string description, int quality, DateTime dueDate, float fixPrice, int dailyQualityModifier, DateTime dateCreated)
        {
            Type = type;
            Description = description;
            Quality = quality;
            startingQuality = quality;
            DueDate = dueDate;
            FixPrice = fixPrice;
            DateCreated = dateCreated;
            this.dailyQualityModifier = dailyQualityModifier;
        }

        protected Product(string type, string description, int quality, DateTime dueDate, float fixPrice, int dailyQualityModifier)
        {
            Type = type;
            Description = description;
            Quality = quality;
            startingQuality = quality;
            DueDate = dueDate;
            FixPrice = fixPrice;
            DateCreated = DateTime.Now;
            this.dailyQualityModifier = dailyQualityModifier;
        }

        protected Product(string type, string description, int quality, int dueInDays, float fixPrice, int dailyQualityModifier, DateTime dateCreated)
        {
            Type = type;
            Description = description;
            Quality = quality;
            startingQuality = quality;
            DueDate = dateCreated.AddDays(dueInDays);
            FixPrice = fixPrice;
            DateCreated = dateCreated;
            this.dailyQualityModifier = dailyQualityModifier;
        }

        protected Product(string type, string description, int quality, int dueInDays, float fixPrice, int dailyQualityModifier)
        {
            Type = type;
            Description = description;
            Quality = quality;
            startingQuality = quality;
            FixPrice = fixPrice;
            DateCreated = DateTime.Now;
            DueDate = DateCreated.AddDays(dueInDays);
            this.dailyQualityModifier = dailyQualityModifier;
        }

        public override string ToString()
        {
            var priceString = GetPrice().ToString();
            if (priceString.Contains(','))
            {
                if (priceString.Substring(priceString.IndexOf(',')).Length != 3)
                {
                    priceString += "0";
                }
            } else
            {
                priceString += ",00";
            }
            
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Type: ");
            stringBuilder.Append(Type);
            stringBuilder.Append(", Description: ");
            stringBuilder.Append(Description);
            stringBuilder.Append(", Quality: ");
            stringBuilder.Append(Quality);
            if(DueDate != DateTime.MaxValue)
            {
                stringBuilder.Append(", Due Date: ");
                stringBuilder.Append(DueDate.ToString("dd.MM.yyyy"));
            }
            stringBuilder.Append(", Price: ");
            stringBuilder.Append(priceString);
            stringBuilder.Append("€");
            return stringBuilder.ToString();
        }

        public virtual float GetPrice()
        {
            return MathF.Round(FixPrice + QUALITY_PRICE_MODIFIER * Quality, 2);
        }

        public virtual int UpdateQuality(DateTime currentDateTime)
        {
            var daysInBetween = currentDateTime.Subtract(DateCreated);
            Quality = startingQuality - (daysInBetween.Days * dailyQualityModifier);
            return Quality;
        }

        public abstract bool IsQualityGood();
    }
}
