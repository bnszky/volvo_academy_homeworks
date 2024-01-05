using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject.Vehicles
{
    public class Car : Vehicle
    {
        public string CarBody { get; }

        private double _leaseeRate;
        public double LeaseRate 
        { 
            get { return _leaseeRate; } 
            set { 
                if (value < 0 || value > 10) { _leaseeRate = 0; } 
                else { _leaseeRate = value; }
            }
        }

        public Car(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, string carBody, double leaseRate) 
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber)
        {
            CarBody = carBody;
            LeaseRate = leaseRate;
        }

        public Car(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, int mileage, int durationOfService, double coefficient, string carBody, double leaseRate)
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber, mileage, durationOfService, coefficient)
        {
            CarBody = carBody;
            LeaseRate = leaseRate;
        }

        public override string ToString()
        {
            return "{Car} " + base.ToString() + $", Car Body: {CarBody}, Lessee's Rate: {LeaseRate}";
        }

    }
}
