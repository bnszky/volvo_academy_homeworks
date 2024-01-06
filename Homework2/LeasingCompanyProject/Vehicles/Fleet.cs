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
        public void AddNewVehicle(Vehicle vehicle)
        {
            fleetList.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            fleetList.Remove(vehicle);
        }

        public void RemoveVehicleById(int index)
        {
            Vehicle? vehicleToRemove = fleetList.FirstOrDefault(vehicle => vehicle.Id == index);
            if (vehicleToRemove != null) { fleetList.Remove(vehicleToRemove); }
        }

        // colorful information about vehicle in terminal
        private void WriteVehicleWithColor(Vehicle vehicle)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = vehicle.Color;

            Console.WriteLine(vehicle.ToString());

            Console.ForegroundColor = currentColor;
        }

        // information about vehicle without color
        private void WriteVehicle(Vehicle vehicle)
        {
            Console.WriteLine(vehicle.ToString());
        }
        public void WriteAllVehicles()
        {
            Console.WriteLine("All vehicles in our fleet: ");
            foreach (Vehicle vehicle in fleetList)
            {
                // WriteVehicle(vehicle);
                WriteVehicleWithColor(vehicle); 
            }
        }

        public void WriteAllBrandedVehicles(String brand)
        {
            Console.WriteLine($"All Vehicles from {brand} company: ");
            foreach (Vehicle vehicle in fleetList)
            {
                if (vehicle.Brand == brand) { WriteVehicleWithColor(vehicle); }
            }
        }

        public void WriteAllOutdatedVehicles()
        {
            foreach (Vehicle vehicle in fleetList)
            {
                if (vehicle.IsOutdated())
                {
                    WriteVehicleWithColor(vehicle);
                }
            }
        }
        public void WriteAllOutdatedVehicles(String model)
        {
            foreach (Vehicle vehicle in fleetList)
            {
                if (vehicle.Model == model && vehicle.IsOutdated())
                {
                    WriteVehicleWithColor(vehicle);
                }
            }
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
            foreach(Vehicle vehicle in fleetList)
            {
                bool isRequired; int distanceToNextInspection;
                (isRequired, distanceToNextInspection) = vehicle.IsInspectionRequired();
                if (isRequired)
                {
                    WriteVehicle(vehicle);
                    Console.WriteLine($"Only {distanceToNextInspection}km to next Inspection!");
                }
            }
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

            // When only color or brand suit
            Console.WriteLine("But you might also like the ones below: ");
            var vehiclesWithChoosenColorXorBrand = sortedVehiclesByComfortRate.Where(vehicle => ( (vehicle.Brand == brand) ^ (vehicle.Color == color) )).ToList();
            foreach( var vehicle in vehiclesWithChoosenColorXorBrand)
            {
                WriteVehicleWithColor(vehicle);
            }
        }



    }
}
