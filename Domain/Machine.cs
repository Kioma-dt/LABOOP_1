namespace LABOOP_1.Domain
{
    internal class Machine
    {
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

        public void ProduceDetails(uint amountOfDetails, out uint hoursRequired, out uint materialsRequired)
        {
            hoursRequired = amountOfDetails * _hoursPerDetail;
            materialsRequired = amountOfDetails * _materialPerDetail;
        }

        public uint Id => _id;
        public string Model => _model;
        public uint MaterialPerDetail => _materialPerDetail;
        uint HoursPerDetail => _hoursPerDetail;
    }
}
