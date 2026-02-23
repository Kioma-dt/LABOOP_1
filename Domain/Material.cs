using System.ComponentModel;
using LABOOP_1.Interface;

namespace LABOOP_1.Domain
{ 
    internal abstract class Material : IStorable
    {
        uint _volume = 0;
        protected uint _cost;
        string _name = "Undefined Material";

        public Material(string name, uint volume, uint cost)
        {
            _name = name;
            _volume = volume;
            _cost = cost;
        }

        public Material(Material other)
        {
            _name = other._name;
            _volume = other._volume;
            _cost = other._cost;
        }

        public string Name => _name;
        public uint Volume => _volume;
        public virtual uint Cost => _cost; 
    }

    internal class Steel : Material
    {
        double _carbonCoefficient;
        public Steel(double carbonCoefficient)
            : base("Steel", 10, 15)
        {
            _carbonCoefficient = carbonCoefficient;
        }

        public Steel(Steel other)
            : base(other)
        {
            _carbonCoefficient= other._carbonCoefficient;
        }

        public override uint Cost => Convert.ToUInt32(_cost * (1 - _carbonCoefficient));
    }

    internal class Iron : Material
    {
        public Iron()
            : base("Iron", 5, 30)
        {
        }

        public Iron(Iron other)
            : base(other)
        { }    }
}
