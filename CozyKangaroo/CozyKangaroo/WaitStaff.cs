using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CozyKangaroo
{
    class WaitStaff: Person
    {
        private string username;
        private string password;

        public WaitStaff(string name, string username, string password) 
        {
            this.name = name;
            this.username = username;
            this.password = password;
        }

        public bool login(String username, String password) {
            return this.username == username && this.password == password;
        }

        public Order takeCustomerOrder(Menu menu, Customer customer, int orderNumber)
        {
            char orderTypeChar;
            do {
                Console.Write(
                    "Order Type\n" +
                    "  D    Dine-in\n" +
                    "  T    Take-away\n" +
                    "Select order type: "
                );
                orderTypeChar = Console.ReadLine()[0];
            } while (orderTypeChar != 'D' && orderTypeChar != 'T');
            OrderType orderType = orderTypeChar == 'D' ? OrderType.DineIn : OrderType.Takeaway;

            String mealStr = "";
            List<Meal> mealList = new List<Meal>();
            do {
                Meal orderMeal = customer.selectMenuItem(menu, ref mealStr);
                if (orderMeal != null) {
                    mealList.Add(orderMeal);
                }
                else if (mealStr != "DONE") {
                    Console.WriteLine("No meal found!");
                }
            } while (mealStr != "DONE");

            if (orderType == OrderType.DineIn) {
                Console.Write("Do you have a reservation (y/N): ");
                char hasReservation = Console.ReadLine().Trim().ToUpper()[0];
                Table table;
                if (hasReservation == 'Y') {
                    String format = "MM/dd/yyyy hh:mm";
                    Console.Write($"Reservation date time (format {format}): ");
                    String dateTimeStr = Console.ReadLine().Trim();
                    DateTime dateTime = DateTime.ParseExact(dateTimeStr, format, new CultureInfo("en-AU"));
                    Console.Write("Table number: ");
                    int tableNumber = Convert.ToInt32(Console.ReadLine().Trim());
                    ApplicationFacade af = ApplicationFacade.Singleton;
                    table = af.Reservation.FindReservation(dateTime, tableNumber);
                } else {
                    table = customer.reserveTable();
                }
                return new Order(orderNumber, mealList, orderType, customer, table);
            }

            return new Order(orderNumber, mealList, orderType, customer);
        }

        public void addMealToOrder(Order order, Meal meal)
        {
            order.addMeal(meal);
        }

        public bool giveMeal(Customer customer, Meal meal)
        {
            return customer.recieveMeal(meal);
        }

        public void markOrderComplete(Order order)
        {
            order.setOrderStatus(OrderStatus.Complete);
        }
    }
}
