using System;
using System.Collections.Generic;
using System.Threading;

namespace CozyKangaroo
{
    sealed class ApplicationFacade
    {
        private static ApplicationFacade singleton = null;
        private static Person lsampleCustomer = new Customer("Customer 1", "cust1@example.org");

        private Reservation reservation = new Reservation(new List<Table>
        {
            new Table(1, 2),
            new Table(2, 4),
            new Table(3, 1)
        });

        static private Menu menu = new Menu("Cozy Kangaroo - All Items", new List<Meal>
        {
            new Meal("name1", "description1", 1.23, false, new List<String> {"ingredient1", "ingredient2", "ingredient3"}, new List<String> {"allergen1", "allergen2"}, true, "https://imagelocation.com/image.png"),
            new Meal("name2", "description2", 4.56, true, new List<String> {"ingredient1", "ingredient2", "ingredient3"}, new List<String> {"allergen1", "allergen2"}, true, "https://imagelocation.com/image.png"),
            new Meal("name3", "description3", 7.89, true, new List<String> {"ingredient1", "ingredient2", "ingredient3"}, new List<String> {"allergen1", "allergen2"}, true, "https://imagelocation.com/image.png"),
        });

        static private List<Order> orders = new List<Order>
        {
            new Order(1, new List<Meal> { menu.GetMeal("name1"), menu.GetMeal("name2") }, OrderType.Takeaway, lsampleCustomer),
            new Order(1, new List<Meal> { menu.GetMeal("name3"), menu.GetMeal("name1") }, OrderType.DineIn, lsampleCustomer)
        };

        private List<Invoice> invoices = new List<Invoice>
        {
            new Invoice(orders[0]),
            new Invoice(orders[1])
        };
        
        // Disabled default constructor
        private ApplicationFacade() {}
        
        // Singleton instance of class
        public static ApplicationFacade Singleton 
        {
            get 
            {
                if (singleton == null) {
                    singleton = new ApplicationFacade();
                }
                return singleton;
            }
        }

        static void Main(string[] args)
        {
            // Whatever...







            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sample Menu Screen:");
                Console.WriteLine("  N    New Order");
                Console.WriteLine("  V    View Menu");
                string lInput = Console.ReadLine();
                char lSelection = (lInput == null ? ' ' : lInput[0]);
                switch (lSelection)
                {
                    case 'C':
                        Console.WriteLine("Paying Cash:");

                        break;
                    case 'V':
                        Console.WriteLine("View Menu:");
                        menu.PrintMenu();

                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        break;
                }


                Console.ReadLine();
            }

            






        }

        static void Login()
        {
            Console.WriteLine("Enter user name");
            // can also be customer!

            
        }

        public Reservation Reservation
        {
            get => reservation;
        }

        public Order GetOrder(int orderNumber)
        {
            // Get order with ID (int)
            return orders.Find(order => order.OrderNumber == orderNumber);
        }
    }
}
