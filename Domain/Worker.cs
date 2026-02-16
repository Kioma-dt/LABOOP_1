namespace LABOOP_1.Domain
{
    internal class Worker
    {
        uint _id;
        string _firstName;
        string _lastName;
        uint _paymentPerHour;

        public Worker(uint id, string firstName, string lastName, uint paymentPerHour)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _paymentPerHour = paymentPerHour;
        }

        public uint Id => _id;
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string FullName => _firstName + " " + _lastName;
        public uint PaymentPerHour => _paymentPerHour;
    }
}
