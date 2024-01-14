using LeasingCompanyProject.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject.CargoTransportStrategies
{
    internal class CargoStandardOrder : ICargoCostPlan
    {
        private decimal PriceForTon = 500;
        public decimal CalculateCargoOrder(int distance, int durationOfTravel, int weight)
        {
            return weight * PriceForTon * distance * (decimal)0.1;
        }

        public decimal CalculateTruckCoefficient(Truck truck)
        {
            return (decimal)truck.SpecificModelCoefficient * (decimal)truck.MaxVelocity / 10;
        }

        public bool IsPossibleToDeliverOrder(Truck truck, int weight)
        {
            return (truck.MaxCargoSize >= weight);
        }
    }
}
