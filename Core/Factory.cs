using LABOOP_1.Control;
using LABOOP_1.Domain;
using LABOOP_1.Interface;

namespace LABOOP_1.Core
{
    internal class Factory
    {
        string _name;
        Accountant _accountant;
        Storage _storage;
        Manager _manager;
        Regulator _regulator;

        readonly List<Worker> _workers = new List<Worker>();
        readonly List<Brigade> _brigades = new List<Brigade>();
        readonly List<Machine> _machines = new List<Machine>();

        public Factory(string name, uint budget, Material material, uint maxMaterial,Detail detail, uint maxDetail)
        {
            _name = name;
            _accountant = new Accountant(budget);
            _storage = new Storage(maxMaterial, maxDetail, new Material(material), new Detail(detail));
            _manager = new Manager();
            _regulator = new Regulator();
        }

        public void HireWorker(string workerFirstName, string workerLastName, uint paymentPerHour)
        {
            _workers.Add(new Worker(workerFirstName, workerLastName, paymentPerHour));
        }

        public void FireWorker(uint workerId)
        {
            _regulator.GetEntityById<Worker>(workerId, _workers);
            _workers.RemoveAt(_workers.FindIndex(x => x.Id == workerId));
        }

        public void FormBrigade(string brigadeName)
        {
            _brigades.Add(new Brigade(brigadeName));
        }

        public void UnFormBrigade(uint brigadeId)
        {
            _regulator.GetEntityById<Brigade>(brigadeId, _brigades);
            _brigades.RemoveAt(_brigades.FindIndex(x => x.Id == brigadeId));
        }

        public void AddMachine(string model, uint materialPerDetail, uint hoursPerDetail)
        {
            _machines.Add(new Machine(model, materialPerDetail, hoursPerDetail));
        }

        public void ThrowMachine(uint machineId)
        {
            _regulator.GetEntityById<Machine>(machineId, _machines);
            _machines.RemoveAt(_machines.FindIndex(x => x.Id == machineId));
        }

        public void AddOrder(Order order)
        {
            _manager.AddOrder(order);
        }
        public void AddOrder(string companyName, uint amountOfdDetails, uint payment)
        {
            _manager.AddOrder(companyName, amountOfdDetails, payment);
        }

        public void CompleteOrder()
        { 
            uint rquiredDetails = _manager.GetCurrentOrderRequirments();
           
            _storage.TakeDetail(rquiredDetails);
            var payment = _manager.CompleteOrder();
            _accountant.AddMoney(payment);
        }
        public void AddWorkerToBrigade(uint workerId, uint brigadeId)
        {
            var worker = _regulator.GetEntityById<Worker>(workerId, _workers);
            var brigade = _regulator.GetEntityById<Brigade>(brigadeId, _brigades);

            _manager.CheckWorkerInBrigade(workerId, _brigades);

            brigade.AddWorker(worker);

        }
        public void ProduceDetails(uint amountOfDetails)
        {
            var brigade = _regulator.ChooseEntityFromCollection<Brigade>(_brigades);
            var machine = _regulator.ChooseEntityFromCollection<Machine>(_machines);
            uint amountOfMaterials, hours;

            machine.ProduceDetails(amountOfDetails,out hours, out amountOfMaterials);
            var cost = brigade.CostOfBrigadesWork(hours);

            _manager.CheckProduction(amountOfDetails, cost, amountOfMaterials, _storage, _accountant);

            _accountant.GetMoney(cost);
            _storage.TakeMaterila(amountOfMaterials);
            _storage.AddDetail(amountOfDetails);
        }

        public void BuyMaterial(uint amountOfMaterial) 
        {
            var cost = _storage.GetMaterialCost(amountOfMaterial);
            _manager.CheckMaterialPurchase(amountOfMaterial, cost, _storage, _accountant);

            _storage.AddMaterial(amountOfMaterial);
            _accountant.GetMoney(cost);
        }

        public string Name => _name;         
    }
}
