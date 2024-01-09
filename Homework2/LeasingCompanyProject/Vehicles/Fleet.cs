using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject.Vehicles
{
    public class Fleet
    {
        private List<Vehicle> fleetList;

        public Fleet()
        {
            fleetList = new List<Vehicle>();
        }

        public Fleet(List<Vehicle> fleetList)
        {
            this.fleetList = fleetList;
        }

        public void SetNewFleetList(List<Vehicle> fleetList)
        {
            this.fleetList = fleetList;
        }

        public List<Vehicle> GetFleetList()
        {
            return fleetList;
        }

        public Vehicle? FindVehicleById(int index)
        {
            return fleetList.FirstOrDefault(vehicle => vehicle.Id == index);
        }

        // colorful information about vehicle in terminal
        public void WriteVehicleWithColor(Vehicle vehicle)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = vehicle.Color;

            Console.WriteLine(vehicle.ToString());

            Console.ForegroundColor = currentColor;
        }

        // information about vehicle without color
        public void WriteVehicle(Vehicle vehicle)
        {
            Console.WriteLine(vehicle.ToString());
        }
        public void WriteAllVehicles()
        {
            Console.WriteLine("All vehicles in our fleet: ");
            if(fleetList.Count() == 0)
            {
                Console.WriteLine("Currently, we have no new vehicles");
            }
            foreach (Vehicle vehicle in fleetList)
            {
                // WriteVehicle(vehicle);
                WriteVehicleWithColor(vehicle); 
            }
        }

        public void WriteAllBrandedVehicles(String brand)
        {
            Console.WriteLine($"All Vehicles from {brand} company: ");
            bool isInList = false;
            foreach (Vehicle vehicle in fleetList)
            {
                if (vehicle.Brand == brand) { WriteVehicleWithColor(vehicle); isInList = true; }
            }
            if (!isInList) Console.WriteLine($"We have no new vehicles of {brand}");
        }

        public void WriteAllOutdatedVehicles()
        {
            Console.WriteLine("All vehicles with an exceeded a predetermined operational tenure: ");
            bool isInList = false;
            foreach (Vehicle vehicle in fleetList)
            {
                if (vehicle.IsOutdated())
                {
                    WriteVehicleWithColor(vehicle);
                    isInList = true;
                }
            }
            if (!isInList) Console.WriteLine($"All vehicles are good to rent and drive");
        }
        public void WriteAllOutdatedVehicles(String model)
        {
            Console.WriteLine($"All {model} vehicles with an exceeded a predetermined operational tenure: ");
            bool isInList = false;
            foreach (Vehicle vehicle in fleetList)
            {
                if (vehicle.Model == model && vehicle.IsOutdated())
                {
                    WriteVehicleWithColor(vehicle);
                    isInList = true;
                }
            }
            if (!isInList) Console.WriteLine($"All {model} vehicles are good to rent and drive");
        }

        public long CalculateTotalValueOfFleet()
        {
            Console.WriteLine("Current value of entire fleet: ");
            long totalValue = 0;
            foreach(Vehicle vehicle in fleetList)
            {
                Console.WriteLine($"{vehicle.Id}. ({vehicle.GetType()}) {vehicle.Brand} {vehicle.Model}: {vehicle.GetCurrentPrice()}$");
                totalValue += (long)vehicle.GetCurrentPrice();
            }
            Console.WriteLine($"Total value: {totalValue}$");
            return totalValue;
        }

        public void WriteAllVehiclesWithUpcomingInspection()
        {
            Console.WriteLine("All vehicles with an upcoming inspection: ");
            bool isInList = false;
            foreach (Vehicle vehicle in fleetList)
            {
                bool isRequired; int distanceToNextInspection;
                (isRequired, distanceToNextInspection) = vehicle.IsInspectionRequired();
                if (isRequired)
                {
                    isInList = true;
                    WriteVehicle(vehicle);
                    Console.WriteLine($"Only {distanceToNextInspection}km to next Inspection!");
                }
            }
            if(isInList) Console.WriteLine($"All vehicles are good to rent and drive");
        }

        public void SearchVehicleByBrandAndColor(String brand, ConsoleColor color)
        {
            var sortedVehiclesByComfortRate = fleetList.OrderByDescending(vehicle => vehicle.ComfortRate).ToList();

            // Exact match
            Console.WriteLine("Vehicles with best match to your preferences: ");
            var vehiclesWithBestMatch = sortedVehiclesByComfortRate.Where(vehicle => ( vehicle.Brand == brand && vehicle.Color == color)).ToList();
            foreach ( var vehicle in vehiclesWithBestMatch)
            {
                WriteVehicleWithColor(vehicle);
            }
            if(vehiclesWithBestMatch.Count() == 0)
            {
                Console.WriteLine("We dont have this specific vehicle");
            }

            // When only color or brand suit
            Console.WriteLine("But you might also like the ones below: ");
            var vehiclesWithChoosenColorXorBrand = sortedVehiclesByComfortRate.Where(vehicle => ( (vehicle.Brand == brand) ^ (vehicle.Color == color) )).ToList();
            foreach( var vehicle in vehiclesWithChoosenColorXorBrand)
            {
                WriteVehicleWithColor(vehicle);
            }
            if (vehiclesWithBestMatch.Count() == 0)
            {
                Console.WriteLine("We have no more offerts");
            }
        }

        public void WriteFastestTrucks(int velocity)
        {
            Console.WriteLine("Trucks by descending velocity: ");
            var trucks = fleetList.Where(vehicle => (vehicle is Truck) && (vehicle as Truck).MaxVelocity >= velocity).ToList();
            trucks = trucks.OrderByDescending(truck => (truck as Truck).MaxVelocity).ToList();

            if(trucks.Count == 0 ) {
                Console.WriteLine("No truck meeting the requirements");
            }

            foreach(var truck in trucks)
            {
                WriteVehicleWithColor(truck);
            }
        }

        public void WriteLargestTrucks(int cargoSize)
        {
            Console.WriteLine("Trucks by descending cargo size: ");
            var trucks = fleetList.Where(vehicle => (vehicle is Truck) && (vehicle as Truck).MaxCargoSize >= cargoSize).ToList();
            trucks = trucks.OrderByDescending(truck => (truck as Truck).MaxCargoSize).ToList();

            if (trucks.Count == 0)
            {
                Console.WriteLine("No truck meeting the requirements");
            }

            foreach (var truck in trucks)
            {
                WriteVehicleWithColor(truck);
            }
        }

    }
}
