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

        public Factory(string name, uint budget, uint maxStorageVolume)
        {
            _name = name;
            _accountant = new Accountant(budget);
            _storage = new Storage(maxStorageVolume);
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
        public void AddOrder(string companyName, uint amountOfdDetails, Detail detail, uint payment)
        {
            _manager.AddOrder(companyName, amountOfdDetails, detail, payment);
        }

        public void CompleteOrder()
        { 
            (uint rquiredDetails, Detail detail) = _manager.GetCurrentOrderRequirments();
           
            _storage.TakeItem(rquiredDetails, detail);
            var payment = _manager.CompleteOrder();
            _accountant.AddMoney(payment);
        }
        public void AddWorkerToBrigade(uint workerId, uint brigadeId)
        {
            var worker = _regulator.GetEntityById<Worker>(workerId, _workers);
            var brigade = _regulator.GetEntityById<Brigade>(brigadeId, _brigades);

            CheckWorkerInBrigade(workerId);

            brigade.AddWorker(worker);

        }
        public void ProduceDetails(uint amountOfDetails, Detail detail, Material material)
        {
            var brigade = _regulator.ChooseEntityFromCollection<Brigade>(_brigades);
            var machine = _regulator.ChooseEntityFromCollection<Machine>(_machines);
            uint amountOfMaterials, hours;

            machine.ProduceDetails(amountOfDetails,out hours, out amountOfMaterials);
            var cost = brigade.CostOfBrigadesWork(hours);

            CheckProduction(amountOfDetails, detail, cost, amountOfMaterials, material);

            _accountant.GetMoney(cost);
            _storage.TakeItem(amountOfMaterials, material);
            _storage.AddItem(amountOfDetails, detail);
        }

        public void BuyMaterial(uint amountOfMaterial, Material material) 
        {
            var cost = material.Cost * amountOfMaterial;
            CheckMaterialPurchase(amountOfMaterial, material, cost);

            _storage.AddItem(amountOfMaterial, material);
            _accountant.GetMoney(cost);
        }

        public string Name => _name;

        void CheckWorkerInBrigade(uint workerId)
        {
            foreach (var brigade in _brigades)
            {
                foreach (var workers in brigade.Workers)
                {
                    if (workers.Id == workerId)
                    {
                        throw new Exception("Worker is already in some Brigade");
                    }
                }
            }
        }

        void CheckProduction(uint amountOfDetails, Detail detail, uint cost, uint amountOfMaterials, Material material)
        {
            if (!_storage.IsFittingItem(amountOfDetails, detail))
            {
                throw new Exception("Details Not Fitting");
            }

            if (!_storage.IsItemEnough(amountOfMaterials, material))
            {
                throw new Exception("Not enough material");
            }

            if (!_accountant.IsMoneyEnough(cost))
            {
                throw new Exception("Not enought money");
            }
        }

        void CheckMaterialPurchase(uint amountOfMaterial, Material material, uint cost)
        {
            if (!_storage.IsFittingItem(amountOfMaterial, material))
            {
                throw new Exception("Material Not Fitting");
            }

            if (!_accountant.IsMoneyEnough(cost))
            {
                throw new Exception("Not enought money");
            }
        }
    }
}
