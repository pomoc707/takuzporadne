using System;
using System.Collections.Generic;
namespace zasobnik
{
    class Program
    {
        static void Main(string[] args)
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
    }
}
