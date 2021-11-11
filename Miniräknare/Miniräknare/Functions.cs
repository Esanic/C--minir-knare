using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniräknare
{
    class Functions
    {
        //Funktion som utför uträkningarna baserat på vilken operator och antalet tal som användaren har angivit.
        //Använder if-satser då man enkelt kan få de resultat man önskar.
        //Skulle kunna använda switch istället.
        public static double Calculate(string Operator, int amountNumbers, List<double> userNumbers)
        {
            if (Operator == "+")
            {
                double result = userNumbers[0];
                for (int i = 1; i < amountNumbers; i++)
                {
                    result += userNumbers[i];
                }
                return result;
            }
            if (Operator == "-")
            {
                double result = userNumbers[0];
                for (int i = 1; i < amountNumbers; i++)
                {
                    result -= userNumbers[i];
                }
                return result;
            }
            if (Operator == "*")
            {
                double result = userNumbers[0];
                for (int i = 1; i < amountNumbers; i++)
                {
                    result *= userNumbers[i];
                }
                return result;
            }
            if (Operator == "/")
            {
                double result = userNumbers[0];
                for (int i = 1; i < amountNumbers; i++)
                {
                    result /= userNumbers[i];
                }
                return result;
            }
            else
            {
                return 0 ;
            }
        }

        public static void Summary(string op, int amountNumbers, List<string> memory, List<double> userNumbers)
        {
            double calculation = Calculate(op, amountNumbers, userNumbers);

            Console.WriteLine($"{string.Join($" {op} ", userNumbers)} = {calculation}");

            memory.Add($"{string.Join($" {op} ", userNumbers)} = {calculation}");
            userNumbers.Clear();

        }

        public static double DivideZero(double inputNum)
        {
            while (inputNum == 0)
            {
                Console.WriteLine("ÄR DU GALEN?! DU KAN INTE DIVIDERA MED NOLL!");
                Console.WriteLine("Ange ett nytt tal som inte är noll:");
                string input = Console.ReadLine();
                while (!double.TryParse(input, out inputNum))
                {
                    Console.WriteLine("Du angav inte ett tal. Försök igen");
                    input = Console.ReadLine();
                }
            }
            return inputNum;
        }

        public static double AssignNum(string quantity, string op)
        {
            Console.WriteLine("Ange ditt " + quantity + " tal: ");
            string input = Console.ReadLine();
            double num;
            while (!double.TryParse(input, out num))
            {
                Console.WriteLine("Du angav inte ett tal. Försök igen\nAnge ditt " + quantity + " tal: ");
                input = Console.ReadLine();
            }
            if (op == "/" && num == 0)
            {
                num = DivideZero(num);
            }
            return num;
        }
    }
}
