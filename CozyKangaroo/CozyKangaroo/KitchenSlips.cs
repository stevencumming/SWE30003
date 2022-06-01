using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class KitchenSlips
    {
        private Invoice invoiceToPrint;
        // Constructor
        public KitchenSlips(Invoice invoice)
        {
            invoiceToPrint = invoice;

        }

        public void printKitchenSlip()
        {
            Console.WriteLine("New Order | Order number: " + invoiceToPrint.orderNumber);
            Console.WriteLine("Time Order Was Placed: " + invoiceToPrint.TimePurchased);
            
            for (int i = 0; i < invoiceToPrint.Meals.Count; i++)
            {
                Console.WriteLine("item " + i + " : " + invoiceToPrint.Meals[i].Name + " | Price Of Item : " + invoiceToPrint.Meals[i].Price);
            }
            switch (invoiceToPrint.OrderType)
            {
                case OrderType.DineIn:
                    {
                        Console.WriteLine("Order is for Dine-In");
                        break;

                    }
                case OrderType.Takeaway:
                    {
                        Console.WriteLine("Order is for Take Away");
                        break;
                    }

            }
        }
    }
}
