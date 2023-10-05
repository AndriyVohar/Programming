using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Product
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Product(int code, string name, int quantity, int unitPrice)
        {
            Code = code;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
    public class Delivery
    {
        public int ProductCode { get; set; }
        public DateTime Date { get; set; }
        public string Supplier { get; set; }
        public int Volume { get; set; }
        public decimal PricePerUnit { get; set; }
        public Delivery(int productCode, string date, string supplier, int volume, int pricePerUnit)
        {
            ProductCode = productCode;
            Date = DateTime.Parse(date);
            Supplier = supplier;
            Volume = volume;
            PricePerUnit = pricePerUnit;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //4) Елементи колекції «Товари» мають наступну структуру: код товару, назва товару,
            //кількість одиниць товару, відпускна ціна одиниці товару (коди унікальні,
            //назви можуть повторюватися). Елементи колекції «Поставки» містять код товару,
            //дату, назву постачальника, обсяг поставок, ціну за одиницю.
            //а) Вивести середній обсяг поставок товару з заданим кодом, здійснених протягом
            //другого півріччя минулого року.
            //b) Вивести постачальників, які поставили товар із заданою назвою за найменшою ціною.
            //с) Вивести по кожній наявній назві товару можливий прибуток від його реалізації.
            
            List<Product> products = new List<Product>()
            {
                new Product(1,"Kronenbourg",20,50),
                new Product(2,"Мівіна",15,10),
                new Product(3,"Ніж",10,150),
                new Product(4,"Дюшес",20,30),
                new Product(5,"Сoca-Cola",50,60),
                new Product(6,"Мука",5,45),
                new Product(7,"Чіпси Lay's",30,25),
                new Product(8,"Шоколад Milka",25,70),
                new Product(9,"Сир Gouda",12,90),
                new Product(10,"Помідори",40,20),
                new Product(11,"Шампунь Pantene",8,120),
                new Product(12,"Мило Dove",40,15),
                new Product(13,"Молоко",30,25),
                new Product(14,"Кава Lavazza",10,80),
                new Product(15,"Зубна щітка",50,5),
                new Product(16,"Папір туалетний",24,15),
                new Product(17,"Сік апельсиновий",20,40),
                new Product(18,"Яйця",60,12),
                new Product(19,"Зубна паста Colgate",15,50),
                new Product(20,"Спеції",8,35),
                new Product(21,"Шоколадка Snickers",18,30),
                new Product(22,"Масло вершкове",25,40),
                new Product(23,"Гречка",10,55),
                new Product(24,"Миска для супу",15,15),
                new Product(25,"Салфетки паперові",50,8)
            };


             
            List< Delivery> deliveryList = new List<Delivery>()
            {
                new Delivery(1, "2022-07-06", "Нова пошта", 45, 62),
                new Delivery(2, "2022-07-07", "Укрпошта", 38, 48),
                new Delivery(3, "2022-07-08", "Meest", 42, 55),
                new Delivery(4, "2022-07-09", "Нова пошта", 48, 67),
                new Delivery(5, "2022-07-06", "Укрпошта", 40, 58),
                new Delivery(6, "2022-07-06", "Meest", 50, 70),
                new Delivery(7, "2022-07-12", "Нова пошта", 55, 75),
                new Delivery(8, "2022-07-06", "Укрпошта", 46, 63),
                new Delivery(9, "2022-07-08", "Meest", 60, 80),
                new Delivery(10, "2022-07-15", "Нова пошта", 60, 85),
                new Delivery(11, "2022-07-06", "Укрпошта", 52, 73),
                new Delivery(12, "2022-07-06", "Meest", 68, 90),
                new Delivery(13, "2022-07-08", "Нова пошта", 68, 95),
                new Delivery(14, "2022-07-06", "Укрпошта", 58, 80),
                new Delivery(15, "2022-07-08", "Meest", 75, 100),
                new Delivery(16, "2022-07-08", "Нова пошта", 75, 105),
                new Delivery(17, "2022-07-07", "Укрпошта", 64, 88),
                new Delivery(18, "2022-07-06", "Meest", 80, 110),
                new Delivery(19, "2022-07-08", "Нова пошта", 80, 115),
                new Delivery(20, "2022-07-06", "Укрпошта", 70, 95),
                new Delivery(21, "2022-07-07", "Meest", 90, 120),
                new Delivery(22, "2022-07-08", "Нова пошта", 90, 125),
                new Delivery(23, "2022-07-06", "Укрпошта", 78, 103),
                new Delivery(24, "2022-07-08", "Meest", 100, 135),
                new Delivery(25, "2022-07-08", "Нова пошта", 100, 140)

            };

            Console.WriteLine(DateTime.Now.Year+1);
            TaskA(deliveryList, 1);
            TaskB(products, deliveryList, "Kronenbourg");
            TaskC(products, deliveryList);
        }

        //а) Вивести середній обсяг поставок товару з заданим кодом, здійснених протягом
        //другого півріччя минулого року.
        public static void TaskA(List<Delivery> deliveries, int productCode)
        {
            int year = DateTime.Now.Year-1;

            double averageVolume = deliveries
                .Where(d => d.ProductCode == productCode && d.Date.Year == year && d.Date.Month >= 7)
                .Average(d => d.Volume);
            Console.WriteLine("Cередній обсяг поставок товару з заданим кодом, здійснених протягом" +
                " другого півріччя минулого року: {0}",averageVolume);
        }

        //b) Вивести постачальників, які поставили товар із заданою назвою за найменшою ціною.
        public static void TaskB(List<Product> products, List<Delivery> deliveries, string productName)
        {
            var suppliers = deliveries
                .Where(d => products.Any(p => p.Code == d.ProductCode && p.Name == productName))
                .GroupBy(d => d.Supplier)
                .Select(group => new
                {
                    Supplier = group.Key,
                    MinPrice = group.Min(d => d.PricePerUnit)
                })
                .OrderBy(s => s.MinPrice);
            Console.WriteLine("Постачальники з найменшою ціною для товару:");
            foreach (var item in suppliers)
            {
                Console.WriteLine($"Постачальник: {item.Supplier}, Мінімальна ціна: {item.MinPrice}");
            }
        }

        //с) Вивести по кожній наявній назві товару можливий прибуток від його реалізації.
        public static void TaskC(List<Product> products, List<Delivery> deliveries)
        {
            var productProfits = products
                .GroupJoin(deliveries,
                    p => p.Code,
                    d => d.ProductCode,
                    (product, deliveryGroup) => new
                    {
                        ProductName = product.Name,
                        PossibleProfit = deliveryGroup.Sum(d => d.Volume * (d.PricePerUnit - product.UnitPrice))
                    });

            Console.WriteLine("Можливий прибуток за товаром:");
            foreach (var item in productProfits)
            {
                Console.WriteLine($"Назва товару: {item.ProductName}, Потенційний прибуток: {item.PossibleProfit}");
            }
        }

    }
}
