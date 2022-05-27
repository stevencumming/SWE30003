
using System;

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

        public bool onlineOrdering() 
        {
            Order order = new Order();
            Menu menu = new Menu();

            return true;
        }

        public bool recieveMeal(Meal meal)
        {
            if (currentMeal != null) {
                return false;
            }            
            currentMeal = meal;
            return true;
        }

        public Meal selectMenuItem()
        {
            return null;
        }

        public bool reserveTable(DateTime dateTime, int tableNumber)
        {
            ApplicationFacade af = ApplicationFacade.Singleton;
            Table table = af.Reservation.CreateReservation(this, tableNumber, dateTime);
            return table != null ? true : false;
        }

        public bool payForOrder()
        {
            currentMeal = null;
            return true;
        }
    }
}