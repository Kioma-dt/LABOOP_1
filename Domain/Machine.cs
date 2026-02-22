using LABOOP_1.Interface;

namespace LABOOP_1.Domain
{
    internal class Machine : IEntity
    {
        protected static uint _currentId = 1;

        uint _id;
        string _model;
        uint _materialPerDetail;
        uint _hoursPerDetail;

        public Machine(uint id, string model, uint materialPerDetail, uint hoursPerDetail)
        {
            _id = id;
            _model = model;
            _materialPerDetail = materialPerDetail;
            _hoursPerDetail = hoursPerDetail;
        }
        public Machine(string model, uint materialPerDetail, uint hoursPerDetail)
            :this(_currentId++, model, materialPerDetail, hoursPerDetail)
        { }

        public virtual void ProduceDetails(uint amountOfDetails, out uint hoursRequired, out uint materialsRequired)
        {
            hoursRequired = amountOfDetails * _hoursPerDetail;
            materialsRequired = amountOfDetails * _materialPerDetail;
        }

        public uint Id => _id;
        public string Model => _model;
        public uint MaterialPerDetail => _materialPerDetail;
        uint HoursPerDetail => _hoursPerDetail;
    }

    internal class AutomatedMachine : Machine
    {
        uint _automatisationCoefficient;
        public AutomatedMachine(uint id, string model, uint materialPerDetail, uint hoursPerDetail, uint automatisationCoefficient)
            : base(id, model, materialPerDetail, hoursPerDetail)
        {
            if (automatisationCoefficient < 0 || automatisationCoefficient > 1)
            {
                throw new Exception("Wring Automatisation Coefficient");
            }

            _automatisationCoefficient = automatisationCoefficient;
        }

        public AutomatedMachine(string model, uint materialPerDetail, uint hoursPerDetail, uint automatisationCoefficient)
            :this(_currentId++, model, materialPerDetail, hoursPerDetail, automatisationCoefficient)
        { }

        public override void ProduceDetails(uint amountOfDetails, out uint hoursRequired, out uint materialsRequired)
        {
            base.ProduceDetails(amountOfDetails, out hoursRequired, out materialsRequired);

            hoursRequired *= 1 - _automatisationCoefficient;
        }
    }
}
