using LeasingCompanyProject.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject.CargoTransportStrategies
{
    // free 10 weights and price for ton only 400$
    internal class CargoOrderWithDiscount : ICargoCostPlan
    {
        private decimal PriceForTon = 400;
        public decimal CalculateCargoOrder(int distance, int durationOfTravel, int weight)
        {
            return (weight-10) * PriceForTon * distance * (decimal)0.1;
        }

        public decimal CalculateTruckCoefficient(Truck truck)
        {
            return (decimal)truck.SpecificModelCoefficient * (decimal)truck.MaxVelocity / 10;
        }

        public bool IsPossibleToDeliverOrder(Truck truck, int weight)
        {
            return (truck.MaxCargoSize >= weight) && weight >= 15;
        }
    }
}
