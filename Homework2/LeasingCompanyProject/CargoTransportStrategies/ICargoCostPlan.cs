using LeasingCompanyProject.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyProject.CargoTransportStrategies
{
    public interface ICargoCostPlan
    {
        public decimal CalculateTruckCoefficient(Truck truck);
        public decimal CalculateCargoOrder(int distance, int durationOfTravel, int weight);
        public bool IsPossibleToDeliverOrder(Truck truck, int weight);
    }
}
