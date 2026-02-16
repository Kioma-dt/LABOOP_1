using LABOOP_1.Core;
using LABOOP_1.Domain;

namespace LABOOP_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new Factory(
                name: "My Factory",
                budget: 10000,
                material: new Material(2, 10),  
                maxMaterial: 1000,
                detail: new Detail(3),
                maxDetail: 1000);

            factory.HireWorker("Avramneko", "Roman", paymentPerHour: 20);
            factory.HireWorker("Smolyan", "Mikhail", paymentPerHour: 25);

            factory.FormBrigade("Lenina");

            factory.AddWorkerToBrigade(workerId: 1, brigadeId: 1);
            factory.AddWorkerToBrigade(workerId: 2, brigadeId: 1);

            factory.AddMachine("Katusha", materialPerDetail: 2,  hoursPerDetail: 3);

            factory.BuyMaterial(100);

            factory.ProduceDetails(20);

            var order = new Order("MAZ", 15, 3000);
            factory.AddOrder(order);

            factory.CompleteOrder();
            Console.WriteLine("Order Done");
        }
    }
}
