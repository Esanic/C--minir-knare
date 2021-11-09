using System;
using System.Collections.Generic;
using System.Text;

namespace Miniräknare
{
    class Program
    {   
        //Inledning:
        //Från början hade jag skrivit ut all kod och inte använt några funktioner överhuvudtaget.
        //Ju längre jag satt med projektet desto mer lärde jag mig och kunde börja använda funktioner, vilket jag aldrig använt innan i C#.
        //Jag är övertygad om att det finns rum för förbättringar men jag tror mig ha kortat ner koden i "Main" till sitt minimum nu utan att överanvända funktioner.
        //Jag vet inte exakt vad som skulle kunna förbättras härifrån då jag successivt har förbättrat och förenklat koden under projektets gång.
        //Ett exempel är på rad 73 och 75 som tidigare tog på +30 rader tillsammans.
        //Inser nu i efterhand att jag borde upprättat Git för att kunna påvisa hur koden utvecklat sig. Man lär sig av sina misstag ¯\_(ツ)_/¯
        //Men i det stora hela har jag lärt mig mycket nya saker. Alltifrån funktioner, for-loopar, TryParse, string.Join, rätt användande av && och ||, \n, ! innan ett påstående för att vända på innebörden av påståendet.
        //hej
        
        //Funktion som utför uträkningarna baserat på vilken operator och antalet tal som användaren har angivit.
        //Använder if-satser då man enkelt kan få de resultat man önskar.
        //Skulle kunna använda switch istället.
        static double Calculate(string Operator, int amountNumbers, List<double> userNumbers)
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
                double result  = userNumbers[0];
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
                return 0;
            }
        }

        //Funktion som tar resultatet från funktionen Calculate
        //Skriver ut beräkningen för användaren genom string.Join som skriver ut en lista och separerar den med vad man anger.
        //Sparar användarens uträkning i en lista genom string.Join.
        //Rensar listan för att inte eventuella framtida beräkningar ska behålla gamla värden.
        //Detta var den mest förenklade lösningen jag kom fram till. Från att ha en if-lista så lärde jag mig att förenkla det till detta.
        static void Summary(string op, int amountNumbers, List<string> memory, List<double> userNumbers)
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
        static double DivideZero(double inputNum)
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
        static double AssignNum(string quantity, string op)
            {
                Console.WriteLine("Ange ditt " + quantity + " tal: ");
                string input = Console.ReadLine();
                double num;
                while (!double.TryParse(input, out num))
                {
                    Console.WriteLine("Du angav inte ett tal. Försök igen\nAnge ditt " + quantity + " tal: ");
                    input = Console.ReadLine();
                }
                if (op == "/" && num==0)
                {
                num = DivideZero(num);
                }
            return num;
            }

        static void Main(string[] args)
        {
            string loop = "ja";
            string Operator = string.Empty;
            List<string> memory = new List<string>();
            List<double> userNumber = new List<double>();            
            List<string> countWord = new List<string>()
            {
                "första",
                "andra",
                "tredje",
                "fjärde",
                "femte",
            };

            Console.WriteLine("Välkommen till miniräknaren!");
            
            //Loopar miniräknaren så länge användaren anger "ja" eller "j" på frågan om det ska göras fler uträkningar
            while (loop == "ja" || loop == "j")
            {
                //Återställer operatorChoice så att koden kan fråga användaren om vilken operator som ska användas.
                string operatorChoice = string.Empty;

                //Så länge inte användaren har gjort några tidigare uträkningar kommer inte "Dina tidigare uträkningar" att skrivas.
                if (memory.Count != 0)
                {
                    Console.WriteLine("Dina tidigare uträkningar är:");
                }

                //Kontrollerar om memory.Count är högre än count, om "true" så inkrementerar den count
                //Skriver sedan ut de historiska uträkningarna där den skriver ut varje sparad entry med hjälp av inkrementeringen av count
                for (int count = 0; count < memory.Count; count++)
                {
                    Console.WriteLine(memory[count]);
                }
                
                //En while-loop som ber användaren att ange vilken operator som önskas användas. 
                //Om den anger något annat än operatorerna (eller marcus) så ber den om ett giltigt värde genom en while loop.
                //Om användaren svarar att den angav fel operator så loopas koden om tills operatorChoice inte blir "j" eller "ja".
                Console.WriteLine();
                while (!(operatorChoice =="j" || operatorChoice=="ja"))
                {
                    Console.WriteLine("Vad för typ av operator önskar du använda?\nAnge +, -, / eller *");
                    Operator = Console.ReadLine().ToLower();
                    Console.Clear();

                    while (Operator != "+" && Operator != "-" && Operator != "/" && Operator != "*" && Operator != "marcus")
                    {
                        Console.WriteLine("Du angav inte en giltig operator. Försök igen.");
                        Console.WriteLine("Vad för typ av operator önskar du använda?\nAnge +, -, / eller *");

                        Operator = Console.ReadLine().ToLower();
                    }
                    //Om marcus anges som operator så skriver den hej och avslutar programmet.
                    if (Operator == "marcus")
                    {
                        Console.WriteLine("Hej!");
                        Environment.Exit(0);
                    }

                    //Programmet talar om vilken operator som användaren valde och frågar om det var rätt. Svarar användaren nej loopas frågan om operator om.
                    Console.WriteLine("Du valde " + Operator + " som operator.\nVar det rätt? Svara med 'j' eller 'n'");
                    operatorChoice = Console.ReadLine().ToLower();
                    
                    if (!(operatorChoice == "ja" && operatorChoice =="j"))
                    {
                        Console.Clear();
                    }
                }

                //Användaren får ange hur många tal den önskar att miniräknaren ska använda vid uträkningen.
                Console.WriteLine("Hur många tal önskar du använda? Välj mellan 2 och 5");
                string inputAmount = Console.ReadLine();
                int amountNumbers;

                while (!int.TryParse(inputAmount, out amountNumbers))
                {
                    Console.WriteLine("Du angav inte ett tal, försök igen.");
                    inputAmount = Console.ReadLine();
                }
                while (amountNumbers <2 || amountNumbers >5)
                {
                    Console.WriteLine("Du angav inte ett tal mellan 2 och 5. Försök igen.");
                    inputAmount = Console.ReadLine();
                    while (!int.TryParse(inputAmount, out amountNumbers))
                    {
                        Console.WriteLine("Du angav inte ett tal, försök igen.");
                        inputAmount = Console.ReadLine();
                    }
                }
                //Med hjälp av funktionen AssignNum så får användaren ange de talen som önskas användas.
                //Loopar lika många gånger som det antalet tal användaren angav i amountNumbers.
                for (int i = 0; i < amountNumbers; i++)
                {
                    userNumber.Add(AssignNum(countWord[i],Operator));
                }

                //Själva uträkningen, utskrivningen och lagringen av historiken
                Summary(Operator, amountNumbers, memory, userNumber);

                
                //Användaren blir tillfrågad om man önskar att genomföra ytterligare uträkningar.
                Console.WriteLine("Önskar du göra en ny uträkning? \nSvara med 'j' eller 'n'");
                loop = Console.ReadLine().ToLower();
                Console.Clear();

            }
            //Om användaren anger att den inte vill utföra fler uträkningar så tackar programmet för användandet.
            Console.Clear();
            Console.WriteLine("Tack för att du använde miniräknaren!");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
