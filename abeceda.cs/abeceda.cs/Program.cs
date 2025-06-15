using System;
using System.Collections.Generic;
using System.Linq;

namespace abeceda.cs
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Zadej závislosti (třeba 'c<b a<c'), nebo 'konec' pro ukončení:");

            while (true)
            {
                string vstup = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(vstup) || vstup.ToLower() == "konec")
                    break;

                Console.WriteLine($"Vstup: {vstup}");
                try
                {
                    var vysledek = SeradZavislosti(vstup);
                    Console.WriteLine($"Výsledek: {vysledek}");
                }
                catch (Exception chyba)
                {
                    Console.WriteLine($"Problém: {chyba.Message}");
                }

                Console.WriteLine("\nZadej další závislosti, nebo 'konec':");
            }
        }

        static string SeradZavislosti(string vstup)
        {
            // Graf závislostí
            var graf = new Dictionary<char, List<char>>();
            var uzly = new HashSet<char>();

            // Rozdělíme vstup na jednotlivý závislosti
            var zavislosti = vstup.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var zavislost in zavislosti)
            {
                // Čekáme formát "x<y"
                if (zavislost.Length != 3 || zavislost[1] != '<')
                    throw new ArgumentException("Špatný formát závislosti");

                char odkud = zavislost[0];
                char kam = zavislost[2];

                // Přidáme uzly do grafu
                if (!graf.ContainsKey(odkud))
                    graf[odkud] = new List<char>();
                graf[odkud].Add(kam);

                // Uložíme všechny uzly
                uzly.Add(odkud);
                uzly.Add(kam);
            }

            // Topologické řazení
            var vysledek = new List<char>();
            var navstivene = new HashSet<char>();
            var docasne = new HashSet<char>(); // Na chytání cyklů

            void Prohledej(char uzel)
            {
                if (docasne.Contains(uzel))
                    throw new InvalidOperationException("Našel jsem cykl v závislostech!");

                if (navstivene.Contains(uzel))
                    return;

                docasne.Add(uzel);

                // Projdeme sousedy
                if (graf.ContainsKey(uzel))
                {
                    foreach (var soused in graf[uzel])
                    {
                        Prohledej(soused);
                    }
                }

                docasne.Remove(uzel);
                navstivene.Add(uzel);
                vysledek.Add(uzel);
            }

            foreach (var uzel in uzly)
            {
                if (!navstivene.Contains(uzel))
                    Prohledej(uzel);
            }
            return new string(vysledek.ToArray());
        }
    }
}