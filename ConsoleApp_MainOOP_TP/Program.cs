// See https://aka.ms/new-console-template for more information
using ClassLibrary3module_6_;
using Restaurant.Core;

public class Program
{
    public static void Main(string[] args)
    {


        // 1. Встановлюємо кодування UTF-8 для правильного відображення та введення української мови
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var dishFile = new JsonFileService<Dish>("dishes.json");
        var courierFile = new JsonFileService<Courier>("couriers.json");
        var resFile = new JsonFileService<TableReservation>("reservations.json");
        var orderFile = new JsonFileService<Order>("orders.json");

        var service = new RestaurantService(dishFile.Load(), courierFile.Load(), resFile.Load(), orderFile.Load());



        while (true)
        {
            Console.WriteLine("\n1 Додати страву | 2 Меню | 3 Вегетаріанське меню | 4 Додати кур'єра");
            Console.WriteLine("5 Забронювати столик | 6 Створити замовлення | 0 Вихід");
            int choice = ReadInt("Вибір: ", 0, 6);
            try
            {
                switch (choice)
                {
                    case 0:
                        dishFile.Save(service.Menu); courierFile.Save(service.Couriers);
                        resFile.Save(service.Reservations); orderFile.Save(service.Orders);
                        return;
                    case 1:
                        service.Menu.Add(new Dish
                        {
                            Name = ReadString("Назва: "),
                            Price = ReadInt("Ціна: ", 1, 100000),
                            IsVegetarian = ReadInt("Вегетаріанська? (1-так, 0-ні): ", 0, 1) == 1,
                            IsAvailable = ReadInt("Доступна? (1-так, 0-ні): ", 0, 1) == 1
                        });
                        break;
                    case 2:
                        var sorted = new List<Dish>(service.Menu); sorted.Sort();
                        sorted.ForEach(Console.WriteLine);
                        break;
                    case 3:
                        service.GetVegetarianMenu().ForEach(Console.WriteLine);
                        break;
                    case 4:
                        service.Couriers.Add(new Courier { Name = ReadString("Ім'я кур'єра: ") });
                        break;
                    case 5:
                        service.Reserve(new TableReservation
                        {
                            TableNumber = ReadInt("Столик (1-50): ", 1, 50),
                            Guests = ReadInt("Гостей (1-20): ", 1, 20),
                            Start = ReadDate("Дата й час (дд.мм.рррр гг:хх): ")
                        });
                        Console.WriteLine("Забронійовано.");
                        break;
                    case 6:
                        var chosen = new List<Dish>();
                        for (int i = 0; i < service.Menu.Count; i++) Console.WriteLine($"{i + 1}. {service.Menu[i]}");
                        while (true)
                        {
                            int n = ReadInt("Номер страви (0 - завершити): ", 0, service.Menu.Count);
                            if (n == 0) break;
                            chosen.Add(service.Menu[n - 1]);
                        }
                        int tip = ReadInt("Чайові, %: ", 0, 100);
                        bool delivery = ReadInt("Доставка? (1-так, 0-ні): ", 0, 1) == 1;
                        var order = service.PlaceOrder(chosen, tip, delivery);
                        Console.WriteLine($"Страви + чайові ({order.CalculateTips()}) + доставка ({order.CalculateDelivery()}) = {order.CalculateTotal()} грн");
                        break;
                }
            }
            catch (Exception ex) when (ex is CourierUnavailableException or DishNotAvailableException
                                       or TableAlreadyReservedException or InvalidOrderException or ArgumentException)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }

        int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int r) && r >= min && r <= max) return r;
                Console.WriteLine("Помилка! Введіть коректне числове значення.");
            }
        }
        string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s;
                Console.WriteLine("Рядок не може бути порожнім.");
            }
        }
        DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out var d)) return d;
                Console.WriteLine("Невірний формат дати.");
            }
        }


    }

}