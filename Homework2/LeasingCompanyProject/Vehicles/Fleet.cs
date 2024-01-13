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

        public void AddNewVehicle(Vehicle vehicle)
        {
            this.fleetList.Add(vehicle);
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

        public List<Vehicle> GetAllBrandedVehicles(String brand)
        {
            return fleetList.Where(vehicle => vehicle.Brand == brand).ToList();
        }

        public List<Vehicle> GetAllOutdatedVehicles()
        {
            return fleetList.Where(vehicle => vehicle.IsOutdated()).ToList();
        }

        public decimal CalculateTotalValueOfFleet()
        {
            decimal totalValue = 0;
            foreach(Vehicle vehicle in fleetList)
            {
                totalValue += (decimal)vehicle.GetCurrentPrice();
            }
            return totalValue;
        }

        public List<Vehicle> GetAllVehiclesWithUpcomingInspection()
        {
            List<Vehicle> vehiclesWithUpcomingInspection = new List<Vehicle>();
            foreach (Vehicle vehicle in fleetList)
            {
                bool isRequired; int distanceToNextInspection;
                (isRequired, distanceToNextInspection) = vehicle.IsInspectionRequired();
                if (isRequired)
                {
                    vehiclesWithUpcomingInspection.Add(vehicle);
                }
            }
            return vehiclesWithUpcomingInspection;
        }

        public List<Vehicle> SearchVehicleByBrandAndColor(String brand, ConsoleColor color)
        {
          
            var vehiclesWithBestMatch = fleetList.Where(vehicle => ( vehicle.Brand == brand && vehicle.Color == color)).ToList();
            return vehiclesWithBestMatch.OrderByDescending(vehicle => vehicle.ComfortRate).ToList();
        }

    }
}
