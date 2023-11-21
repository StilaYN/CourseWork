using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SIMPLE CALCULATOR";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                try
                {
                    var input = Console.ReadLine();
                    Console.WriteLine("Result: {0}",Calculator.OperationSelection(input).ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
