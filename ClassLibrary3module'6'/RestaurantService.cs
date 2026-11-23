using Restaurant.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3module_6_
{
    public class RestaurantService : IFilterVegetarianMenu
    {
        public List<Dish> Menu { get; }
        public List<Courier> Couriers { get; }
        public List<TableReservation> Reservations { get; }
        public List<Order> Orders { get; }

        public RestaurantService(List<Dish> menu, List<Courier> couriers,
            List<TableReservation> reservations, List<Order> orders)
        {
            Menu = menu; Couriers = couriers; Reservations = reservations; Orders = orders;
        }

        public List<Dish> GetVegetarianMenu() => Menu.FindAll(d => d.IsVegetarian && d.IsAvailable);

        public void Reserve(TableReservation r)
        {
            if (Reservations.Exists(x => x.ConflictsWith(r)))
                throw new TableAlreadyReservedException(r.TableNumber, r.Start);
            Reservations.Add(r);
        }

        public Order PlaceOrder(List<Dish> dishes, decimal tipPercent, bool delivery)
        {
            if (dishes.Count == 0) throw new InvalidOrderException("Замовлення порожнє.");
            foreach (var d in dishes)
                if (!d.IsAvailable) throw new DishNotAvailableException(d.Name);

            Courier? courier = null;
            if (delivery)
            {
                courier = Couriers.Find(c => !c.IsBusy) ?? throw new CourierUnavailableException();
                courier.IsBusy = true;
            }
            var order = new Order { Dishes = dishes, TipPercent = tipPercent, IsDelivery = delivery, Courier = courier };
            Orders.Add(order);
            return order;
        }
    }
}
