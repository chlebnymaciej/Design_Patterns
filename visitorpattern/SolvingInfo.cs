using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solvers
{
    public static class SolvingInfo
    {
        public static void Solved(string solver_model, string problem_name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[   OK  ] ");
            Console.ResetColor();
            Console.WriteLine($"{problem_name} solved by {solver_model}.");
        }

        public static void NotSolved(string solver_model, string problem_name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ ERROR ] ");
            Console.ResetColor();
            Console.WriteLine($"{problem_name} cannot be solved by {solver_model}.");
        }

        public static void ComposedSolved(string problem_name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[   OK  ] ");
            Console.ResetColor();
            Console.WriteLine($"{problem_name} was solved.");
        }

        public static void ComposedNotSolved(string problem_name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ ERROR ] ");
            Console.ResetColor();
            Console.WriteLine($"{problem_name} cannot be solved.");
        }
        
        
        // Gdy nie działa kolorowanie tekstu terminala 
        // proszę o zakomnetowanie funckji powyżej i odkomentowanie funkcji poniżej.


        /*

        public static void Solved(string solver_model, string problem_name)
        {
            Console.Write("[   OK  ] ");
            Console.WriteLine($"{problem_name} solved by {solver_model}.");
        }

        public static void NotSolved(string solver_model, string problem_name)
        {
            Console.Write("[ ERROR ] ");
            Console.WriteLine($"{problem_name} cannot be solved by {solver_model}.");
        }

        public static void ComposedSolved(string problem_name)
        {

            Console.Write("[   OK  ] ");
            Console.WriteLine($"{problem_name} was solved.");
        }

        public static void ComposedNotSolved(string problem_name)
        {
            Console.Write("[ ERROR ] ");
            Console.WriteLine($"{problem_name} cannot be solved.");
        }
        */
    }
}
