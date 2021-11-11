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

        //Funktion som tar resultatet från funktionen Calculate i klassen Calculation
        //Skriver ut beräkningen för användaren genom string.Join som skriver ut en lista och separerar den med vad man anger.
        //Sparar användarens uträkning i en lista genom string.Join.
        //Rensar listan för att inte eventuella framtida beräkningar ska behålla gamla värden.
        //Detta var den mest förenklade lösningen jag kom fram till. Från att ha en if-lista så lärde jag mig att förenkla det till detta med hjälp av string.Join.
        public static void Summary(string op, int amountNumbers, List<string> memory, List<double> userNumbers)
        {
            double calculation = Calculate(op, amountNumbers, userNumbers);

            Console.WriteLine($"{string.Join($" {op} ", userNumbers)} = {calculation}");

            memory.Add($"{string.Join($" {op} ", userNumbers)} = {calculation}");
            userNumbers.Clear();

        }
        //Funktion som kontrollerar om användaren anger 0 vid division. Användaren blir då ifrågasatt ifall hen är galen och uppmanad till att ange en ny siffra till dess att den inte längre är 0.
        //Om användaren anger något annat än tal blir användaren uppmanad att ange ett tal tills dess att användaren anger ett tal.
        //Returnerar värdet av inputNum
        //Använder TryParse för att inte användaren ska kunna ange något annat än ett tal med decimaler.
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

        //Funktion där användaren blir tillfrågad om vilket tal hen ska ange.
        //Om användaren anger något annat än tal blir användaren uppmanad att ange ett tal tills dess att användaren anger ett tal.
        //Om användaren har angett division som operator och anger 0 som tal så går den in i funktionen DivideZero
        //Returnerar värdet av num
        //Använder TryParse för att inte användaren ska kunna ange något annat än ett tal med decimaler.
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
