namespace bonuslinkedlist
{
    internal class Program
    {
        static void Main(string[] args)
        {


            LinkedList zena = new LinkedList();
            LinkedList muz = new LinkedList();
            zena.soucet(muz);
            zena.rozdil(muz);



        }
        class Node
        {
            public Node(int value)
            {
                Value = value;
            }
            public int Value { get; set; }

            public Node Next { get; set; }

        }
        class LinkedList
        {
            public Node Head { get; set; }
            public void Add(int value)
            {
                if (Head == null)
                    Head = new Node(value);

                else
                {
                    Node newNode = new Node(value);
                    newNode.Next = Head;
                    Head = newNode;
                }
            }
            public void Reverse()
            {
                Node previous = null;
                Node current = Head;
                Node next = null;

                while (current != null)
                {
                    next = current.Next; // Uložíme si odkaz na další uzel
                    current.Next = previous; // Obrátíme směr odkazu
                    previous = current; // Posuneme ukazatel "previous" na aktuální uzel
                    current = next; // Posuneme ukazatel "current" na další uzel
                }

                Head = previous;
            }
            public int Length()
            {
                int count = 0;
                Node current = Head;
                while (current != null)
                {
                    count++;
                    current = current.Next;
                }
                return count;
            }
            public void vypis()
            {
                Node node = Head;

                if (node == null)
                {
                    Console.Write("bruv nic tam neni");
                    return;
                }
                while (node != null)
                {
                    Console.Write($"{node.Value}");
                    node = node.Next;
                }
                Console.WriteLine();
                return;
            }
            public void Rozdeleni_do_cislic(LinkedList other)
            {
                Console.WriteLine("zadejte prvni cislo");
                string input = Console.ReadLine();
                Console.WriteLine("druhe cislo");

                string input01 = Console.ReadLine();
                string muz01 = new string(input.Where(char.IsDigit).ToArray());
                string zena01 = new string(input01.Where(char.IsDigit).ToArray());//odstranim cokoli jineho nez cisla

                foreach (char c in muz01)
                {
                    int cislice = c - '0'; //vemu jednotive cislo a definuju ho jako cislo 
                    other.Add(cislice);//pridam cislicka do linkedlistu
                }

                foreach (char c in zena01)
                {
                    int yislice = c - '0';
                    this.Add(yislice); //pridam cislicka do linkedlistu
                }

            }
            public void soucet(LinkedList other)
            {
                this.Rozdeleni_do_cislic(other);
                LinkedList vysledek = new LinkedList();
                Node kluk = other.Head;
                Node holka = this.Head;
                int navic = 0;

                while (kluk != null || holka != null || navic != 0)
                {
                    int sum = navic; // zacnu s tim co je mby navic z minula

                    if (kluk != null)
                    {
                        sum += kluk.Value;//pridame do sum z current uzlu z jednoho cisla tu cifru na ktery prave sme
                        kluk = kluk.Next;
                    }

                    if (holka != null)
                    {
                        sum += holka.Value;//pridame do sum z current uzlu z jednoho cisla tu cifru na ktery prave sme
                        holka = holka.Next;//dem na dalsi uzel
                    }

                    navic = sum / 10; // videli sum 10 a pracuje tak ze nema zbytek takze to bude budto 0 nebo 1
                    vysledek.Add(sum % 10); // dava tam posledni cislici sum


                }
                Console.WriteLine();
                Console.Write($" vysledek rovnice je ");
                vysledek.vypis();

                
            }//ten bonus
            public void rozdil(LinkedList other)
            {
                this.Rozdeleni_do_cislic(other);
                LinkedList vysledek = new LinkedList();
                Node kluk = other.Head;
                Node holka = this.Head;
                int navic = 0;

                while (kluk != null || holka != null || navic != 0)
                {
                    int rozdil = navic;

                    if (kluk != null)
                    {
                        rozdil += kluk.Value; 
                        kluk = kluk.Next;
                    }

                    if (holka != null)
                    {
                        rozdil -= holka.Value; 
                        holka = holka.Next; 
                    }

                    if (rozdil < 0) 
                    {
                        rozdil += 10; //jedine tohle je jinak a to pokud bude zeo rozdil tec cifer vzchazet mensi jak 0 tak musime aby to nebylo minus pricist deset a timpadem pristi kolo odectem jedna
                        navic = -1; 
                    }
                    else
                    {
                        navic = 0; 
                    }

                    vysledek.Add(rozdil); 
                }

                Console.WriteLine();
                Console.Write("vysledek rozdilu cisel je ");
                vysledek.vypis();
            }//doufam v bodiky navic
        }

    }
}

