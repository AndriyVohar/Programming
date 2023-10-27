using Newtonsoft.Json.Linq;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class Painting
    {
        public string Code { get; set; }
        public string ArtistLastName { get; set; }
        public string PaintingTitle { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Завантаження JSON файлу
            string jsonFilePath = "Halereya.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            JObject jsonData = JObject.Parse(jsonContent);

            // Виведення всього JSON вмісту на консоль
            Console.WriteLine("Зміст файлу halereya.json:");
            Console.WriteLine(jsonData.ToString());
            Console.WriteLine();

            // Введення коду картини
            Console.Write("Введіть код картини: ");
            string inputCode = Console.ReadLine();

            // Отримання списку картин
            List<Painting> paintings = jsonData["paintings"].ToObject<List<Painting>>();

            // Пошук картини за кодом
            var painting = paintings.Find(p => p.Code == inputCode);

            if (painting != null)
            {
                Console.WriteLine($"Прізвище художника: {painting.ArtistLastName}");
                Console.WriteLine($"Назва картини: {painting.PaintingTitle}");
                Console.WriteLine($"Ціна: {painting.Price}");
            }
            else
            {
                Console.WriteLine("Картину з введеним кодом не знайдено.");
            }
            Console.WriteLine();

            // Обчислення сумарної ціни картин в запаснику (status == 1)
            double totalPriceInStorage = paintings.Where(p => p.Status == 1).Sum(p => p.Price);
            Console.WriteLine($"Сумарна ціна усіх картин в запаснику: {totalPriceInStorage}");
        }
    }
}
