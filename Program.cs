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
                maxStorageVolume: 10000);
            Console.WriteLine("Factory Created");

            factory.HireWorker("Avramneko", "Roman", paymentPerHour: 20);
            factory.HireWorker("Smolyan", "Mikhail", paymentPerHour: 25);
            Console.WriteLine("Workers Added");

            factory.FormBrigade("Lenina");
            Console.WriteLine("Brigade Formed");

            factory.AddWorkerToBrigade(workerId: 1, brigadeId: 1);
            factory.AddWorkerToBrigade(workerId: 2, brigadeId: 1);
            Console.WriteLine("Workers Added To Brigade");

            factory.AddMachine("Katusha", materialPerDetail: 2,  hoursPerDetail: 3);
            Console.WriteLine("Machine Added");

            factory.BuyMaterial(100, new Steel(0.07));
            Console.WriteLine("Materials Bought");

            factory.ProduceDetails(20, new Block(), new Steel(0.07));
            Console.WriteLine("Details Produced");

            var order = new Order("MAZ", 15, new Block(), 3000);
            factory.AddOrder(order);
            Console.WriteLine("Recieved Order");

            factory.CompleteOrder();
            Console.WriteLine("Order Done");
        }
    }
}
