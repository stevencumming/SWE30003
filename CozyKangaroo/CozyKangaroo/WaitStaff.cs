using System;
using System.Collections.Generic;
using System.Globalization;

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
            string orderTypeStr;
            char orderTypeChar = ' ';
            do {
                if (orderTypeChar != ' ') {
                    Console.WriteLine("\nPlease enter a correct order type!\n");
                }
                Console.Write(
                    "Order Type\n" +
                    "  D    Dine-in\n" +
                    "  T    Take-away\n" +
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
                Meal orderMeal = customer.selectMenuItem(menu, ref mealStr);
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
