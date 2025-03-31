using System;
using System.IO;
using System.Text;

namespace BInarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<Student> tree = new AVLTree<Student>();

            using (StreamReader streamReader = new StreamReader("studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),
                        studentData[1],
                        studentData[2],
                        Convert.ToInt16(studentData[3]),
                        studentData[4]);

                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            tree.Show();
        }
    }

    class Node<T>
    {
        public int Key { get; set; }
        public T Value { get; set; }
        public int Height { get; set; }
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
            LeftSon = null;
            RightSon = null;
            Height = 1;
        }

        public Node<T> LeftSon { get; set; }
        public Node<T> RightSon { get; set; }
    }

    class AVLTree<T>
    {
        public Node<T> Root { get; private set; }

        public void Insert(int key, T value)
        {
            Root = _insert(Root, key, value);
        }

        private Node<T> _insert(Node<T> node, int key, T value)
        {
            if (node == null)
                return new Node<T>(key, value);

            if (key < node.Key)
                node.LeftSon = _insert(node.LeftSon, key, value);
            else if (key > node.Key)
                node.RightSon = _insert(node.RightSon, key, value);
            else
                return node;

            node.Height = 1 + Math.Max(GetHeight(node.LeftSon), GetHeight(node.RightSon));

            int balance = GetBalance(node);

            if (balance > 1 && key < node.LeftSon.Key)
                return RightRotate(node);

            if (balance < -1 && key > node.RightSon.Key)
                return LeftRotate(node);

            if (balance > 1 && key > node.LeftSon.Key)
            {
                node.LeftSon = LeftRotate(node.LeftSon);
                return RightRotate(node);
            }

            if (balance < -1 && key < node.RightSon.Key)
            {
                node.RightSon = RightRotate(node.RightSon);
                return LeftRotate(node);
            }

            return node;
        }

        public Node<T> Find(int key)
        {
            Node<T> _find(Node<T> node, int key)
            {
                if (node == null)
                    return null;
                if (key == node.Key)
                    return node;
                else if (key > node.Key)
                    return _find(node.RightSon, key);
                else
                    return _find(node.LeftSon, key);
            }
            return _find(Root, key);
        }

        public void Delete(int key)
        {
            Root = _delete(Root, key);
        }

        private Node<T> _delete(Node<T> node, int key)
        {
            if (node == null)
                return null;

            if (key < node.Key)
                node.LeftSon = _delete(node.LeftSon, key);
            else if (key > node.Key)
                node.RightSon = _delete(node.RightSon, key);
            else
            {
                if (node.LeftSon == null)
                    return node.RightSon;
                else if (node.RightSon == null)
                    return node.LeftSon;
                else
                {
                    Node<T> minNode = _min(node.RightSon);
                    node.Key = minNode.Key;
                    node.Value = minNode.Value;
                    node.RightSon = _delete(node.RightSon, minNode.Key);
                }
            }

            node.Height = 1 + Math.Max(GetHeight(node.LeftSon), GetHeight(node.RightSon));

            int balance = GetBalance(node);

            if (balance > 1 && GetBalance(node.LeftSon) >= 0)
                return RightRotate(node);

            if (balance > 1 && GetBalance(node.LeftSon) < 0)
            {
                node.LeftSon = LeftRotate(node.LeftSon);
                return RightRotate(node);
            }

            if (balance < -1 && GetBalance(node.RightSon) <= 0)
                return LeftRotate(node);

            if (balance < -1 && GetBalance(node.RightSon) > 0)
            {
                node.RightSon = RightRotate(node.RightSon);
                return LeftRotate(node);
            }

            return node;
        }

        public string Show()
        {
            void _show(Node<T> node, StringBuilder nodes)
            {
                if (node != null)
                {
                    _show(node.LeftSon, nodes);
                    nodes.Append(node.Value.ToString());
                    nodes.Append(" ");
                    _show(node.RightSon, nodes);
                }
            }
            StringBuilder sb = new StringBuilder();
            _show(Root, sb);
            return sb.ToString().Trim();
        }

        public Node<T> Min()
        {
            return _min(Root);
        }

        private Node<T> _min(Node<T> node)
        {
            if (node == null || node.LeftSon == null)
                return node;
            return _min(node.LeftSon);
        }

        private int GetHeight(Node<T> node)
        {
            return node == null ? 0 : node.Height;
        }

        private int GetBalance(Node<T> node)
        {
            return node == null ? 0 : GetHeight(node.LeftSon) - GetHeight(node.RightSon);
        }

        private Node<T> RightRotate(Node<T> y)
        {
            Node<T> x = y.LeftSon;
            Node<T> T2 = x.RightSon;

            x.RightSon = y;
            y.LeftSon = T2;

            y.Height = 1 + Math.Max(GetHeight(y.LeftSon), GetHeight(y.RightSon));
            x.Height = 1 + Math.Max(GetHeight(x.LeftSon), GetHeight(x.RightSon));

            return x;
        }

        private Node<T> LeftRotate(Node<T> x)
        {
            Node<T> y = x.RightSon;
            Node<T> T2 = y.LeftSon;

            y.LeftSon = x;
            x.RightSon = T2;

            x.Height = 1 + Math.Max(GetHeight(x.LeftSon), GetHeight(x.RightSon));
            y.Height = 1 + Math.Max(GetHeight(y.LeftSon), GetHeight(y.RightSon));

            return y;
        }
    }

    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }
}