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
            string orderTypeStr;
            char orderTypeChar = ' ';
            do {
                if (orderTypeChar != ' ') {
                    Console.WriteLine("\nPlease enter a correct order type!\n");
                }
                Console.Write(
                    "Order Type\n\n" +
                    "  D    Dine-in\n" +
                    "  T    Take-away\n\n" +
                    "Select order type: "
                );
                orderTypeStr = Console.ReadLine().Trim().ToUpper();
                if (orderTypeStr != "") {
                    orderTypeChar = orderTypeStr[0];
                }
            } while (orderTypeChar != 'D' && orderTypeChar != 'T');
            OrderType orderType = orderTypeChar == 'D' ? OrderType.DineIn : OrderType.Takeaway;

            string mealStr = "";
            List<Meal> mealList = new List<Meal>();
            do {
                Meal orderMeal = selectMenuItem(menu, ref mealStr);
                if (orderMeal != null) {
                    mealList.Add(orderMeal);
                }
                else if (mealStr.ToUpper() != "DONE") {
                    Console.WriteLine("\nNo meal found!\n");
                }
            } while (mealStr.ToUpper() != "DONE");

            if (orderType == OrderType.DineIn) {
                string hasReservationStr;
                char hasReservationChar = ' ';
                do {
                    if (hasReservationChar != ' ') {
                        Console.WriteLine("\nPlease enter a correct answer!\n");
                    }
                    Console.Write("Do you have a reservation (y/N): ");
                    hasReservationStr = Console.ReadLine().Trim().ToUpper();
                    if (hasReservationStr == "") {
                        hasReservationChar = hasReservationStr[0];
                    }
                } while (hasReservationChar != 'Y' && hasReservationChar != 'N');

                Table table;
                if (hasReservationChar == 'Y') {
                    string format = "dd-MM-yyyy HH:mm";
                    CultureInfo local = new CultureInfo("en-AU");
                    string dateTimeStr;
                    bool dateConverted = false;
                    DateTime dateTime;
                    int tableNumber = -1;
                    ApplicationFacade af = ApplicationFacade.Singleton;
                    do {
                        Console.Write($"Reservation date time (format {format}): ");
                        dateTimeStr = Console.ReadLine().Trim();
                        dateConverted = DateTime.TryParseExact(dateTimeStr, format, local, new DateTimeStyles(), out dateTime);
                        Console.Write("Table number: ");
                        try {
                            tableNumber = Convert.ToInt32(Console.ReadLine().Trim());
                        } catch {
                            Console.WriteLine("\nPlease enter a valid table number!\n");
                        }
                        if (!dateConverted) {
                            Console.WriteLine("\nPlease enter in a valid date time!\n");
                        }
                        table = af.Reservation.FindReservation(dateTime, tableNumber);
                    } while (!dateConverted || table == null);
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
            Console.Write("Enter meal name (Type 'DONE' when done): ");
            mealStr = Console.ReadLine().Trim();
            return menu.GetMeal(mealStr);
        }

        public Table reserveTable()
        {
            Table table;
            string format = "dd-MM-yyyy HH:mm";
            CultureInfo local = new CultureInfo("en-AU");
            string dateTimeStr;
            DateTime dateTime;
            int tableNumber = -1;
            bool dateConverted = false;
            ApplicationFacade af = ApplicationFacade.Singleton;
            do {
                Console.Write($"Reservation date time (format {format}): ");
                dateTimeStr = Console.ReadLine().Trim();
                dateConverted = DateTime.TryParseExact(dateTimeStr, format, local, DateTimeStyles.None, out dateTime);
                Console.Write("Table number: ");
                try {
                    tableNumber = Convert.ToInt32(Console.ReadLine().Trim());
                } catch {
                    Console.WriteLine("\nPlease enter a valid table number!\n");
                }
                if (!dateConverted) {
                    Console.WriteLine("\nPlease enter in a valid date time!\n");
                }
                table = af.Reservation.FindReservation(dateTime, tableNumber);
                Console.WriteLine(dateConverted);
                Console.WriteLine(dateTimeStr);
                Console.WriteLine(dateTime);
            } while (!dateConverted || table == null);
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