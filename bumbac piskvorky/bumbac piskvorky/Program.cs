using static System.Net.Mime.MediaTypeNames;

namespace bumbac_piskvorky
{
    internal class Program
    {
        static void Main(string[] args)
        {
        int[,] board = new int[6, 7]
        {
                    { 0, 0, 0, 0, 1, 0, 0 },
                    { 0, 0, 0, 1, 2, 0, 0 },
                    { 0, 0, 1, 1, 1, 0, 0 },
                    { 0, 1, 1, 1, 2, 2, 2 },
                    { 1, 1, 2, 1, 2, 2, 2 },
                    { 1, 2, 2, 1, 2, 2, 2 }
        };
            int[] position = { 0, 4 };
            if (win_otaznik(board, position, 1, 5))
            {
                Console.WriteLine($"Broski {1} ma W rizz");
            }
            else
            {
                Console.WriteLine("eh not cool");
            }
           
        }

        static bool win_otaznik(int[,] hracipole, int[] pozice, int hrac, int kolik_por_win)
        {
            return radek_kuk(hracipole, pozice, hrac, kolik_por_win) ||
                        sloupec_kuk(hracipole, pozice, hrac, kolik_por_win) ||
                               diagonala_kuk(hracipole, pozice, hrac, kolik_por_win);
        }
        static bool sloupec_kuk(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win)
        {
            int kolik = hracipole.GetLength(0);
            int sluopec_nakuk = pozice[1];
            int Wrizz = 0;
            for (int radek = pozice[0]; radek < kolik; radek++)
            {
                if (hracipole[radek, sluopec_nakuk] == hrac)
                {
                    Wrizz += 1;
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


        static bool radek_kuk(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win)
        {
            int radek_na_kuk = pozice[0];
            int Wrizz = 0;
            int kolik = hracipole.GetLength(1);
            for (int sloupec = pozice[1]; sloupec < kolik; sloupec++)
            {
                if (hracipole[radek_na_kuk, sloupec] == hrac)
                {
                    Wrizz += 1;
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
            if (Wrizz == kolik_pro_win) {
                for (int sloupec = pozice[1] - 1; sloupec >= 0; sloupec--)
                {
                    if (hracipole[radek_na_kuk, sloupec] == hrac)
                    {
                        Wrizz ++  ;
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
        static bool diagonala_kuk(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win)
        {
            return prvni_diag(hracipole,pozice,hrac,kolik_pro_win) || druha_diag(hracipole,pozice,hrac,kolik_pro_win);
        }
        static bool prvni_diag(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win)
        {
            int radek = pozice[0];
            int sloupec = pozice[1];
            int Wrizz = 0;
            for (int i = 0;radek - i >= 0 && sloupec - i >= 0; i++)
            {
                if (hracipole[radek - i,sloupec - i] == hrac)
                {
                    Wrizz ++ ;
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
            int polesir = hracipole.GetLength (1);
            for (int i = 1; radek + i < polevys && sloupec + i < polesir; i++)
            {
                if (hracipole[radek + i, sloupec + i] == hrac)
                {
                    Wrizz++;
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
        static bool druha_diag(int[,] hracipole, int[] pozice, int hrac, int kolik_pro_win)
        {
            int radek = pozice[0];
            int sloupec = pozice[1];
            int Wrizz = 0;
            int polevys = hracipole.GetLength(0);
            int polesir = hracipole.GetLength(1);
            for (int i = 0; radek - i >= 0 && sloupec + i <polesir; i++)
            {
                if (hracipole[radek - i, sloupec + i] == hrac)
                {
                    Wrizz++;
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
            
            for (int i = 1; radek + i < polevys && sloupec - i >=0; i++)
            {
                if (hracipole[radek + i, sloupec - i] == hrac)
                {
                    Wrizz++;
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
