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
        static void Main(string[] args)
        {
            Console.WriteLine("We're really glad to see you again!");
            var loadedData = TryReadAndLoadData();
            //var loadedData = CSVFileManager.Load(fileName);

            Fleet fleet = new Fleet(loadedData);

            while (true)
            {
                ConsoleManager.MenuWindow();
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
                            ConsoleManager.WriteFleet(
                                "All vehicles in our fleet:",
                                fleet.GetFleetList(),
                                ConsoleManager.WriteVehicleWithColor
                            );
                            break;
                        case Options.WriteSpecificBrand:
                            Console.WriteLine("Enter name of the brand: ");
                            string brand = Console.ReadLine();
                            ConsoleManager.WriteFleet(
                                $"All Vehicles from {brand} company: ",
                                fleet.GetAllBrandedVehicles(brand),
                                ConsoleManager.WriteVehicleWithColor,
                                $"We have no new vehicles of {brand}"
                            );
                            break;
                        case Options.WriteOutdated:
                            ConsoleManager.WriteFleet(
                                "All vehicles with an exceeded a predetermined operational tenure: ",
                                fleet.GetAllOutdatedVehicles(),
                                ConsoleManager.WriteVehicleWithColor,
                                $"All vehicles are good to rent and drive"
                            );
                            break;
                        case Options.CalculateTotalValue:
                            ConsoleManager.WriteFleet(
                                $"Current value of entire fleet: {Math.Round(fleet.CalculateTotalValueOfFleet(), 2)}",
                                fleet.GetFleetList(),
                                ConsoleManager.WriteVehicleWithCurrentValue
                            );
                            break;
                        case Options.SearchByBrandAndColor:
                            Console.WriteLine("Enter name of the color");
                            var color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Console.ReadLine());
                            Console.WriteLine("Enter name of the brand");
                            brand = Console.ReadLine();
                            fleet.SearchVehicleByBrandAndColor(brand, color);
                            ConsoleManager.WriteFleet(
                                "Vehicles with best match to your preferences: ",
                                fleet.SearchVehicleByBrandAndColor(brand, color),
                                ConsoleManager.WriteVehicleWithColor,
                                "We dont have this specific vehicle"
                            );
                            break;
                        case Options.CheckInspection:
                            ConsoleManager.WriteFleet(
                                "All vehicles with an upcoming inspection: ",
                                fleet.GetAllVehiclesWithUpcomingInspection(),
                                (Vehicle vehicle) =>
                                {
                                    ConsoleManager.WriteVehicleWithColor(vehicle);
                                    int distanceToNextInspection;
                                    (_, distanceToNextInspection) = vehicle.IsInspectionRequired();
                                    Console.WriteLine($"Only {distanceToNextInspection}km to next Inspection!");
                                },
                                $"All vehicles are good to rent and drive"
                            );
                            break;
                        case Options.RentVehicle:
                            ConsoleManager.RentVehicleWindow(fleet);
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