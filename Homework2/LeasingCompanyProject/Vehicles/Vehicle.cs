using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject.Vehicles
{
    public abstract class Vehicle
    {
        public int Id { get; }
        public string Brand { get; }
        public string Model { get; }
        public int YearOfManufacture { get; }

        public ConsoleColor Color { get; set; }
        public decimal Price { get; set; }
        public string RegistrationNumber { get; set; }

        private int _mileage;
        private int _durationOfService;
        private double _coefficient;
        private double _comfortRate;
        public int Mileage { 
            get { return _mileage; }
            set
            {
                if (value < 0) { _mileage = 0; }
                else { _mileage = value; }
            } 
        }

        public int DurationOfService
        {
            get { return _durationOfService; }
            set 
            { 
                if (value < 0) { _durationOfService = 0; }
                else { _durationOfService = value; }
            }
        }
        public double Coefficient
        {
            set 
            { 
                if (_coefficient < 0 || _coefficient > 5) { _coefficient = 0; }
                else
                {
                    _coefficient = value;
                }
            }
            get { return _coefficient; }
        }

        public double ComfortRate
        {
            set
            {
                if (_comfortRate < 0 || _comfortRate > 10) { _comfortRate = 0; }
                else
                {
                    _comfortRate = value;
                }
            }
            get { return _comfortRate; }
        }

        public Vehicle(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, double comfortRate) {
            Id = id;
            Brand = brand;
            Model = model;
            YearOfManufacture = yearOfManufacture;
            Color = color;
            Price = price;
            RegistrationNumber = registrationNumber;
            Mileage = 0;
            DurationOfService = 0;
            Coefficient = 1;
            ComfortRate = comfortRate;
        }

        public Vehicle(int id, string brand, string model, int yearOfManufacture, ConsoleColor color, decimal price, string registrationNumber, double comfortRate, int mileage, int durationOfService, double coefficient)
        {
            Id = id;
            Brand = brand;
            Model = model;
            YearOfManufacture = yearOfManufacture;
            Color = color;
            Price = price;
            RegistrationNumber = registrationNumber;
            Mileage = mileage;
            DurationOfService = durationOfService;
            Coefficient = coefficient;
            ComfortRate = comfortRate;
        }

        public override string ToString()
        {
            return $"{Id}. {Brand} {Model} - {YearOfManufacture} - {Price}$ - {RegistrationNumber}: Mileage: {Mileage}km, In service: {DurationOfService} days, Coefficient: {Coefficient}";
        }

        public abstract bool IsOutdated();
        public abstract decimal GetCurrentPrice();
        public abstract (bool, int) IsInspectionRequired();

    }

}
