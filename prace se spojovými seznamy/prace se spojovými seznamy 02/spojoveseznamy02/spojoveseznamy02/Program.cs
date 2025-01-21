

using System.Security.Cryptography.X509Certificates;

namespace spojoveseznamy02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList tinder01 = new LinkedList(); // vytvoření nového instance typu LinkedList (to je už náš vlastní datový typ)
            LinkedList tinder02 = new LinkedList();
            tinder01.Add(1);
            tinder01.Add(2);
            tinder02.Add(3);
            tinder01.Add(3);
            tinder02.Add(1);
            tinder01.Add(1);

            tinder01.Podsem(tinder02);
            

            

        }
    }

    class Node // Node je náš název pro třídu reprezentující jeden prvek spojového seznamu
               // tvoříme tak vlastní datový typ
    {
        public Node(int value) // konstruktor třídy Node - volá se při vytváření nové instance
        {
            Value = value;
        }
        public int Value { get; set; }
        // public - tato vlastnost je vidět i z jiné třídy ->  díky tomu ji můžeme používat ve třídě LinkedList
        // int - je celočíselného typu
        // Value - toto je název vlastnosti reprezentující Hodnotu toho prvku seznamu
        // { get; } - tím říkáme, že hodnotu lze dále v kódu jen přečíst/získat (read-only), ale nelze ji už přenastavit
        // poslední místo, kde ji můžeme nastavit je v konstruktoru, což jsme také udělali
        public Node Next { get; set; }
        // Node - vlastnost Next je typu Node - je to taková rekurzivní definice :)
        // Next - název vlastnosti označující ukazatel na další prvek seznamu
        // { get; set; } - tato vlastnost lze číst i měnit kdekoli v kódu
        // výchozí hodnota je null (což platí pro každý vlastní datový typ)
    }
    class LinkedList // LinkedList je náš název pro třídu reprezentující samotný spojový seznam
    {
        public Node Head { get; set; }
        // pro spojový seznam si stačí pamatovat odkaz na první prvek -> hlavu Head
        // { get; set; } - tato vlastnost lze číst i měnit kdekoli v kódu
        // výchozí hodnota je null

        public void Add(int value) // metoda pro přidání prvku na začátek seznamu
        {
            if (Head == null) // když seznam je zatím prázdný
                Head = new Node(value); // vložíme do ukazatele na první prvek (Head) nový prvek typu Node
                                        // všimněte si, že to je to místo, kde voláme konstruktor třídy Node s parametrem value - hodnotou, kterou má mít nový prvek
            else // v seznamu už něco je
            {
                Node newNode = new Node(value); // vytvoříme nový prvek typu Node
                newNode.Next = Head; // jeho ukazatel na další prvek (Next) nastavíme na prvek, kam ukazovala hlava seznamu -> přidáváme před původní první prvek
                Head = newNode; // přehodíme hlavu, aby ukazovala na nový první prvek
            }
        }

        public bool Find(int value)

        {
            Node node = Head;
            while (node != null)
            {
                if (node.Value == value)
                    return true;
                node = node.Next;
            }
            return false;
        }
        public void Find_low()
        {
            if (Head == null)
            {
                Console.WriteLine("neni minimum");
                return;
            }
            Node node = Head;
            int to_co_hledame = node.Value;

            while (node != null)
            {
                if (node.Value < to_co_hledame)
                    to_co_hledame = node.Value;
                node = node.Next;
            }
            Console.WriteLine($"tu mas min: {to_co_hledame}");
        }
        public void vypis()
        {
            Node node = Head;

            if (node == null)
            {
                Console.WriteLine("bruv nic tam neni");
                return;
            }
            while (node != null)
            {
                Console.WriteLine($".[{node.Value}].");
                node = node.Next;
            }
            return;
        }
        public void dolu()
        {
            if (Head == null)
            {
                Console.WriteLine("bruv nic tam neni");
                return;
            }

            bool swapped;
            do
            {
                swapped = false;
                Node current = Head;

                while (current.Next != null) // 
                {
                    if (current.Value > current.Next.Value)
                    {
                        // Swap values
                        int temp = current.Value;
                        current.Value = current.Next.Value;
                        current.Next.Value = temp;
                        swapped = true; // => swap tru
                    }
                    current = current.Next; // dalsi node
                }
            } while (swapped); // dokud swap fals

        }
        public void nahoru()
        {
            if (Head == null)
            {
                Console.WriteLine("bruv nic tam neni");
                return;
            }

            bool swapped;
            do
            {
                swapped = false;
                Node current = Head;

                while (current.Next != null) // 
                {
                    if (current.Value > current.Next.Value)
                    {
                        // Swap values
                        int temp = current.Value;
                        current.Value = current.Next.Value;
                        current.Next.Value = temp;
                        swapped = true; // => swap tru
                    }
                    current = current.Next; // dalsi node
                }
            } while (swapped); // dokud swap fals

            vypis();
        }
        private bool Find02(int value, LinkedList list)
        {
            Node current = list.Head;
            while (current != null)
            {
                if (current.Value == value)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
        public void bumbum(LinkedList other)
        {
            Node L1 = Head;
            LinkedList destrukce = new LinkedList();

            while (L1 != null)
            {
                if (Find02(L1.Value, other)) //kontrola jestli je v L2
                {
                    if (!Find02(L1.Value, destrukce)) // kontrola abz nebzlo cislo dvakrat v tom listu
                    {
                        destrukce.Add(L1.Value); // přidání do seynamu konečného
                    }
                }
                L1 = L1.Next;
            }
            destrukce.vypis();
        }
        public void Podsem(LinkedList other)
        {
            Node L1 = Head;
            LinkedList seznamek = new LinkedList();
            

            if (L1 == null)
            {
                Console.WriteLine("bruv nic tam neni");
                return;
            }
            while (L1 != null)
            {
                if (!Find02(L1.Value, seznamek)) // kontrola abz nebzlo cislo dvakrat v tom listu
                {
                    seznamek.Add(L1.Value); // přidání do seynamu konečného
                }
                L1= L1.Next;   
                
            }
            Node L2 = other.Head;
            while (L2 != null)
            {
                    if (!Find02(L2.Value, seznamek)) // kontrola abz nebzlo cislo dvakrat v tom listu
                    {
                        seznamek.Add(L2.Value); // přidání do seynamu konečného
                    } 
                L2 = L2.Next;
            }
            seznamek.nahoru();            
        }

    }
}