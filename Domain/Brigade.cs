using LABOOP_1.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABOOP_1.Domain
{
    internal class Brigade : IEntity
    {
        static uint _currentId = 1;

        uint _id;
        string _name;
        readonly List<Worker> _workers = new List<Worker>();

        public Brigade(uint id, string name)
        {
            _id = id;
            _name = name;
        }
        public Brigade(string name)
            :this(_currentId++, name)
        { }

        public void AddWorker(Worker worker)
        {
            if (_workers.Contains(worker))
            {
                throw new Exception($"Worker {worker.FullName} is already in brigade {Name}");
            }

            _workers.Add(worker);
        }

        public void AddWorker(uint id, string firstName, string lastName, uint paymentPerHour)
        {
            var worker = new Worker(id, firstName, lastName, paymentPerHour);

            if (_workers.Contains(worker))
            {
                throw new Exception($"Worker {worker.FullName} is already in brigade {Name}");
            }

            _workers.Add(worker);
        }

        public void DeleteWorker(Worker worker)
        {
            if (!_workers.Contains(worker))
            {
                throw new Exception($"Worker {worker.FullName} is not in brigade {Name}");
            }
            _workers.Remove(worker);
        }

        public uint CostOfBrigadesWork(uint hours)
        {
            uint cost = 0;

            foreach (Worker worker in _workers)
            {
                cost += worker.PaymentPerHour;
            }

            return cost * hours;
        }

        public uint Id => _id;
        public string Name => _name;

        public List<Worker> Workers => new List<Worker>(_workers);
    }
}
