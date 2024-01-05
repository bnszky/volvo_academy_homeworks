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

        public Truck(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, double maxVelocity, double maxCargoSize)
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber)
        {
            MaxVelocity = maxVelocity;
            MaxCargoSize = maxCargoSize;
        }

        public Truck(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, int mileage, int durationOfService, double coefficient, double maxVelocity, double maxCargoSize)
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber, mileage, durationOfService, coefficient)
        {
            MaxVelocity = maxVelocity;
            MaxCargoSize = maxCargoSize;
        }

        public override string ToString()
        {
            return "{Truck} " + base.ToString() + $", Max Velocity: {MaxVelocity}km/h, Max Cargo Size: {MaxCargoSize}t";
        }
    }

}
