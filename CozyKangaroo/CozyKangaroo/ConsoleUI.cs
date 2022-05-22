using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class ConsoleUI
    {
        public const int APP_WIDTH = 80;
        public const ConsoleColor BORDER_COLOUR = ConsoleColor.Green;
        public const ConsoleColor TEXT_COLOUR = ConsoleColor.White;
        // application will be 80 characters wide

        public void clearConsole()
        {
            // function to clear the current console and show application header
            Console.Clear();
        }
        
       public void printHeading(string heading)
        {
            lineBorder();

            printJustifyCenter(heading);

            lineBorder();

            Console.ForegroundColor = ConsoleColor.White;                               // reset colour of heading
        }

       public void printMenu(string[] options)
        {
            lineBorder();


        }
        
        public void changeColour(string colour)
        {

        }

        public void writeLine(string text)
        {
            // should probably do some range-checking here...?
            Console.WriteLine(text);
        }

        public void lineBorder()
        {
            Console.ForegroundColor = BORDER_COLOUR;
            for (int i = 0; i < APP_WIDTH; i++)
            {
                Console.Write("*");
            }
            Console.Write("\n");
            Console.ForegroundColor = TEXT_COLOUR;
        }

        public void printJustifyCenter(string text)
        {
            if (text.Length <= APP_WIDTH - 2)                                            // heading will fit on the display
            {
                int lremaining = APP_WIDTH - text.Length - 2;                            // work out left and right margins (width minus text minus border)
                if (lremaining % 2 == 0)                                                    // it's even
                {
                    Console.ForegroundColor = BORDER_COLOUR;
                    Console.Write("*");                                                     // left border
                    for (int i = 0; i < lremaining / 2; i++)                                // whitespace left margin
                    {
                        Console.Write(" ");
                    }
                    Console.ForegroundColor = TEXT_COLOUR;
                    Console.Write(text);                                                 // heading
                    Console.ForegroundColor = BORDER_COLOUR;
                    for (int i = 0; i < lremaining / 2; i++)                                // whitespace right margin
                    {
                        Console.Write(" ");
                    }
                    Console.Write("*\n");                                                   // right border
                    Console.ForegroundColor = TEXT_COLOUR;
                }
                else
                {                                                                           // it's odd, so heading will be slightly off-center
                    Console.ForegroundColor = BORDER_COLOUR;
                    Console.Write("*");                                                     // left border
                    for (int i = 0; i < (lremaining / 2) + 1; i++)                          // whitespace left margin, plus one to center
                    {
                        Console.Write(" ");
                    }
                    Console.ForegroundColor = TEXT_COLOUR;
                    Console.Write(text);                                                 // heading
                    Console.ForegroundColor = BORDER_COLOUR;
                    for (int i = 0; i < lremaining / 2; i++)                                // whitespace right margin
                    {
                        Console.Write(" ");
                    }
                    Console.Write("*\n");                                                   // right border
                    Console.ForegroundColor = TEXT_COLOUR;
                }
            }
        }

        public void printJustifyLeft(string text)
        {
            if (text.Length <= APP_WIDTH - 2 - 4)
            {
                Console.ForegroundColor = BORDER_COLOUR;
                Console.Write("*    ");
                Console.ForegroundColor = TEXT_COLOUR;
                Console.Write(text);
                Console.ForegroundColor = BORDER_COLOUR;
                for (int i = 0; i < (APP_WIDTH - text.Length - 2 - 4); i++)
                {
                    Console.Write(" ");
                }
                Console.Write("*\n");
                Console.ForegroundColor = TEXT_COLOUR;
            }
        }
    }
}
