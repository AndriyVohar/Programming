using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MKR
{
    internal class Program
    {
        public static void SavePatientsToXml(List<Patient> patients, string fileName)
        {
            XElement xmlPatients = new XElement("Patients",
                from patient in patients
                select new XElement("Patient",
                    new XElement("LastName", patient.LastName),
                    new XElement("Diagnosis", patient.Diagnosis),
                    new XElement("DoctorLastName", patient.DoctorLastName),
                    new XElement("AppointmentDate", patient.AppointmentDate.ToString("yyyy-MM-dd"))
                )
            );

            xmlPatients.Save(fileName);
            Console.WriteLine($"Дані пацієнтів збережено у файлі {fileName}");
        }
        public class Doctor
        {
            public string LastName { get; set; }
            public string Department { get; set; }
        }

        public class Patient
        {
            public string LastName { get; set; }
            public string Diagnosis { get; set; }
            public string DoctorLastName { get; set; }
            public DateTime AppointmentDate { get; set; }
        }
        static void Main(string[] args)
        {
            //==================
            List<Doctor> doctors = new List<Doctor>
            {
            new Doctor { LastName = "Vogar", Department = "Traumatology" },
            new Doctor { LastName = "Pivkach", Department = "Stomatology" },
            new Doctor { LastName = "Dzyobak", Department = "Endocrinology" },
            };

            List<Patient> patients = new List<Patient>
            {
            new Patient { LastName = "Vogar", Diagnosis = "Fracture", DoctorLastName = "Vogar", AppointmentDate = DateTime.Parse("2023-09-23") },
            new Patient { LastName = "Novikova", Diagnosis = "Tooth cavity", DoctorLastName = "Pivkach", AppointmentDate = DateTime.Parse("2023-08-13") },
            new Patient { LastName = "Kirilenko", Diagnosis = "Thyroid inflammation", DoctorLastName = "Dzyobak", AppointmentDate = DateTime.Parse("2023-09-26") },
            new Patient { LastName = "Bobik", Diagnosis = "Hormonal imbalance", DoctorLastName = "Dzyobak", AppointmentDate = DateTime.Parse("2023-09-26") },
            };

            // a) Вивести різні дати та кількості прийомів, виконаних у ці дати.
            var appointmentCountsByDate = patients
                .GroupBy(p => p.AppointmentDate.Date)
                .Select(group => new
                {
                    Date = group.Key,
                    Count = group.Count()
                })
                .OrderBy(result => result.Date)
                .ToList();

            Console.WriteLine("Різні дати та кількості прийомів:");
            foreach (var result in appointmentCountsByDate)
            {
                Console.WriteLine($"{result.Date.ToShortDateString()}: {result.Count} прийомів");
            }

            // b) Вивести назви відділень та список прізвищ пацієнтів, яких приймали лікарі заданого відділення у вказаному році.
            Console.WriteLine("\n Введіть назву відділення: (Traumatology, Stomatology, Endocrinology) ");
            string targetDepartment = Console.ReadLine();

            Console.WriteLine("Введіть рік: ");
            int targetYear = int.Parse(Console.ReadLine());

            var patientsInDepartmentAndYear = patients
                .Where(p => doctors.Any(d => d.LastName == p.DoctorLastName && d.Department == targetDepartment) && p.AppointmentDate.Year == targetYear)
                .GroupBy(p => p.DoctorLastName)
                .ToDictionary(group => group.Key, group => group.Select(p => p.LastName).ToList());

            Console.WriteLine($"Пацієнти в відділенні '{targetDepartment}' у {targetYear} році:");
            foreach (var doctorPatients in patientsInDepartmentAndYear)
            {
                Console.WriteLine($"Лікар {doctorPatients.Key}: {string.Join(", ", doctorPatients.Value)}");
            }

            // c) Зберегти дані однієї з колекцій в xml-файлі.
            SavePatientsToXml(patients, "patients.xml");
        }
    }
}
