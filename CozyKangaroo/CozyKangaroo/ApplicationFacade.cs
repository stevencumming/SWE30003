using System;
using System.Collections.Generic;

namespace CozyKangaroo
{
    sealed class ApplicationFacade
    {
        private static ApplicationFacade singleton = null;
        private Reservation reservation = new Reservation(new List<Table>
        {
            new Table(1, 2),
            new Table(2, 4),
            new Table(3, 1)
        });

        private Menu menu = new Menu("Cozy Kangaroo - All Items", new List<Meal>
        {
            new Meal("name1", "description1", 1.23, false, new List<String> {"ingredient1", "ingredient2", "ingredient3"}, new List<String> {"allergen1", "allergen2"}, true, "https://imagelocation.com/image.png"),
            new Meal("name2", "description2", 4.56, true, new List<String> {"ingredient1", "ingredient2", "ingredient3"}, new List<String> {"allergen1", "allergen2"}, true, "https://imagelocation.com/image.png"),
            new Meal("name2", "description3", 7.89, true, new List<String> {"ingredient1", "ingredient2", "ingredient3"}, new List<String> {"allergen1", "allergen2"}, true, "https://imagelocation.com/image.png"),
        });
        
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

        }

        static void Login()
        {
            Console.WriteLine("Enter user name");

            string luser = Console.ReadLine();
        }

        public Reservation Reservation
        {
            get 
            {
                return reservation;
            }
        }
    }
}
