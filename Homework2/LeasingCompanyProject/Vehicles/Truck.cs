using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject.Vehicles
{
    public class Truck : Vehicle
    {
        public double MaxVelocity { get; }
        public double MaxCargoSize { get; }

        public Truck(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, double comfortRate, double maxVelocity, double maxCargoSize)
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber, comfortRate)
        {
            MaxVelocity = maxVelocity;
            MaxCargoSize = maxCargoSize;
        }

        public Truck(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, double comfortRate, int mileage, int durationOfService, double coefficient, double maxVelocity, double maxCargoSize)
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber, comfortRate, mileage, durationOfService, coefficient)
        {
            MaxVelocity = maxVelocity;
            MaxCargoSize = maxCargoSize;
        }

        public override string ToString()
        {
            return "{Truck} " + base.ToString() + $", Max Velocity: {MaxVelocity}km/h, Max Cargo Size: {MaxCargoSize}t";
        }

        public override bool IsOutdated()
        {
            return (Mileage > 1e7 || ((double)DurationOfService / 365) > 15); // mileage over 1 000 000km or in service over 15 years
        }

        public override decimal GetCurrentPrice()
        {
            int numberOfFullYearsInService = DurationOfService / 365;
            return Math.Max(0, Price - (decimal)(1 - Math.Pow(0.07, (double)numberOfFullYearsInService))); // initial value -= 7% per full year
        }

        public override (bool, int) IsInspectionRequired()
        {
            int distanceFromLastService = Mileage % 15000;
            bool isServiceRequired = (distanceFromLastService >= 14000);
            return (isServiceRequired, 15000 - distanceFromLastService); // return additionally distance to next service
        }
    }

}
