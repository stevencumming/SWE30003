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

        }
    }
}
