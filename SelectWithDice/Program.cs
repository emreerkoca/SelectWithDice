using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectWithDice
{
    class Program
    {
        enum elementTypes { Book, Clothes };

        static void Main(string[] args)
        {
            Console.WriteLine("Elements to choose from:\n");
           
            var bookList = InsertList("Book");

            var selectedElement = SelectWithDice(bookList);

            Console.WriteLine("\n\nChoice of dice: {0}", selectedElement[0].Name);
            Console.ReadKey();
        }

        static List<IElement> DivideAndSelect(List<IElement> optionList)
        {
            List<IElement> list = new List<IElement>();
            int firstIndex = 0;
            int lastIndex = 0;
            bool isEven = optionList.Count % 2 == 0 ? true : false; //Is list count even or odd
            int middleIndex = isEven ? optionList.Count / 2 : (optionList.Count - 1) / 2;
            int diceValue = RollDice();
            if (diceValue == 1 || diceValue == 2 || diceValue == 3)
            {
                lastIndex = isEven ? middleIndex : middleIndex + 1;
            }
            else //Other situations: 4, 5, 6
            {
                firstIndex = isEven ? middleIndex : middleIndex + 1;
                lastIndex = optionList.Count;
            }

            for (int i = firstIndex; i < lastIndex; i++)
            {
                list.Add(optionList[i]);
            }

            return list;
        }

        //Divide list 2 parts
        static List<IElement> SelectWithDice(List<IElement> optionList)
        {
            if(optionList.Count == 1)
            {
                return optionList;
            }

            optionList = DivideAndSelect(optionList);

            if (optionList.Count >= 6)
            {
                optionList = SelectWithDice(optionList);
            }
            else if(optionList.Count <= 6)
            {
                Random rnd = new Random();
                int selectedIndex = rnd.Next(0, optionList.Count - 1);
                IElement selectedOption = optionList[selectedIndex];
                optionList = new List<IElement> { selectedOption };
            }

            return optionList;
        }

        public static List<IElement> InsertList(string type)
        {
            Book book;
            Dress dress;
            List<IElement> list = new List<IElement>();
            for (int i = 0; i < 13; i++)
            {
                switch (type)
                {
                    case "Book":
                        book = new Book();
                        book.Name = "Book" + i;
                        list.Add(book);
                        Console.Write(book.Name + " ");
                        break;
                    case "Dress":
                        dress = new Dress();
                        dress.Name = "Dress" + i;
                        list.Add(dress);
                        Console.Write(dress.Name + " ");
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        public static int RollDice()
        {
            Random rnd = new Random();
            return rnd.Next(1, 6); // Dice values: 1, 2, 3, 4, 5, 6
        }
    }
}
