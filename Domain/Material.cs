using System.ComponentModel;

namespace LABOOP_1.Domain
{ 
    internal class Material
    {
        uint _volume;
        uint _cost;
        string _name;

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
        public uint Cost => _cost;  
    }
}
