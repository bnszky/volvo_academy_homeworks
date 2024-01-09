using LeasingCompanyProject.Vehicles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject
{
    static class ConsoleVehicleCreator
    {
        private static Random random = new Random();
        private static string GenerateRegistrationNumber(int letterCount, int numberCount)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            string randomLetters = new string(Enumerable.Repeat(letters, letterCount)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string randomNumbers = new string(Enumerable.Repeat(numbers, numberCount)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomLetters + randomNumbers;
        }
        private static (int, string, string, int, ConsoleColor, decimal, string, double, int, int, double) CreateVehicleInformation()
        {
            Console.WriteLine("Id: ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.WriteLine("Brand: ");
            string brand = Console.ReadLine();

            Console.WriteLine("Model: ");
            string model = Console.ReadLine();

            Console.WriteLine("Year of manufacture: ");
            int.TryParse(Console.ReadLine(), out int yearOfManufacture);
            if (yearOfManufacture < 1950 || yearOfManufacture > 2024) throw new Exception("Year of manufacture should be between 1950-2024");

            var color = (ConsoleColor)random.Next(1,16);

            Console.WriteLine("Price: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);

            string registrationNumber = GenerateRegistrationNumber(3, 5);

            double comfortRate = Math.Round(random.NextDouble() * 10, 1);

            int mileage;
            int durationOfService;
            double coefficient;

            Console.WriteLine("Do you want to set mileage, duration of service and coefficient manually?");
            if(Console.ReadLine() == "y")
            {
                Console.WriteLine("Mileage [km]: ");
                int.TryParse(Console.ReadLine(), out mileage);
                Console.WriteLine("Duration of service [days]: ");
                int.TryParse(Console.ReadLine(), out durationOfService);
                Console.WriteLine("coefficient (0-5): ");
                double.TryParse(Console.ReadLine(), out coefficient);
                if (coefficient > 5 || coefficient < 0) { throw new Exception("Coefficient must be between 0-5"); }
            }
            else
            {
                mileage = random.Next(0, 300000);
                durationOfService = random.Next(0, 365 * 15);
                coefficient = Math.Round(random.NextDouble() / 5, 1);
            }

            return (id, brand, model, yearOfManufacture, color, price, registrationNumber, comfortRate, mileage, durationOfService, coefficient);
        }
        public static Car? CreateCar()
        {
            try
            {
                int id; string brand; string model; int yearOfManufacture; ConsoleColor color; decimal price; string registrationNumber; double comfortRate; int mileage; int durationOfService; double coefficient;
                (id, brand, model, yearOfManufacture, color, price, registrationNumber, comfortRate, mileage, durationOfService, coefficient) = CreateVehicleInformation();
                Console.WriteLine("Car body: ");
                string carBody = Console.ReadLine();
                Console.WriteLine("Lease rate: ");
                double.TryParse(Console.ReadLine(), out double leaseRate);
                if (leaseRate < 0 || leaseRate > 10) throw new Exception("Lease rate should be between 0-10");
                return new Car(
                    id: id,
                    brand: brand,
                    model: model,
                    yearOfManufacture: yearOfManufacture,
                    color: color,
                    price: price,
                    registrationNumber: registrationNumber,
                    comfortRate: comfortRate,
                    carBody: carBody,
                    leaseRate: leaseRate,
                    mileage: mileage,
                    durationOfService: durationOfService,
                    coefficient: coefficient
                    );
            } catch (Exception e){
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static Truck? CreateTruck()
        {
            try
            {
                int id; string brand; string model; int yearOfManufacture; ConsoleColor color; decimal price; string registrationNumber; double comfortRate; int mileage; int durationOfService; double coefficient;
                (id, brand, model, yearOfManufacture, color, price, registrationNumber, comfortRate, mileage, durationOfService, coefficient) = CreateVehicleInformation();
                Console.WriteLine("Max Velocity: ");
                double.TryParse(Console.ReadLine(), out double maxVelocity);
                if (maxVelocity < 30 || maxVelocity > 250) throw new Exception("Invalid value of velocity");

                Console.WriteLine("Max Cargo Size: ");
                double.TryParse(Console.ReadLine(), out double maxCargoSize);
                if (maxCargoSize < 0 || maxCargoSize > 30) throw new Exception("Invalid value of cargo size");

                return new Truck(
                    id: id,
                    brand: brand,
                    model: model,
                    yearOfManufacture: yearOfManufacture,
                    color: color,
                    price: price,
                    registrationNumber: registrationNumber,
                    comfortRate: comfortRate,
                    maxVelocity: maxVelocity,
                    maxCargoSize: maxCargoSize,
                    mileage: mileage,
                    durationOfService: durationOfService,
                    coefficient: coefficient
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
