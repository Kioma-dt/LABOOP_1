namespace LABOOP_1.Control
{
    internal class Accountant
    {
        uint _budget;

        public Accountant(uint budget)
        {
            _budget = budget;
        }

        public Accountant()
            :this(0)
        { }

        public void AddMoney(uint money)
        {
            _budget += money;
        }

        public void GetMoney(uint money) 
        {
            if (!IsMoneyEnough(money))
            {
                throw new Exception("Not enough money");
            }

            _budget -= money;
        }

        public bool IsMoneyEnough(uint money)
        {
            return _budget >= money;
        }
    }
}
