using LABOOP_1.Control;
using LABOOP_1.Domain;

namespace LABOOP_1.Core
{
    internal class Factory
    {
        Random rand = new Random();
        string _name;
        Accountant _accountant;
        Storage _storage;
        Manager _manager;

        uint _workerId;
        uint _brigadeId;
        uint _machineId;

        readonly List<Worker> _workers = new List<Worker>();
        readonly List<Brigade> _brigades = new List<Brigade>();
        readonly List<Machine> _machines = new List<Machine>();

        readonly Queue<Order> _orders = new Queue<Order>();

        public Factory(string name, uint budget, Material material, uint maxMaterial,Detail detail, uint maxDetail)
        {
            _name = name;
            _accountant = new Accountant(budget);
            _storage = new Storage(maxMaterial, maxDetail, new Material(material), new Detail(detail));
            _manager = new Manager();

            _brigadeId = 1;
            _machineId = 1;
            _workerId = 1;
        }

        private uint NextWorkerId => _workerId++;
        private uint NextBrigadeId => _brigadeId++;
        private uint NextMachineId => _machineId++;

        public void HireWorker(string workerFirstName, string workerLastName, uint paymentPerHour)
        {
            _workers.Add(new Worker(NextWorkerId, workerFirstName, workerLastName, paymentPerHour));
        }

        public void FireWorker(uint workerId)
        {
            GetWorkerById(workerId);
            _workers.RemoveAt(_workers.FindIndex(x => x.Id == workerId));
        }

        public void FormBrigade(string brigadeName)
        {
            _brigades.Add(new Brigade(NextBrigadeId, brigadeName));
        }

        public void UnFormBrigade(uint brigadeId)
        {
            GetBrigadeById(brigadeId);
            _brigades.RemoveAt(_brigades.FindIndex(x => x.Id == brigadeId));
        }

        public void AddMachine(string model, uint materialPerDetail, uint hoursPerDetail)
        {
            _machines.Add(new Machine(NextMachineId, model, materialPerDetail, hoursPerDetail));
        }

        public void ThrowMachine(uint machineId)
        {
            GetMachineById(machineId);
            _machines.RemoveAt(_machines.FindIndex(x => x.Id == machineId));
        }

        public void AddOrder(Order order)
        {
            _orders.Enqueue(new Order(order));
        }

        public void CompleteOrder()
        {
            var order = _manager.GetCurrentOrder(_orders);

            uint rquiredDetails = order.AmountOfDetails;
           
            _storage.TakeDetail(rquiredDetails);
            _accountant.AddMoney(order.Payment);

            _orders.Dequeue();
        }
        public void AddWorkerToBrigade(uint workerId, uint brigadeId)
        {
            var worker = GetWorkerById(workerId);
            var brigade = GetBrigadeById(brigadeId);

            _manager.CheckWorkerInBrigade(workerId, _brigades);

            brigade.AddWorker(worker);

        }
        public void ProduceDetails(uint amountOfDetails)
        {
            var brigade = ChooseBrigade(_brigades);
            var machine = ChooseMachine(_machines);
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

        private Worker GetWorkerById(uint id)
        {
            if (!_workers.Select(x => x.Id).Contains(id))
            {
                throw new Exception("No Such Worker");
            }

            return _workers.Find(x => x.Id == id);
        }
        private Brigade GetBrigadeById(uint id)
        {
            if (!_brigades.Select(x => x.Id).Contains(id))
            {
                throw new Exception("No Such Brigade");
            }

            return _brigades.Find(x => x.Id == id);
        }
        private Machine GetMachineById(uint id)
        {
            if (!_machines.Select(x => x.Id).Contains(id))
            {
                throw new Exception("No Such Machine");
            }

            return _machines.Find(x => x.Id == id);
        }

        private Brigade ChooseBrigade(List<Brigade> brigades)
        {
            return brigades[rand.Next(brigades.Count)];
        }

        private Machine ChooseMachine(List<Machine> machine)
        {
            return machine[rand.Next(machine.Count)];
        }

    }
}
