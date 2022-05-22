using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class ConsoleUI
    {
        public const int APP_WIDTH = 80;
        // application will be 80 characters wide

        public void clearConsole()
        {
            // function to clear the current console and show application header
            Console.Clear();
        }
        public void heading(string heading, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;                                               // set colour of heading
            
            for (int i = 0; i < APP_WIDTH; i++)
            {
                Console.Write("*");
            }
            Console.Write("\n");


            if (heading.Length <= APP_WIDTH - 2)                                            // heading will fit on the display
            {
                int lremaining = APP_WIDTH - heading.Length - 2;                            // work out left and right margins (width minus text minus border)
                if (lremaining % 2 == 0)                                                    // it's even
                {
                    Console.Write("*");                                                     // left border
                    for (int i = 0; i < lremaining / 2; i++)                                // whitespace left margin
                    {
                        Console.Write(" ");
                    }
                    Console.Write(heading);                                                 // heading
                    for (int i = 0; i < lremaining / 2; i++)                                // whitespace right margin
                    {
                        Console.Write(" ");
                    }
                    Console.Write("*\n");                                                   // right border
                }
                else
                {                                                                           // it's odd, so heading will be slightly off-center
                    Console.Write("*");                                                     // left border
                    for (int i = 0; i < (lremaining / 2) + 1; i++)                          // whitespace left margin, plus one to center
                    {
                        Console.Write(" ");
                    }
                    Console.Write(heading);                                                 // heading
                    for (int i = 0; i < lremaining / 2; i++)                                // whitespace right margin
                    {
                        Console.Write(" ");
                    }
                    Console.Write("*\n");                                                   // right border
                }


                for (int i = 0; i < APP_WIDTH; i++)
                {
                    Console.Write("*");
                }
                Console.Write("\n");

                Console.ForegroundColor = ConsoleColor.White;                               // reset colour of heading
            }
        }

        public void changeColour(string colour)
        {

        }

        public void writeLine(string text)
        {
            // should probably do some range-checking here...?
            Console.WriteLine(text);
        }


    }
}
