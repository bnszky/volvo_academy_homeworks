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

        public Car(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, double comfortRate, string carBody, double leaseRate) 
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber, comfortRate)
        {
            CarBody = carBody;
            LeaseRate = leaseRate;
        }

        public Car(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, double comfortRate, int mileage, int durationOfService, double coefficient, string carBody, double leaseRate)
            : base(id, brand, model, yearOfManufacture, color, price, registrationNumber, comfortRate, mileage, durationOfService, coefficient)
        {
            CarBody = carBody;
            LeaseRate = leaseRate;
        }

        public override string ToString()
        {
            return "{Car} " + base.ToString() + $", Car Body: {CarBody}, Lessee's Rate: {LeaseRate}";
        }

        public override bool IsOutdated()
        {
            return (Mileage > 1e6 || ((double)DurationOfService / 365) > 5); // mileage over 100 000km or in service over 5 years
        }

        public override decimal GetCurrentPrice()
        {
            int numberOfFullYearsInService = DurationOfService / 365;
            return Math.Max(0, Price - (decimal)(1 - Math.Pow(0.1, (double)numberOfFullYearsInService))); // initial value -= 10% per full year
        }

        public override (bool, int) IsInspectionRequired()
        {
            int distanceFromLastService = Mileage%5000;
            bool isServiceRequired = (distanceFromLastService >= 4000);
            return (isServiceRequired, 5000 - distanceFromLastService); // return additionally distance to next service
        }
    }
}
