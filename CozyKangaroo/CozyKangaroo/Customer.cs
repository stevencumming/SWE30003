using System;
using System.Collections.Generic;

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

            // Add dine-in conditional code block

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

        public bool reserveTable(DateTime dateTime, int tableNumber)
        {
            ApplicationFacade af = ApplicationFacade.Singleton;
            Table table = af.Reservation.CreateReservation(this, tableNumber, dateTime);
            return table != null ? true : false;
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