using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Lenovo x270\Documents\SDHC\Програмування (C#)\mkr2\Zavod.mdf"";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var rep = new ZavodRepository(connection);

                while (true)
                {
                Start: Console.WriteLine("Таблиця бібліотека. Введіть яку операцію ви хочете робити з таблицею:");
                    Console.WriteLine("а - вибрати всіх працівників;");
                    Console.WriteLine("b - вибрати працывника за прізвищем;");
                    Console.WriteLine("с - отримати середню зарплатню за номером цеху;");
                    Console.WriteLine("exit - вийти");
                    Console.Write(":");
                    string request = Console.ReadLine();
                    List<Zavod> zavod = rep.GetAll();
                    if (request.ToLower() == "a")
                    {
                        zavod = rep.GetAll();
                    }
                    else if (request.ToLower() == "b")
                    {
                        Console.Write("Введіть прізвище працівника: ");
                        zavod = rep.GetBySurname(Console.ReadLine());
                    }
                    else if (request.ToLower() == "c")
                    {
                        Console.Write("Введіть номер цеху (від 1 до 3): ");
                        int averageSalary = rep.GetAverageSalary(int.Parse(Console.ReadLine()));
                        Console.WriteLine($"{averageSalary}");
                        goto Start;
                    }
                    else if (request.ToLower() == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Такого не існує");
                        goto End;
                    }

                    foreach (var zav in zavod)
                    {
                        Console.WriteLine($"ShopNumber: {zav.ShopNumber}, Seniority: {zav.Seniority}, Position: {zav.Position}, Salary: {zav.Salary}");
                    }
                End: Console.ReadLine();
                    goto Start;
                }
            }
        }
    }
}
