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
            Vehicle vehicleToRemove = fleetList.FirstOrDefault(vehicle => vehicle.Id == index);
            if (vehicleToRemove != null) { fleetList.Remove(vehicleToRemove); }
        }

        public void WriteAllVehicles()
        {
            Console.WriteLine("All vehicles in our fleet: ");
            foreach (Vehicle vehicle in fleetList)
            {
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = vehicle.Color;

                Console.WriteLine(vehicle.ToString());

                Console.ForegroundColor = currentColor;
            }
        }
    }
}
