using System;
using System.Collections.Generic;
using System.Threading;

namespace CozyKangaroo
{
    sealed class ApplicationFacade
    {
        // Set to 2 since we have some orders pre-defined
        private static int orderNumber = 2;

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
            new Order(0, new List<Meal> { menu.GetMeal("name1"), menu.GetMeal("name2") }, OrderType.Takeaway, customers[0]),
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
            Person person = Login();
            switch (person) {
                case Customer:
                    customerMenu((Customer) person);
                    break;
                case WaitStaff:
                    break;
                case Manager:
                    break;
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

        private static void customerMenu(Customer customer)
        {
            Char selection;
            do {
                Console.Clear();
                Console.Write(
                    "Cusomer Menu\n" +
                    "  N    New Online Order\n" +
                    "  M    View Menu\n" +
                    "  O    View Orders\n" +
                    "  X    Exit\n" +
                    "Select option: "
                );
                selection = Console.ReadLine().Trim()[0];
                switch (selection) {
                    case 'N':
                        orders.Add(customer.onlineOrdering(menu, orderNumber++));
                        break;
                    case 'M':
                        menu.PrintMenu();
                        Console.ReadLine();
                        break;
                    case 'O':
                        printOrders(customer);
                        break;
                }
            } while (selection != 'X');
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

        private static void printOrders(Customer customer)
        {
            foreach (Order order in orders) {
                if (order.PlacedBy == customer) {
                    Console.WriteLine(order.ToString());
                }
            }
            Console.ReadLine();
        }
    }
}
