using System;
using System.Collections.Generic;
using System.Globalization;

namespace CozyKangaroo 
{
    class Customer: Person
    {
        private string email;
        private Meal currentMeal = null;

        public Customer(string name, string email) 
        {
            this.name = name;
            this.email = email;
        }

        public Order onlineOrdering(Menu menu, int orderNumber)
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
                Meal orderMeal = selectMenuItem(menu, ref mealStr);
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
                    table = reserveTable();
                }
                return new Order(orderNumber, mealList, orderType, this, table);
            }
            return new Order(orderNumber, mealList, orderType, this);
        }

        public bool recieveMeal(Meal meal)
        {
            if (currentMeal != null) {
                return false;
            }            
            currentMeal = meal;
            return true;
        }

        public Meal selectMenuItem(Menu menu, ref String mealStr)
        {
            menu.PrintMenu();
            Console.Write(
                "(Type 'DONE' when done)\n" +
                "Enter meal name: "
            );
            mealStr = Console.ReadLine().Trim();
            return menu.GetMeal(mealStr);
        }

        public Table reserveTable()
        {
            String format = "MM/dd/yyyy hh:mm";
            Console.Write($"Reservation date time (format {format}): ");
            String dateTimeStr = Console.ReadLine().Trim();
            DateTime dateTime = DateTime.ParseExact(dateTimeStr, format, new CultureInfo("en-AU"));
            Console.Write("Table number: ");
            int tableNumber = Convert.ToInt32(Console.ReadLine().Trim());
            ApplicationFacade af = ApplicationFacade.Singleton;
            Table table = af.Reservation.CreateReservation(this, tableNumber, dateTime);
            return table;
        }

        public Invoice payForOrder(Order order)
        {
            currentMeal = null;
            return order.pay();
        }

        public String Email
        {
            get => email;
        }
    }
}