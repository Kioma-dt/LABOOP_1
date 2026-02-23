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

        public void AddOrder(string companyName, uint amountOfdDetails,Detail detail, uint payment)
        {
            _orders.Enqueue(new Order(companyName, amountOfdDetails,detail, payment));
        }

        public (uint amountOfDetail, Detail detail) GetCurrentOrderRequirments()
        {
            if (_orders.Count == 0)
            {
                throw new Exception("No orders");
            }
            
            return (_orders.Peek().AmountOfDetails, _orders.Peek().ReqiredDetail);
        }

        public uint CompleteOrder()
        {
            if (_orders.Count == 0)
            {
                throw new Exception("No orders");
            }

            return _orders.Dequeue().Payment;
        }
    }
}
