using System;
using System.Collections.Generic;
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

        public bool takeCustomerOrder(Customer customer) 
        {
            //Order order = new Order();
            //Menu menu = new Menu();

            return true;
        }

        public bool editOrder(Order order)
        {
            return true;
        }

        //public Order getOrder(int orderId) 
        //{
        //    return new Order();
        //}

        public bool giveMeal(Customer customer, Meal meal)
        {
            return customer.recieveMeal(meal);
        }

        public bool markOrderComplete(Order order)
        {
            return true;
        }
    }
}
