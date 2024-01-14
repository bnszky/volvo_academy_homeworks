using LeasingCompanyProject.CargoTransportStrategies;
using LeasingCompanyProject.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject
{
    static class ConsoleManager {

        public delegate void function(Vehicle vehicle);

        // colorful information about vehicle in terminal
        public static void WriteVehicleWithColor(Vehicle vehicle)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = vehicle.Color;

            Console.WriteLine(vehicle.ToString());

            Console.ForegroundColor = currentColor;
        }

        // information about vehicle without color
        public static void WriteVehicle(Vehicle vehicle)
        {
            Console.WriteLine(vehicle.ToString());
        }

        public static void WriteVehicleWithCurrentValue(Vehicle vehicle)
        {
            Console.WriteLine($"{vehicle.ToString()} - current price: {Math.Round(vehicle.GetCurrentPrice(), 2)}");
        }
        public static void WriteFleet(String info, List<Vehicle> vehicles, function func = null, String textIfEmpty = "Currently, we have no new vehicles")
        {
            Console.WriteLine(info);
            if (vehicles.Count() == 0)
            {
                Console.WriteLine(textIfEmpty);
            }
            foreach (Vehicle vehicle in vehicles)
            {
                func = func ?? WriteVehicle;
                func(vehicle);
            }
        }
        public static void RentVehicleWindow(Fleet fleet)
        {
            Console.WriteLine("Enter id of choosen vehicle");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                var vehicle = fleet.FindVehicleById(index);
                if (vehicle != null)
                {
                    Console.WriteLine("Enter distance in km");
                    int.TryParse(Console.ReadLine(), out int distance);
                    Console.WriteLine("Enter duration of rent in days");
                    int.TryParse(Console.ReadLine(), out int durationOfTravel);
                    decimal? rentalCost = vehicle.CalculateRentCost(distance, durationOfTravel);
                    if (rentalCost != null)
                    {
                        ConsoleManager.WriteVehicleWithColor(vehicle);
                        Console.WriteLine($"Rental cost for this car equals {rentalCost}$");
                        Console.WriteLine("Are you sure, you want to rent this vehicle? y/n");
                        if (Console.ReadLine() == "y")
                        {
                            string info = vehicle.Rent(distance, durationOfTravel);

                            Console.WriteLine(info);
                        }
                    } else
                    {
                        Console.WriteLine("Distance and duration must be positive values!");
                    }
                }
                else
                {
                    Console.WriteLine($"Vehicle with an index {index} doesn't exist");
                }
            }
            else
            {
                Console.WriteLine("Invalid id. Please enter a number.");
            }
        }

        public static void TryCargoOrder(Fleet fleet, ICargoCostPlan cargoPlan)
        {
            Console.WriteLine("Enter id of a choosen vehicle");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                var vehicle = fleet.FindVehicleById(index);
                if (vehicle != null && vehicle is Truck)
                {
                    Console.WriteLine("Enter distance in km");
                    int.TryParse(Console.ReadLine(), out int distance);
                    Console.WriteLine("Enter duration of rent in days");
                    int.TryParse(Console.ReadLine(), out int durationOfTravel);
                    Console.WriteLine("Enter weight of cargo");
                    int.TryParse(Console.ReadLine(), out int weight);
                    if(cargoPlan.IsPossibleToDeliverOrder((Truck)vehicle, weight))
                    {
                        decimal cargoPrice = cargoPlan.CalculateTruckCoefficient((Truck)vehicle) * cargoPlan.CalculateCargoOrder(distance, durationOfTravel, weight);
                        Console.WriteLine($"Your order is in a queue and will be realized in next days. Price: {cargoPrice}");
                    } 
                    else
                    {
                        Console.WriteLine("It isn't possible to deliver a cargo with this order plan");
                    }
                }
                else
                {
                    Console.WriteLine($"Truck with an index {index} doesn't exist");
                }
            }
            else
            {
                Console.WriteLine("Invalid id. Please enter a number.");
            }
        }
        public static void MenuWindow()
        {
            Console.WriteLine($"""
                ********************************************************************************************************
                                                                  MENU
                ********************************************************************************************************
                {(int)Options.Exit}. Exit
                {(int)Options.Load}. Load
                {(int)Options.Save}. Save
                {(int)Options.SaveAs}. Save as
                {(int)Options.WriteAll}. Write all vehicles in our fleet
                {(int)Options.WriteSpecificBrand}. Write all vehicles of a specific brand
                {(int)Options.WriteOutdated}. Write all vehicles with an exceeded predetermined operational tenure
                {(int)Options.CalculateTotalValue}. Calculate the total value of the entire vehicle fleet
                {(int)Options.SearchByBrandAndColor}. Search vehicles by chosen color and brand
                {(int)Options.CheckInspection}. Show all vehicles whose inspection will take place in the next 1000 km
                {(int)Options.RentVehicle}. Rent a vehicle with a specific id
                {(int)Options.AddTruck}. Add new Truck
                {(int)Options.AddCar}. Add new Car
                {(int)Options.CargoOrder}. Order cargo (500$ for 1 ton)
                {(int)Options.CargoOrderWithDiscount}. Order cargo (400$ for 1 ton, 10 tons for free if you order more than 14 tons weight)
                ********************************************************************************************************
                Enter an option:
                """);
        }
    }
}
