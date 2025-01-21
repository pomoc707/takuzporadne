namespace bonuslinkedlist
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            
            LinkedList.Rozdeleni_do_cislic();
        }
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
        }//idk jestli funguje
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
        public void Rozdeleni_do_cislic()

        {
            Console.WriteLine("zadejte prvni cislo");
            string nula = Console.ReadLine();
            Console.WriteLine("druhe cislo");
            string jedna = Console.ReadLine();


            LinkedList zena = new LinkedList();     
            LinkedList muz = new LinkedList();  
            foreach(char c in nula) 
            {
                int cislice = c;
                muz.Add(cislice);
            }
            
            foreach(char c in jedna)
            {
                int yislice = c;
                zena.Add(yislice);
            }
            
            ///////
            LinkedList vysledek = new LinkedList(); 
            Node kluk = muz.Head;
            Node holka = zena.Head;
            int navic = 0 ;
            int ten_delsi = 0;
            if (muz.Length() > zena.Length())
            {
                 ten_delsi = muz.Length();
            }
            if (muz.Length() < zena.Length())
            {
                 ten_delsi = zena.Length();
            }
            while ( ten_delsi != 0 )
            {
                if (kluk.Value + holka.Value + navic <= 10)
                {
                    vysledek.Add(kluk.Value + holka.Value + navic - 10);
                    navic = 1;
                    Console.WriteLine(vysledek.Head);
                }
                else
                {
                    vysledek.Add(kluk.Value + holka.Value + navic);
                    navic = 0;
                    Console.WriteLine(vysledek.Head);
                }
                holka= holka.Next;
                kluk = kluk.Next;
                ten_delsi --;
            }
             
        }
    }
}

