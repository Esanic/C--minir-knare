using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miniräknare
{
    class Program
    {

        //Inledning:
        //Från början hade jag skrivit ut all kod och inte använt några funktioner överhuvudtaget.
        //Ju längre jag satt med projektet desto mer lärde jag mig och kunde börja använda funktioner, vilket jag aldrig använt innan i C#.
        //Jag har även flyttat dessa funktioner till en klass som kallas när det behövs.
        //Jag är övertygad om att det finns rum för förbättringar men jag tror mig ha kortat ner koden i "Main" till sitt minimum nu utan att överanvända funktioner.
        //Jag vet inte exakt vad som skulle kunna förbättras härifrån då jag successivt har förbättrat och förenklat koden under projektets gång. 
        //Ett exempel är på rad 67 och 69 i Functions.cs som tidigare tog på +30 rader tillsammans.
        //Inser nu i efterhand att jag borde upprättat Git från början för att kunna påvisa hur koden utvecklat sig. Man lär sig av sina misstag ¯\_(ツ)_/¯
        //Men i det stora hela har jag lärt mig mycket nya saker. Alltifrån funktioner, for-loopar, TryParse, string.Join, rätt användande av && och ||, \n, ! innan ett påstående för att vända på innebörden av påståendet.

        static void Main(string[] args)
        {
            string loop = "ja";
            string Operator = string.Empty;
            
            List<string> memory = new();
            List<double> userNumber = new();
            //Hämtar räkneorden ifrån .txt dokumentet countWords och lägger i listan countWord
            var logFile = File.ReadAllLines("../../../countWords.txt");
            List<string> countWord = new(logFile);
            
            int amountNumbersLowest = 2;
            int amountNumbersHighest = countWord.Count;
 
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
                    Console.WriteLine($"Du valde {Operator} som operator.\nVar det rätt? Svara med 'j' eller 'n'");
                    operatorChoice = Console.ReadLine().ToLower();
                    
                    if (!(operatorChoice == "ja" && operatorChoice =="j"))
                    {
                        Console.Clear();
                    }
                }

                //Användaren får ange hur många tal den önskar att miniräknaren ska använda vid uträkningen.
                Console.WriteLine($"Hur många tal önskar du använda? Välj mellan {amountNumbersLowest} och {amountNumbersHighest}");
                string inputAmount = Console.ReadLine();
                int amountNumbers;

                while (!int.TryParse(inputAmount, out amountNumbers))
                {
                    Console.WriteLine("Du angav inte ett tal, försök igen.");
                    inputAmount = Console.ReadLine();
                }
                while (amountNumbers < amountNumbersLowest || amountNumbers > amountNumbersHighest)
                {
                    Console.WriteLine($"Du angav inte ett tal mellan {amountNumbersLowest} och {amountNumbersHighest}. Försök igen.");
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
                    userNumber.Add(Functions.AssignNum(countWord[i],Operator));
                }

                //Själva uträkningen, utskrivningen och lagringen av historiken
                Functions.Summary(Operator, amountNumbers, memory, userNumber);

                
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
