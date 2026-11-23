namespace Restaurant.Core
{
    public class Courier
    {
        private string _name = "Без імені";
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім'я кур'єра не може бути порожнім.");
                _name = value;
            }
        }
        public bool IsBusy { get; set; }
        public override string ToString() => $"{Name} ({(IsBusy ? "зайнятий" : "вільний")})";
    }
}