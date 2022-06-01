using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace CozyKangaroo
{
    class Invoice
    {
        private Order invoiceOrder;
        private String timeOrderWasProcessed;
        private List<Meal> purchasedMeals;
        // Constructor
        public Invoice(Order order)
        {
            invoiceOrder = order;
            purchasedMeals = order.Meals;
            timeOrderWasProcessed = DateTime.Now.ToString("hh:mm:ss");
        }

        // Getters and Setters
        public List<Meal> Meals
        {
            get => purchasedMeals;
        }
        public int orderNumber
        {
            get => invoiceOrder.OrderNumber;
        }
        public OrderType OrderType
        {
            get => invoiceOrder.OrderType;

        }
        public string TimeandDate
        {
            get => timeOrderWasProcessed;

        }

   
        public void generateInvoice()
        {
            // produces tax invoice if the order has been paid for
            if (invoiceOrder.pay() != null)
            {
                Console.WriteLine("Thankyou For Shopping with the COZY KANGAROO");
                Console.WriteLine("Your Order Number :   " + invoiceOrder.OrderNumber);
                Console.WriteLine("Your Table Number(if you are Dining in ) :   " + invoiceOrder.TableNumber);

                Console.WriteLine("Your Ordered Items  :   ");
                for (int i = 0; i < invoiceOrder.Meals.Count; i++)
                {
                    Console.WriteLine("item "+i + " : " +invoiceOrder.Meals[i].Name + " | Price Of Item : " + invoiceOrder.Meals[i].Price);
                }

                Console.WriteLine("Total Paid :   " + invoiceOrder.OrderTotal());
                Console.WriteLine("Total GST (included):   " + (invoiceOrder.OrderTotal()*0.01));

            }
            else
            { 
                Console.WriteLine("Order has not been Paid for, please return to order screen and pay for meal.");

            }
        }

    }
}
