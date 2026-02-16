namespace LABOOP_1.Domain
{
    internal class Detail
    {
        string _name;
        uint _volume;

        public Detail(string name, uint volume)
        {
            _name = name;
            _volume = volume;
        }

        public Detail(Detail other)
        {
            _name = other._name;
            _volume = other._volume;
        }

        public string Name => _name;
        public uint Volume => _volume;
    }
}
