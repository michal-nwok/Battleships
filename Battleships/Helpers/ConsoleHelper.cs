using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Helpers
{
    public static class ConsoleHelper
    {
        public static void SetConsoleColorById(int id)
        {
            switch (id)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 2:
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
