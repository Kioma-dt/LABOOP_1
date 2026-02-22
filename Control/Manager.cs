using LABOOP_1.Domain;

namespace LABOOP_1.Control
{
    internal class Manager
    {
        readonly Queue<Order> _orders = new Queue<Order>();

        public Manager() { }

        public void AddOrder(Order order)
        {
            _orders.Enqueue(order);
        }

        public void AddOrder(string companyName, uint amountOfdDetails, uint payment)
        {
            _orders.Enqueue(new Order(companyName, amountOfdDetails, payment));
        }

        public uint GetCurrentOrderRequirments()
        {
            if (_orders.Count == 0)
            {
                throw new Exception("No orders");
            }
            
            return _orders.Peek().AmountOfDetails;
        }

        public uint CompleteOrder()
        {
            if (_orders.Count == 0)
            {
                throw new Exception("No orders");
            }

            return _orders.Dequeue().Payment;
        }

        public void CheckWorkerInBrigade(uint workerId, List<Brigade> brigades)
        {
            foreach (var brigade in brigades)
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

        public void CheckProduction(uint amountOfDetails, uint cost, uint amountOfMaterials, Storage storage, Accountant accountant)
        {
            if (!storage.IsFittingDetail(amountOfDetails))
            {
                throw new Exception("Details Not Fitting");
            }

            if (!storage.IsMaterialEnough(amountOfMaterials))
            {
                throw new Exception("Not enough material");
            }

            if (!accountant.IsMoneyEnough(cost))
            {
                throw new Exception("Not enought money");
            }
        }

        public void CheckMaterialPurchase(uint amountOfMaterial, uint cost, Storage storage, Accountant accountant)
        {
            if (!storage.IsFittingMaterial(amountOfMaterial))
            {
                throw new Exception("Material Not Fitting");
            }

            if (!accountant.IsMoneyEnough(cost))
            {
                throw new Exception("Not enought money");
            }
        }
    }
}
