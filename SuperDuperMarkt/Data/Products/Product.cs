using System;
using System.Text;

namespace SuperDuperMarkt.Data.Products
{
    public abstract class Product
    {
        private const float QUALITY_PRICE_MODIFIER = 0.1f;

        public string Description { get; protected set; }
        public int Quality { get; protected set; }
        public int DueInDays { get; protected set; }
        public DateTime DueDate { get; protected set; }
        public float FixPrice { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        private readonly int dailyQualityModifier;
        private readonly int startingQuality;
        

        protected Product(string description, int quality, DateTime dueDate, float fixPrice, int dailyQualityModifier, DateTime dateCreated)
        {
            Description = description;
            Quality = quality;
            startingQuality = quality;
            DueDate = dueDate;
            FixPrice = fixPrice;
            DateCreated = dateCreated;
            this.dailyQualityModifier = dailyQualityModifier;
            CheckProduct();
        }

        protected Product(string description, int quality, DateTime dueDate, float fixPrice, int dailyQualityModifier)
        {
            Description = description;
            Quality = quality;
            startingQuality = quality;
            DueDate = dueDate;
            FixPrice = fixPrice;
            DateCreated = DateTime.Now;
            this.dailyQualityModifier = dailyQualityModifier;
            CheckProduct();
        }

        protected Product(string description, int quality, int dueInDays, float fixPrice, int dailyQualityModifier, DateTime dateCreated)
        {
            Description = description;
            Quality = quality;
            startingQuality = quality;
            DueDate = dateCreated.AddDays(dueInDays);
            DueInDays = dueInDays;
            FixPrice = fixPrice;
            DateCreated = dateCreated;
            this.dailyQualityModifier = dailyQualityModifier;
            CheckProduct();
        }

        protected Product(string description, int quality, int dueInDays, float fixPrice, int dailyQualityModifier)
        {
            Description = description;
            Quality = quality;
            startingQuality = quality;
            FixPrice = fixPrice;
            DateCreated = DateTime.Now;
            DueDate = DateCreated.AddDays(dueInDays);
            DueInDays = dueInDays;
            this.dailyQualityModifier = dailyQualityModifier;
            CheckProduct();
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
            stringBuilder.Append(this.GetType().Name);
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

        /// <summary>
        /// Gets the calculated price by using the formula "FixPrice + 0,1 * Quality".
        /// The price is rounded to 2 decimal points.
        /// </summary>
        /// <returns>The price based on the fix price and the quality of this product.</returns>
        public virtual float GetPrice()
        {
            return MathF.Round(FixPrice + QUALITY_PRICE_MODIFIER * Quality, 2);
        }

        /// <summary>
        /// Updates the quality based on new <see cref="DateTime"/>.
        /// Saves the updated value to <see cref="Quality"/>.
        /// </summary>
        /// <param name="currentDateTime">The <see cref="DateTime"/> the quality has to be updated for.</param>
        /// <returns>The updated <see cref="Quality"/></returns>
        public virtual int UpdateQuality(DateTime currentDateTime)
        {
            var daysInBetween = currentDateTime.Subtract(DateCreated);
            Quality = startingQuality - (daysInBetween.Days * dailyQualityModifier);
            return Quality;
        }

        /// <summary>
        /// This function is called by <see cref="Product"/> in the constructor to validate the parameters.
        /// </summary>
        protected abstract void CheckProduct();

        /// <summary>
        /// Determins whether the quality of this product is still good or not.
        /// </summary>
        /// <returns>A <see cref="bool"/> whether this product is still good or not.</returns>
        public abstract bool IsQualityGood();
    }
}
