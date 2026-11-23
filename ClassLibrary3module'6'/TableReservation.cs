using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class TableReservation : ITableReservation
    {
        private int _tableNumber = 1;
        private int _guests = 1;
        public static readonly TimeSpan Duration = TimeSpan.FromHours(2);

        public Guid Id { get; set; } = Guid.NewGuid();
        public int TableNumber
        {
            get => _tableNumber;
            set
            {
                if (value < 1 || value > 50) throw new ArgumentException("Столик має бути від 1 до 50.");
                _tableNumber = value;
            }
        }
        public DateTime Start { get; set; }
        public int Guests
        {
            get => _guests;
            set
            {
                if (value < 1 || value > 20) throw new ArgumentException("Гостей має бути від 1 до 20.");
                _guests = value;
            }
        }
        public bool ConflictsWith(TableReservation other) =>
            TableNumber == other.TableNumber &&
            Start < other.Start + Duration && other.Start < Start + Duration;

        public override string ToString() => $"Столик {TableNumber}, {Start:dd.MM.yyyy HH:mm}, гостей: {Guests}";


    }
}
