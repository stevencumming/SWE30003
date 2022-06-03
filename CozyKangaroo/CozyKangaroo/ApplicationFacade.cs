using System;
using System.Collections.Generic;

namespace CozyKangaroo
{
    sealed class ApplicationFacade
    {
        // Set to 2 since we have some orders pre-defined
        private static int orderNumber = 3;
        private static int reportId = 1;

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

        private Reservation reservation = new Reservation(
            new List<Table>
            {
                new Table(1, 2),
                new Table(2, 4),
                new Table(3, 1)
            }
        );

        static private Menu menu = new Menu("Cozy Kangaroo - All Items", new List<Meal>
        {
            new Meal("porterhouse", "Porterhouse Steak 300g w/ sauce & chips", 38.90, false, new List<String> {"beef", "potato", "salt", "vegetable oil"}, new List<String> {"soy beans"}, true, "https://imagelocation.com/image.png"),
            new Meal("spaghetti", "Spaghetti Bolognese", 22.90, true, new List<String> {"lean beef mince", "lean pork mince", "tomato sauce"}, new List<String> {"tomato"}, true, "https://imagelocation.com/image.png"),
            new Meal("pasta", "Penne Napoletana w/ Homemade Tomato Sauce", 21.90, true, new List<String> {"pasta", "tomato sauce"}, new List<String> {"tomato"}, true, "https://imagelocation.com/image.png"),
            new Meal("salad", "Italian Salad ", 21.90, true, new List<String> {"lettuce", "tomato", "cucumber", "olives", "balsamic dressing"}, new List<String> {"tomato"}, true, "https://imagelocation.com/image.png"),
            new Meal("parmi", "Chicken Parmigiana", 21.90, true, new List<String> {"chicken", "mozarella", "tomato sauce"}, new List<String> {"tomato"}, true, "https://imagelocation.com/image.png")
        });

        static private List<Order> orders = new List<Order>
        {
            new Order(1, new List<Meal> { menu.GetMeal("pasta"), menu.GetMeal("salad") }, OrderType.Takeaway, customers[0]),
            new Order(2, new List<Meal> { menu.GetMeal("spaghetti"), menu.GetMeal("spaghetti"), menu.GetMeal("parmi") }, OrderType.DineIn, customers[0])
        };

        private static List<Invoice> invoices = new List<Invoice>
        {
            new Invoice(orders[0]),
            new Invoice(orders[1])
        };

        private static List<Report> reports = new List<Report>();

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
            Person person;
            do
            {
                Console.Clear();
                person = Login();
                if (person == null) // if exit requested
                {
                    break;
                }
                Console.Clear();
                if (person.GetType() == typeof(Customer))
                {
                    customerMenu((Customer)person);
                }
                else if (person.GetType() == typeof(WaitStaff))
                {
                    waitStaffMenu((WaitStaff)person);
                }
                else if (person.GetType() == typeof(Manager))
                {
                    managerMenu((Manager)person);
                }
            } while (person != null);
            Console.WriteLine("Bye ~ Cozy Kangaroo!");
        }

        static Person Login()
        {
            Person person = null;
            string loginTypeStr;
            char loginTypeChar = ' ';
            do {
                if (loginTypeChar != ' ') {
                    Console.WriteLine("\nPlease enter a valid login type\n");
                }
                Console.Write(
                    "~ Welcome to Cozy Kangaroo ~\n\n" +
                    "Please select Login Type\n" +
                    "  C    Customer Login\n" +
                    "  S    Staff Login\n" +
                    "  X    Exit\n\n" +
                    "Select login type: "
                );
                loginTypeStr = Console.ReadLine().Trim().ToUpper();
                if (loginTypeStr != "") {
                    loginTypeChar = loginTypeStr[0];
                }
            } while (loginTypeChar != 'C' && loginTypeChar != 'S' && loginTypeChar != 'X');
            switch (loginTypeChar) {
                case 'C':
                    person = customerScreen();
                    break;
                case 'S':
                    person = loginStaff();
                    break;
                case 'X':
                    person = null;
                    break;
            }
            return person;
        }

        private static Customer customerScreen()
        {
            Customer customer = null;
            string loginOptStr;
            char loginOptChar = ' ';
            do {
                Console.Clear();
                if (loginOptChar != ' ') {
                    Console.WriteLine("\nPlease enter a valid login type\n");
                }
                Console.Write(
                    "Cozy Kangaroo - Customer\n\n" +
                    "  L    Login\n" +
                    "  C    Create Account\n\n" +
                    "Select login type: "
                );
                loginOptStr = Console.ReadLine().Trim().ToUpper();
                if (loginOptStr != "") {
                    loginOptChar = loginOptStr[0];
                }
            } while (loginOptChar != 'L' && loginOptChar != 'C');

            switch (loginOptChar) {
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
            bool failed = false;
            Customer customer;
            string name;
            string email;
            do {
                if (failed) {
                    Console.WriteLine("\nPlease enter a valid name and password!\n");
                }
                Console.Write("Enter name: ");
                name = Console.ReadLine().Trim();
                Console.Write("Enter email: ");
                email = Console.ReadLine().Trim();
                customer = getCustomer(email);
                if (customer == null) {
                    failed = true;
                }
            } while (customer == null);
            return customer;
        }

        private static Person loginStaff()
        {
            bool failed = false;
            Person staff;
            String username;
            String password;
            do {
                if (failed) {
                    Console.WriteLine("\nPlease enter a valid username and password!\n");
                }
                Console.Write("Enter username: ");
                username = Console.ReadLine().Trim();
                Console.Write("Enter password: ");
                password = Console.ReadLine().Trim();
                staff = getStaff(username, password);
                if (staff == null) {
                    failed = true;
                }
            } while (staff == null);
            return staff;
        }

        private static Customer createCustomer()
        {
            bool failed = false;
            string name;
            string email;
            do {
                if (failed) {
                    Console.WriteLine("\nPlease enter a valid name and email!\n");
                    failed = false;
                }
                Console.Write("Enter name: ");
                name = Console.ReadLine().Trim();
                Console.Write("Enter email: ");
                email = Console.ReadLine().Trim();
                if (name == "" || email == "" || getCustomer(email) != null) {
                    failed = true;
                }
            } while (failed);
            return new Customer(name, email);
        }

        private static void customerMenu(Customer customer)
        {
            string selectionStr;
            char selectionChar = ' ';
            do {
                Console.Clear();
                Console.Write(
                    "Customer Menu\n" +
                    "  N    New Online Order\n" +
                    "  M    View Menu\n" +
                    "  O    View Orders\n" +
                    "  R    Reserve Table\n" +
                    "  P    Pay for Order\n" +
                    "  X    Logout\n" +
                    "Select option: "
                );
                selectionStr = Console.ReadLine().Trim().ToUpper();
                if (selectionStr != "") {
                    selectionChar = selectionStr[0];
                }
                switch (selectionChar) {
                    case 'N':
                        orders.Add(customer.onlineOrdering(menu, orderNumber++));
                        break;
                    case 'M':
                        Console.Clear();
                        Console.Write("~ Cozy Kangaroo ~\nMenu: (All Items)\n");
                        menu.PrintMenu();
                        Console.WriteLine("\nPress enter to continue!\n");
                        Console.ReadLine();
                        break;
                    case 'O':
                        printOrders(customer);
                        Console.WriteLine("\nPress enter to continue!\n");
                        Console.ReadLine();
                        break;
                    case 'R':
                        Console.WriteLine(customer.reserveTable());
                        Console.WriteLine("\nPress enter to continue!\n");
                        Console.ReadLine();
                        break;
                    case 'P':
                        printOrders(customer);
                        int orderId = -1;
                        Order order;
                        do {
                            Console.Write("Enter order ID: ");
                            try {
                                orderId = Convert.ToInt32(Console.ReadLine().Trim());
                            } catch {
                                Console.WriteLine("\nPlease enter a valid orderId!\n");
                            }
                            order = GetOrder(orderId);
                        } while (order == null);
                        invoices.Add(customer.payForOrder(order));
                        break;
                    case 'X':
                        break;
                    default:
                        Console.WriteLine("\nInvalid Selection.\nPlease try again with a valid selection! Press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            } while (selectionChar != 'X');
        }

        private static void waitStaffMenu(WaitStaff waitStaff)
        {
            string selectionStr;
            char selectionChar = ' ';
            do {
                Console.Clear();
                if (selectionChar != ' ') {
                    Console.WriteLine("\nPlease enter a valid selection!\n");
                }
                Console.Write(
                    "Wait Staff Menu\n" +
                    "  N    New Customer Order\n" +
                    "  A    Add Meal to Order\n" +
                    "  O    Get Order\n" +
                    "  G    Give Meal to Customer\n" +
                    "  M    Mark Order Complete\n" +
                    "  X    Logout\n" +
                    "Select option: ");
                selectionStr = Console.ReadLine().Trim().ToUpper();
                if (selectionStr != "") {
                    selectionChar = selectionStr[0];
                }
                switch (selectionChar) {
                    case 'N':
                        String customerEmail = "";
                        Customer customer;
                        do {
                            if (customerEmail != "") {
                                Console.WriteLine("\nPlease enter a valid customer email!\n");
                            }
                            Console.Write("Enter customer email: ");
                            customerEmail = Console.ReadLine().Trim();
                            customer = getCustomer(customerEmail);
                        } while (customer == null);
                        orders.Add(waitStaff.takeCustomerOrder(menu, customer, orderNumber++));
                        break;
                    case 'A':
                        menu.PrintMenu();
                        Console.Write("Enter meal name: ");
                        String mealName = Console.ReadLine().Trim();
                        Meal meal = menu.GetMeal(mealName);
                        if (meal != null) {
                            Console.Write("Enter order ID: ");
                            int mealOrderId;
                            try {
                                mealOrderId = Convert.ToInt32(Console.ReadLine().Trim());
                            } catch {
                                Console.WriteLine("\nPlease enter a valid report Id! Press enter to continue!\n");
                                Console.ReadLine();
                                continue;
                            }
                            Order gotOrder = GetOrder(mealOrderId);
                            if (gotOrder == null) {
                                Console.WriteLine("\nPlease enter a valid order number! Press enter to continue!\n");
                                Console.ReadLine();
                            }
                            waitStaff.addMealToOrder(gotOrder, meal);
                        } else {
                            Console.WriteLine("\nMeal not found! Press enter to continue!\n");
                            Console.ReadLine();
                        }
                        break;
                    case 'O':
                        Console.Write("Enter order ID: ");
                        int orderId;
                        try {
                            orderId = Convert.ToInt32(Console.ReadLine().Trim());
                        } catch {
                            Console.WriteLine("\nPlease enter a valid order number! Press enter to continue!\n");
                            Console.ReadLine();
                            continue;
                        }
                        Order order = GetOrder(orderId);
                        if (order == null) {
                            Console.WriteLine("\nPlease enter a valid order number! Press enter to continue!\n");
                            Console.ReadLine();
                            continue;
                        }
                        Console.WriteLine(GetOrder(orderId));
                        Console.ReadLine();
                        break;
                    case 'G':
                        menu.PrintMenu();
                        Console.Write("Enter meal name: ");
                        String giveMealName = Console.ReadLine().Trim();
                        Meal giveMealMeal = menu.GetMeal(giveMealName);
                        if (giveMealMeal == null) {
                            Console.WriteLine("\nPlease enter a valid meal name! Press enter to continue!\n");
                            Console.ReadLine();
                        }
                        String giveCustomerEmail = "";
                        Customer giveCustomer;
                        do {
                            if (giveCustomerEmail != "") {
                                Console.WriteLine("\nPlease enter a valid customer email!\n");
                            }
                            Console.Write("Enter customer email: ");
                            giveCustomerEmail = Console.ReadLine().Trim();
                            giveCustomer = getCustomer(giveCustomerEmail);
                        } while (giveCustomer == null);
                        waitStaff.giveMeal(giveCustomer, giveMealMeal);
                        break;
                    case 'M':
                        Console.Write("Enter order ID: ");
                        int markOrderId;
                        try {
                            markOrderId = Convert.ToInt32(Console.ReadLine().Trim());
                        } catch {
                            Console.WriteLine("\nPlease enter a valid order number! Press enter to continue!\n");
                            Console.ReadLine();
                            continue;
                        }
                        Order markOrder = GetOrder(markOrderId);
                        if (markOrder == null) {
                            Console.WriteLine("\nOrder not found! Press enter to continue!\n");
                            Console.ReadLine();
                            continue;
                        }
                        waitStaff.markOrderComplete(markOrder);
                        break;
                }
            } while (selectionChar != 'X');
        }

        private static void managerMenu(Manager manager)
        {
            string selectionStr;
            char selectionChar = ' ';
            do {
                Console.Clear();
                if (selectionChar != ' ' && selectionChar != 'V' && selectionChar != 'G') {
                    Console.WriteLine("\nPlease enter in a valid selection!\n");
                }
                Console.Write(
                    "Manager Menu\n" +
                    "  G    Generate Complete Report\n");
                if (reports.Count != 0) Console.Write("  V    View Report\n");      // Only show option if reports actually available.
                Console.Write("  X    Logout\n" +
                    "Select option: "
                );
                selectionStr = Console.ReadLine().Trim().ToUpper();
                if (selectionStr != "") {
                    selectionChar = selectionStr[0];
                }
                switch (selectionChar) {
                    case 'G':
                        Console.Clear();
                        reports.Add(manager.generateReport(reportId++));
                        Console.Clear();
                        Console.WriteLine("Report Generated Successfully.\nPlease press Enter to continue.");
                        Console.ReadLine();
                        break;
                    case 'V':
                        if (reports.Count == 0) break;                              // if no reports available break out

                        // print current reports available
                        foreach (Report print_report in reports)
                        {
                            Console.WriteLine(print_report.ReportID + "  - Report Generated: " + "");
                        }

                        int viewReportId;
                        Report report = null;
                        do {
                            Console.Write("\nPlease enter report Id: ");
                            try {
                                viewReportId = Convert.ToInt32(Console.ReadLine().Trim());
                            } catch {
                                Console.WriteLine("\nPlease enter a valid report Id!\n");
                                continue;
                            }
                            report = getReport(viewReportId);
                        } while (report == null);
                        report.reportAllOrders();
                        Console.WriteLine("\nPlease press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            } while (selectionChar != 'X');
        }

        public Reservation Reservation
        {
            get => reservation;
        }

        public List<Invoice> Invoices
        {
            get => invoices;
        }

        public static Order GetOrder(int orderNumber)
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

        private static Report getReport(int reportId)
        {
            foreach (Report report in reports) {
                if (report.ReportID == reportId) {
                    return report;
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
        }
    }
}
