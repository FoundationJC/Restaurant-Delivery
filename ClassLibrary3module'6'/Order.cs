using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Order : ITipsDeliveryCalculation
    {
        private decimal _tipPercent;
        public const decimal DeliveryFee = 50m;

        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Dish> Dishes { get; set; } = new();
        public decimal TipPercent
        {
            get => _tipPercent;
            set
            {
                if (value < 0 || value > 100) throw new ArgumentException("Чайові: від 0 до 100%.");
                _tipPercent = value;
            }
        }
        public bool IsDelivery { get; set; }
        public Courier? Courier { get; set; }

        public decimal CalculateTips() => Dishes.Sum(d => d.Price) * TipPercent / 100m;
        public decimal CalculateDelivery() => IsDelivery ? DeliveryFee : 0m;

        public decimal CalculateTotal() => Dishes.Sum(d => d.Price) + CalculateTips() + CalculateDelivery();
    }
}
