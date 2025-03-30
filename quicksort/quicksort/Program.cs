namespace quicksort;
using System.Collections.Generic;
using System;

public class QuickSortAlgorithm
{
    static int NejhezciPivot(int[] souborPrvku, int imal, int ivel)
    {
        if (imal > ivel)
        {
            Console.WriteLine("Varování: Neplatný rozsah indexů v NejhezciPivot.");
            return -1;
        }

        double sum = 0;
        for (int k = imal; k <= ivel; k++)
        {
            sum += souborPrvku[k];
        }
        double prumer = sum / (ivel - imal + 1);// divame se na rozdíl jednotlivých prvků od průměru

        int indexCisla = imal;
        double nejrozdil = Math.Abs(souborPrvku[imal] - prumer);

        for (int k = imal + 1; k <= ivel; k++)
        {
            double rozdil = Math.Abs(souborPrvku[k] - prumer);  //když najdeme menší průměr než jsme měli doteď tak si ho uložíme a uložíme i index toho prvku 
            if (rozdil < nejrozdil)
            {
                nejrozdil = rozdil;
                indexCisla = k;
            }
        }
        return indexCisla;
    }

    static int Partition(int[] souborPrvku, int imal, int ivel)
    {
        if (imal >= ivel) //jestli tam je dost prvku n serazeni
        {
            return imal;
        }

        int pivotIndex = NejhezciPivot(souborPrvku, imal, ivel);

        if (pivotIndex == -1)
        {
            return imal; 
        }

        int pivotValue = souborPrvku[pivotIndex];

        Swap(souborPrvku, pivotIndex, ivel);

        int i = imal;
        int j = ivel - 1;

        while (i <= j)
        {
            while (i <= j && souborPrvku[i] < pivotValue)
            {
                i++;
            }

            while (i <= j && souborPrvku[j] > pivotValue)//indexi se posouvaji dokud nenajdou neco co jim nematchuje a to pak navyajem prohodi 
            {
                j--;
            }

            if (i <= j)
            {
                Swap(souborPrvku, i, j);
                i++;
                j--;
            }
        }

        Swap(souborPrvku, i, ivel);

        return i;
    }

    static void Swap(int[] souborPrvku, int i, int j) //funkce na prohaovani jen
    {
        int temp = souborPrvku[i];
        souborPrvku[i] = souborPrvku[j]; 
        souborPrvku[j] = temp;
    }

    public static int[] random_pole(int minCount, int maxCount, int minValue, int maxValue) //generovaní random pole cisel 
    {
        Random random = new Random();
        int count = random.Next(minCount, maxCount + 1);
        int[] randomList = new int[count];

        for (int i = 0; i < count; i++)
        {
            randomList[i] = random.Next(minValue, maxValue + 1);
        }

        return randomList;
    }

    public static void QuickSort(int[] souborPrvku, int imal, int ivel) 
    {
        if (imal < ivel)
        {
            int pi = Partition(souborPrvku, imal, ivel); //returne index piota po rozrazeni
            QuickSort(souborPrvku, imal, pi - 1); 
            QuickSort(souborPrvku, pi + 1, ivel);
        }
    }

    public static void Main(string[] args)
    {
        int[] nahodnaData = random_pole(5, 20, 1, 20);
        int n = nahodnaData.Length;

        Console.WriteLine("pole před seřazením");
        PrintniToUz(nahodnaData);

        QuickSort(nahodnaData, 0, n - 1);

        Console.WriteLine("Pole po seřazení:");
        PrintniToUz(nahodnaData);
    }

    static void PrintniToUz(int[] souborPrvku)
    {
        for (int i = 0; i < souborPrvku.Length; i++)
        {
            Console.Write(souborPrvku[i] + " ");
        }
        Console.WriteLine();
    }
}