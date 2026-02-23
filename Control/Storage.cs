using LABOOP_1.Domain;
using LABOOP_1.Interface;
using System.Runtime.CompilerServices;

namespace LABOOP_1.Control
{
    internal class Storage
    {
        uint _maxVolume;
        uint _occupiedVolume;
        Dictionary<string, uint> _items = new Dictionary<string, uint>();

        public Storage(uint  maxVolume)
        {
            _maxVolume = maxVolume;
            _occupiedVolume = 0;
        }

        public void AddItem(uint amount, IStorable item)
        {
            if (amount * item.Volume > FreeVolume)
            {
                throw new Exception("Not enought space");
            }

            if (_items.ContainsKey(item.Name))
            {
                _items[item.Name] += amount;
            }
            else
            {
                _items[item.Name] = amount;
            }

            _occupiedVolume += amount * item.Volume;
        }

        public void TakeItem(uint amount, IStorable item)
        {
            if((!_items.ContainsKey(item.Name)) || (_items[item.Name] < amount))
            {
                throw new Exception("Not enought items");
            }

            _items[item.Name] -= amount;
        }    

        public bool IsFittingItem(uint amount, IStorable item)
        {
            return FreeVolume >= amount * item.Volume;
        }
       
        public bool IsItemEnough(uint amount, IStorable item)
        {
            return (_items.ContainsKey(item.Name)) && (_items[item.Name] >= amount); 
        }

        private uint FreeVolume => _maxVolume - _occupiedVolume;
    }
}
