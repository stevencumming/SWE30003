using System;

namespace CozyKangaroo
{
    class ApplicationFacade
    {
        static void Main(string[] args)
        {
            // on start, show logon?
            ConsoleUI console = new ConsoleUI();

            console.clearConsole();
            console.printHeading("The Cozy Kangaroo");

            console.printJustifyLeft("Testing testing testing");
            console.printJustifyLeft("");

            console.lineBorder();

          

        }

        


        static void Login()
        {
            Console.WriteLine("Enter user name");

            string luser = Console.ReadLine();

            switch (luser)
            {
                "aaa:
                    Console.WriteLine();
                    break;

                default:
                    break;
            }
        }
    }
}
