namespace Restaurant.Core
{
    public class Dish : IComparable<Dish>
    {
        private string _name = "Без назви";
        private decimal _price = 1;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва страви не може бути порожньою.");
                _name = value;
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0) throw new ArgumentException("Ціна має бути більшою за 0.");
                _price = value;
            }
        }
        public bool IsVegetarian { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int CompareTo(Dish? other) =>
            other == null ? 1 : string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        public override string ToString() =>
            $"{Name} - {Price} грн{(IsVegetarian ? " [вегетаріанська]" : "")}{(IsAvailable ? "" : " (недоступна)")}";
    }
}