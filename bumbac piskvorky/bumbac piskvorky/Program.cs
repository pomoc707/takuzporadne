using System;

namespace bumbac_piskvorky
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] board = new int[6, 7]; // Vytvořím hrací pole 6*7
            int hrac = 1; 

            while (true) 
            {
                Console.Clear(); // promazavani pred novym tahem 
                VykresliHraciPole(board, null); // herní pole kuk

                int sloupec; 
                while (true) 
                {
                    Console.Write($"Hráč {hrac}, zadej sloupec (1-7): "); 
                    if (int.TryParse(Console.ReadLine(), out sloupec) && sloupec >= 1 && sloupec <= 7) // kontrola jestli sloupec sedi
                    {
                        sloupec--; // odectu 1 aby sedeli indexy
                        break; 
                    }
                    Console.WriteLine("Neplatný vstup. Zadej prosím číslo od 1 do 7."); /// Hráč zadal blbost, tak ho napomenu
                }

                int radek = NajdiVolnyRadek(board, sloupec); // kam to bumbac
                if (radek == -1) 
                {
                    Console.WriteLine("Sloupec je plný. Zadej jiný."); // Jo, tak smůla
                    Console.ReadKey();
                    continue; 
                }
                board[radek, sloupec] = hrac; // placnu cislo hrace

                int[] pozice = { radek, sloupec }; // Uložím si pozici posledního tahu
                int[,] vyherniPole = null; 
                if (win_otaznik(board, pozice, hrac, 4, out vyherniPole)) // kuk na vyhru
                {
                    Console.Clear();
                    VykresliHraciPole(board, vyherniPole); // Vykreslím pole 
                    Console.WriteLine($"Hráč {hrac} vyhrál!"); 
                    break; 
                }

                hrac = 3 - hrac; //Prepinani hracu
            }

            Console.ReadKey(); 
        }

        static void VykresliHraciPole(int[,] hracipole, int[,] vyherniPole)
        {
            for (int i = 0; i < hracipole.GetLength(0); i++) //Projedu všechny řádky
            {
                for (int j = 0; j < hracipole.GetLength(1); j++) //všechny sloupce
                {
                    if (vyherniPole != null && vyherniPole[i, j] != 0)  // kontrola vyhry
                    {
                        Console.ForegroundColor = ConsoleColor.Green; 
                        Console.Write(hracipole[i, j] + " "); 
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(hracipole[i, j] + " "); 
                    }
                }
                Console.WriteLine();
            }
        }

        static int NajdiVolnyRadek(int[,] hracipole, int sloupec)
        {
            for (int radek = hracipole.GetLength(0) - 1; radek >= 0; radek--) // Projedu řádky 
            {
                if (hracipole[radek, sloupec] == 0) // Najdu  nezadany radek
                {
                    return radek; 
                }
            }
            return -1; 
        }

        static bool win_otaznik(int[,] hracipole, int[] pozice, int hrac, int kolik_por_win, out int[,] vyherniPole)
        {
            vyherniPole = new int[hracipole.GetLength(0), hracipole.GetLength(1)]; 
            return radek_kuk(hracipole, pozice, hrac, kolik_por_win, vyherniPole) ||
                   sloupec_kuk(hracipole, pozice, hrac, kolik_por_win, vyherniPole) ||
                   diagonala_kuk(hracipole, pozice, hrac, kolik_por_win, vyherniPole); 
        }

        
        static bool sloupec_kuk(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win, int[,] vyherniPole)
        {
            int kolik = hracipole.GetLength(0);
            int sluopec_nakuk = pozice[1];
            int Wrizz = 0;
            for (int radek = pozice[0]; radek < kolik; radek++)
            {
                if (hracipole[radek, sluopec_nakuk] == hrac)
                {
                    Wrizz += 1;
                    vyherniPole[radek, sluopec_nakuk] = 1; // dam jako výherní
                    if (Wrizz == kolik_pro_win)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        static bool radek_kuk(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win, int[,] vyherniPole)
        {
            int radek_na_kuk = pozice[0];
            int Wrizz = 0;
            int kolik = hracipole.GetLength(1);
            for (int sloupec = pozice[1]; sloupec < kolik; sloupec++)
            {
                if (hracipole[radek_na_kuk, sloupec] == hrac)
                {
                    Wrizz += 1;
                    vyherniPole[radek_na_kuk, sloupec] = 1;
                    if (Wrizz == kolik_pro_win)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            if (Wrizz == kolik_pro_win)
            {
                Wrizz--; // Odečtu poslední přičtení, protože jsem našel kolik_pro_win
                for (int sloupec = pozice[1] - 1; sloupec >= 0; sloupec--)
                {
                    if (hracipole[radek_na_kuk, sloupec] == hrac)
                    {
                        Wrizz++;
                        vyherniPole[radek_na_kuk, sloupec] = 1;
                        if (Wrizz == kolik_pro_win)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return false;
        }

        static bool diagonala_kuk(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win, int[,] vyherniPole)
        {
            return prvni_diag(hracipole, pozice, hrac, kolik_pro_win, vyherniPole) || druha_diag(hracipole, pozice, hrac, kolik_pro_win, vyherniPole);
        }

        static bool prvni_diag(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win, int[,] vyherniPole)
        {
            int radek = pozice[0];
            int sloupec = pozice[1];
            int Wrizz = 0;
            for (int i = 0; radek - i >= 0 && sloupec - i >= 0; i++)
            {
                if (hracipole[radek - i, sloupec - i] == hrac)
                {
                    Wrizz++;
                    vyherniPole[radek - i, sloupec - i] = 1;
                    if (Wrizz == kolik_pro_win)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            int polevys = hracipole.GetLength(0);
            int polesir = hracipole.GetLength(1);
            for (int i = 1; radek + i < polevys && sloupec + i < polesir; i++)
            {
                if (hracipole[radek + i, sloupec + i] == hrac)
                {
                    Wrizz++;
                    vyherniPole[radek + i, sloupec + i] = 1;
                    if (Wrizz == kolik_pro_win)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        static bool druha_diag(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win, int[,] vyherniPole)
        {
            int radek = pozice[0];
            int sloupec = pozice[1];
            int Wrizz = 0;
            int polevys = hracipole.GetLength(0);
            int polesir = hracipole.GetLength(1);
            for (int i = 0; radek - i >= 0 && sloupec + i < polesir; i++)
            {
                if (hracipole[radek - i, sloupec + i] == hrac)
                {
                    Wrizz++;
                    vyherniPole[radek - i, sloupec + i] = 1;
                    if (Wrizz == kolik_pro_win)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; radek + i < polevys && sloupec - i >= 0; i++)
            {
                if (hracipole[radek + i, sloupec - i] == hrac)
                {
                    Wrizz++;
                    vyherniPole[radek + i, sloupec - i] = 1;
                    if (Wrizz == kolik_pro_win)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }
    }
}