using LeasingCompanyProject.Vehicles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace LeasingCompanyProject
{
    static class CSVFileManager
    {
        private const string PATH = "C:\\Users\\micha\\source\\repos\\volvo_academy_homeworks\\Homework2\\LeasingCompanyProject\\"; 
        public static void Save(List<Vehicle> vehicles, string fileName)
        {
            StringBuilder sb = new StringBuilder();

            // Add header
            sb.AppendLine("ID;Type;Brand;Model;YearOfManufacture;Color;Price;RegistrationNumber;Mileage;DurationOfService;Coefficient;CarBody;LeaseRate;MaxVelocity;MaxCargoSize;ComfortRate");

            // Add lines
            foreach (var vehicle in vehicles)
            {
                if (vehicle is Car)
                {
                    var car = (Car)vehicle;
                    sb.AppendLine($"{car.Id};Car;{car.Brand};{car.Model};{car.YearOfManufacture};{car.Color};{car.Price};{car.RegistrationNumber};{car.Mileage};{car.DurationOfService};{car.SpecificModelCoefficient};{car.CarBody};{car.LeaseRate};;;{car.ComfortRate}");
                }
                else if (vehicle is Truck)
                {
                    var truck = (Truck)vehicle;
                    sb.AppendLine($"{truck.Id};Truck;{truck.Brand};{truck.Model};{truck.YearOfManufacture};{truck.Color};{truck.Price};{truck.RegistrationNumber};{truck.Mileage};{truck.DurationOfService};{truck.SpecificModelCoefficient};;;{truck.MaxVelocity};{truck.MaxCargoSize};{truck.ComfortRate}");
                }
            }

            File.WriteAllText(PATH+fileName, sb.ToString());
            Console.WriteLine($"Changes has been saved to {fileName}");
        }

        public static List<Vehicle> Load(string fileName)
        {
            var vehicles = new List<Vehicle>();
            var lines = File.ReadAllLines(PATH + fileName);

            // Skip the header line
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var values = line.Split(';');

                if (values[1] == "Car")
                {
                    var car = new Car(
                        id: int.Parse(values[0]),
                        brand: values[2],
                        model: values[3],
                        yearOfManufacture: int.Parse(values[4]),
                        color: (ConsoleColor)Enum.Parse(typeof(ConsoleColor), values[5]),
                        price: decimal.Parse(values[6]),
                        registrationNumber: values[7],
                        mileage: int.Parse(values[8]),
                        durationOfService: int.Parse(values[9]),
                        coefficient: double.Parse(values[10]),
                        carBody: values[11],
                        leaseRate: double.Parse(values[12]),
                        comfortRate: double.Parse(values[15])
                    );
                    vehicles.Add(car);
                }
                else if (values[1] == "Truck")
                {
                    var truck = new Truck(
                        id: int.Parse(values[0]),
                        brand: values[2],
                        model: values[3],
                        yearOfManufacture: int.Parse(values[4]),
                        color: (ConsoleColor)Enum.Parse(typeof(ConsoleColor), values[5]),
                        price: decimal.Parse(values[6]),
                        registrationNumber: values[7],
                        mileage: int.Parse(values[8]),
                        durationOfService: int.Parse(values[9]),
                        coefficient: double.Parse(values[10]),
                        maxVelocity: double.Parse(values[13]),
                        maxCargoSize: double.Parse(values[14]),
                        comfortRate: double.Parse(values[15])
                    );
                    vehicles.Add(truck);
                }
            }

            Console.WriteLine($"List of vehicles has been loaded successfully from {fileName}");
            return vehicles;
        }


    }
}
