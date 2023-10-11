using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Відкриття файлу
            XmlDocument doc = new XmlDocument();
            doc.Load("Halereya.xml");

            //=======Переглядаємо файл у консолі
            Console.WriteLine("Зміст файлу Halereya.xml:");
            Console.WriteLine(doc.InnerXml);
            Console.WriteLine();

            //=======За кодом вивести на консоль прізвище художника, назву картини та її ціну.
            Console.Write("Введіть код картини: ");
            string inputCode = Console.ReadLine();
            XmlNodeList paintings = doc.SelectNodes("/paintings/painting");

            foreach (XmlNode painting in paintings)
            {
                string code = painting.SelectSingleNode("code").InnerText;
                if (code == inputCode)
                {
                    string artistLastName = painting.SelectSingleNode("artist_lastname").InnerText;
                    string paintingTitle = painting.SelectSingleNode("painting_title").InnerText;
                    string price = painting.SelectSingleNode("price").InnerText;

                    Console.WriteLine($"Прізвище художника: {artistLastName}");
                    Console.WriteLine($"Назва картини: {paintingTitle}");
                    Console.WriteLine($"Ціна: {price}");
                }
            }
            Console.WriteLine();

            //=======Обчислити сумарну ціну усіх картин, що містяться в запаснику
            double totalPriceInStorage = 0;
            foreach (XmlNode painting in paintings)
            {
                string status = painting.SelectSingleNode("status").InnerText;
                if (status == "2") // "2" означає картину в запаснику
                {
                    double price = Convert.ToDouble(painting.SelectSingleNode("price").InnerText);
                    totalPriceInStorage += price;
                }
            }
            Console.WriteLine($"Сумарна ціна усіх картин в запаснику: {totalPriceInStorage}");
        }
    }
}
