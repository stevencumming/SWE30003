using System;
using System.Collections.Generic;
using System.Threading;

namespace CozyKangaroo
{
    sealed class ApplicationFacade
    {
        private static ApplicationFacade singleton = null;

        private static List<Customer> customers = new List<Customer>
        {
            new Customer("Customer 1", "cust1@example.org")
        };

        private static List<Person> staff = new List<Person>
        {
            new Manager("Frank", "boss_man", "i_am_cool"),
            new WaitStaff("Sam", "sam_1998", "sam_i_am")
        };

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
            new Order(1, new List<Meal> { menu.GetMeal("name1"), menu.GetMeal("name2") }, OrderType.Takeaway, customers[0]),
            new Order(1, new List<Meal> { menu.GetMeal("name3"), menu.GetMeal("name1") }, OrderType.DineIn, customers[0])
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
            Login();
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

        static Person Login()
        {
            Person person = null;
            char loginType;
            do {
                Console.Write(
                    "Login Type\n" +
                    "  C    Customer Login\n" +
                    "  S    Staff Login\n" +
                    "Select login type: "
                );
                loginType = Console.ReadLine().Trim()[0];
            } while (loginType != 'C' && loginType != 'S');
            switch (loginType) {
                case 'C':
                    person = customerScreen();
                    break;
                case 'S':
                    person = loginStaff();
                    break;
            }
            return person;
        }

        private static Customer customerScreen()
        {
            Customer customer = null;
            char loginOpt;
            do {
                Console.Write(
                    "Cozy Kangaroo\n" +
                    "  L    Login\n" +
                    "  C    Create Account\n" +
                    "Select login type: "
                );
                loginOpt = Console.ReadLine().Trim()[0];
            } while (loginOpt != 'L' && loginOpt != 'C');

            switch (loginOpt) {
                case 'L':
                    customer = loginCustomer();
                    break;
                case 'C':
                    customer = createCustomer();
                    break;
            }
            return customer;
        }

        private static Customer loginCustomer()
        {
            Customer customer;
            String name;
            String email;
            do {
                Console.Write("Enter name: ");
                name = Console.ReadLine().Trim();
                Console.Write("Enter email: ");
                email = Console.ReadLine().Trim();
                customer = getCustomer(email);
            } while (customer == null);
            return customer;
        }

        private static Person loginStaff()
        {
            Person staff;
            String username;
            String password;
            do {
                Console.Write("Enter username: ");
                username = Console.ReadLine().Trim();
                Console.Write("Enter password: ");
                password = Console.ReadLine().Trim();
                staff = getStaff(username, password);
            } while (staff == null);
            return staff;
        }

        private static Customer createCustomer()
        {
            String name;
            String email;
            do {
                Console.Write("Enter name: ");
                name = Console.ReadLine().Trim();
                Console.Write("Enter email: ");
                email = Console.ReadLine().Trim();
            } while (getCustomer(email) != null);
            return new Customer(name, email);
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

        private static Customer getCustomer(String email)
        {
            foreach (Customer customer in customers) {
                if (email == customer.Email) {
                    return customer;
                }
            }
            return null;
        }

        private static Person getStaff(String username, String password)
        {
            foreach (Person person in staff) {
                if (person is Manager && ((Manager) person).login(username, password)) {
                    return person;
                } else if (person is WaitStaff && ((WaitStaff) person).login(username, password)) {
                    return person;
                }
            }
            return null;
        }
    }
}
