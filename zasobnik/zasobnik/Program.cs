using System;
using System.Collections.Generic;
namespace zasobnik
{
    class Program
    {
        static void Main(string[] args)
        {
            
            celydohromady01();
            celydohromady02();


        }
        static void celydohromady01()
        {
        Console.WriteLine("Zadejte řetězec závorek:");
        string vstup = Console.ReadLine();

            if (zavorky(vstup))
                    {
                        Console.WriteLine("Správně uzávorkováno.");
                    }
            else    
                    {
                        Console.WriteLine("Nesprávně uzávorkováno.");
                    }
        }
        static bool zavorky(string vstup)
        {
            Stack<char> zasobnik = new Stack<char>();

            foreach (char znak in vstup)
            {
                if (znak == '(' || znak == '[' || znak == '{')
                {
                    //otevrena zavorka
                    zasobnik.Push(znak);
                }
                else if (znak == ')' || znak == ']' || znak == '}')
                {
                    // zaviraci  závorka
                    if (zasobnik.Count == 0)
                    {
                        return false; // Uzavírací závorka bez prvni otevírací
                    }

                    char vrchol = zasobnik.Pop();

                    // kontrola jestli závorka sedi
                    if (!JeParova(vrchol, znak))
                    {
                        return false;
                    }
                }
            }

            // Zásobník musí být na konci prázdný
            return zasobnik.Count == 0;
        }

        static bool JeParova(char oteviraci, char uzaviraci)
        {
            return (oteviraci == '(' && uzaviraci == ')') ||
                   (oteviraci == '[' && uzaviraci == ']') ||
                   (oteviraci == '{' && uzaviraci == '}');
        }
        static void celydohromady02()
        {
            Console.WriteLine("Zadejte celé kladné číslo:");
            int cislo = int.Parse(Console.ReadLine());

            Console.WriteLine("Možnosti rozkladu:");
            List<int> aktualniRozklad = new List<int>();
            NajdiRozklady(cislo, 1, aktualniRozklad);
        }

        static void NajdiRozklady(int zbyle, int min, List<int> aktualniRozklad)
        {
            if (zbyle == 0)
            {
                // Pokud je zbytek nula, vypíšeme aktuální rozklad
                Console.WriteLine(string.Join("+", aktualniRozklad));
                return;
            }

            for (int i = min; i <= zbyle; i++)
            {
                aktualniRozklad.Add(i); // Přidáme číslo do rozkladu
                NajdiRozklady(zbyle - i, i, aktualniRozklad); // Rekurzivně pokračujeme
                aktualniRozklad.RemoveAt(aktualniRozklad.Count - 1); // Odstraníme číslo z rozkladu
            }
        }
    }
}
