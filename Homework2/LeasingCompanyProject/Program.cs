using LeasingCompanyProject.Vehicles;
using System.Drawing;

namespace LeasingCompanyProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var newList = CSVFileManager.Load("vehicles.csv");
            Fleet fleet = new Fleet(newList);
            fleet.WriteAllVehicles();
            Console.ReadKey();
        }
    }
}