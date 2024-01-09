using LeasingCompanyProject.Vehicles;
using System.Drawing;

namespace LeasingCompanyProject
{
    internal class Program
    {
        private static string fileName = "";

        private static List<Vehicle> TryReadAndLoadData()
        {
            Console.WriteLine("Please give us a name of the file you would like to load");
            while (true)
            {
                fileName = Console.ReadLine();

                try
                {
                    List<Vehicle> loadedData = CSVFileManager.Load(fileName);
                    return loadedData;
                }
                catch
                {
                    Console.WriteLine("Ops... We have a problem with this file");
                    Console.WriteLine("Please try again");
                }
            }
        }

        private static void RentVehicleWindow(Fleet fleet)
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
                        fleet.WriteVehicleWithColor(vehicle);
                        Console.WriteLine($"Rental cost for this car equals {rentalCost}$");
                        Console.WriteLine("Are you sure, you want to rent this vehicle? y/n");
                        if ((String)Console.ReadLine() == "y")
                        {
                            vehicle.Rent(distance, durationOfTravel);
                        }

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
        private static void MenuWindow() {
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
                {(int)Options.WriteFastestTrucks}. Write trucks that are faster than the given velocity
                {(int)Options.WriteLargestTrucks}. Write trucks whose capacity is larger than the given cargo size
                {(int)Options.AddTruck}. Add new Truck
                {(int)Options.AddCar}. Add new Car
                ********************************************************************************************************
                Enter an option:
                """);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("We're really glad to see you again!");
            var loadedData = TryReadAndLoadData();
            //var loadedData = CSVFileManager.Load(fileName);

            Fleet fleet = new Fleet(loadedData);

            while (true)
            {
                MenuWindow();
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    Options option = (Options)result;
                    switch (option)
                    {
                        case Options.Exit:
                            return;
                        case Options.Load:
                            fleet.SetNewFleetList(TryReadAndLoadData());
                            break;
                        case Options.Save:
                            CSVFileManager.Save(fleet.GetFleetList(), fileName);
                            break;
                        case Options.SaveAs:
                            Console.WriteLine("Please enter name of the file with ending .csv");
                            string fileToSave = Console.ReadLine();
                            CSVFileManager.Save(fleet.GetFleetList(), fileToSave);
                            break;
                        case Options.WriteAll:
                            fleet.WriteAllVehicles();
                            break;
                        case Options.WriteSpecificBrand:
                            Console.WriteLine("Enter name of the brand: ");
                            string brand = Console.ReadLine();
                            fleet.WriteAllBrandedVehicles(brand);
                            break;
                        case Options.WriteOutdated:
                            fleet.WriteAllOutdatedVehicles();
                            break;
                        case Options.CalculateTotalValue:
                            fleet.CalculateTotalValueOfFleet();
                            break;
                        case Options.SearchByBrandAndColor:
                            Console.WriteLine("Enter name of the color");
                            var color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Console.ReadLine());
                            Console.WriteLine("Enter name of the brand");
                            brand = Console.ReadLine();
                            fleet.SearchVehicleByBrandAndColor(brand, color);
                            break;
                        case Options.CheckInspection:
                            fleet.WriteAllVehiclesWithUpcomingInspection();
                            break;
                        case Options.RentVehicle:
                            RentVehicleWindow(fleet);
                            break;
                        case Options.WriteFastestTrucks:
                            Console.WriteLine("Enter the lowest acceptable max velocity: ");
                            if(int.TryParse(Console.ReadLine(), out int maxVelocity))
                            {
                                fleet.WriteFastestTrucks(maxVelocity);
                            }
                            else
                            {
                                Console.WriteLine("Velocity must be a number");
                            }
                            break;
                        case Options.WriteLargestTrucks:
                            Console.WriteLine("Enter the lowest acceptable cargo size: ");
                            if (int.TryParse(Console.ReadLine(), out int maxCargoSize))
                            {
                                fleet.WriteLargestTrucks(maxCargoSize);
                            }
                            else
                            {
                                Console.WriteLine("Cargo size must be a number");
                            }
                            break;
                        case Options.AddTruck:
                            var truck = ConsoleVehicleCreator.CreateTruck();
                            if (truck != null)
                            {
                                fleet.AddNewVehicle(truck);
                                Console.WriteLine($"{truck.Id}. {truck.Brand} {truck.Model} has been added!");
                            }
                            break;
                        case Options.AddCar:
                            var car = ConsoleVehicleCreator.CreateCar();
                            if (car != null)
                            {
                                fleet.AddNewVehicle(car);
                                Console.WriteLine($"{car.Id}. {car.Brand} {car.Model} has been added!");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }
    }
}