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
