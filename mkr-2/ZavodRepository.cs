using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr2
{
    internal class ZavodRepository
    {
        protected SqlConnection _connection;
        public ZavodRepository(SqlConnection connection)
        {
            _connection = connection;
        }
        public List<Zavod> GetAll()
        {
            var zavods = new List<Zavod>();
            string query = "SELECT * FROM Zavod;";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Zavod zavod = new Zavod()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ShopNumber = Convert.ToInt32(reader["ShopNumber"]),
                            Seniority = Convert.ToInt32(reader["Seniority"]),
                            Position = Convert.ToString(reader["Position"]),
                            Salary = Convert.ToInt32(reader["Salary"]),
                            Surname = Convert.ToString(reader["Surname"])
                        };
                        zavods.Add(zavod);
                    }
                }
            }
            return zavods;
        }
        public List<Zavod> GetBySurname(string surname)
        {
            var zavods = new List<Zavod>();
            string query = $"SELECT ShopNumber, Seniority, Position, Salary FROM Zavod WHERE Surname = '{surname}';";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Zavod zavod = new Zavod()
                        {
                            ShopNumber = Convert.ToInt32(reader["ShopNumber"]),
                            Seniority = Convert.ToInt32(reader["Seniority"]),
                            Position = Convert.ToString(reader["Position"]),
                            Salary = Convert.ToInt32(reader["Salary"]),
                        };
                        zavods.Add(zavod);
                    }
                }
            }
            return zavods;
        }
        public int GetAverageSalary(int shop)
        {
            int averageSalary = 0;
            string query = $"SELECT AVG(Salary) as AverageSalary FROM Zavod WHERE ShopNumber = {shop};";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        averageSalary = Convert.ToInt32(reader["AverageSalary"]);
                    }
                }
            }
            return averageSalary;
        }
    }
}
