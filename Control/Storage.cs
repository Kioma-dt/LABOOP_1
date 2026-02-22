using LABOOP_1.Domain;

namespace LABOOP_1.Control
{
    internal class Storage
    {
        uint _maxMaterialsVolume;
        uint _maxDetailsVolume;
        uint _amountOfMaterials;
        uint _amountOfDetails;

        Material _material;
        Detail _detail;

        public Storage(uint  maxMaterialsVolume, uint maxDetailsVolume, Material material, Detail detail)
        {
            _maxMaterialsVolume = maxMaterialsVolume;
            _maxDetailsVolume = maxDetailsVolume;
            _material = material;
            _detail = detail;
        }

        public void AddMaterial(uint amount)
        {
            if (amount * _material.Volume > FreeVolumeForMaterial)
            {
                throw new Exception("Not enought space for material");
            }

            _amountOfMaterials += amount;
        }

        public void TakeMaterila(uint amount)
        {
            if(amount > _amountOfMaterials)
            {
                throw new Exception("Not enought material");
            }

            _amountOfMaterials -= amount;
        }

        public uint GetMaterialCost(uint amount)
        {
            return amount * _material.Cost;
        }

        public void AddDetail(uint amount)
        {
            if (amount * _detail.Volume > FreeVolumeForDetail)
            {
                throw new Exception("Not enought space for detail");
            }

            _amountOfDetails += amount;
        }

        public void TakeDetail(uint amount)
        {
            if (amount > _amountOfDetails)
            {
                throw new Exception("Not enought detail");
            }

            _amountOfDetails -= amount;
        }

        public bool IsFittingMaterial(uint amountOfMaterial)
        {
            return FreeVolumeForMaterial >= amountOfMaterial * _material.Volume;
        }

        public bool IsFittingDetail(uint amountOfDetail)
        {
            return FreeVolumeForDetail >= amountOfDetail * _detail.Volume;
        }

        public bool IsMaterialEnough(uint amountOfMaterial)
        {
            return _amountOfMaterials >= amountOfMaterial; 
        }
        public bool IsDetailEnough(uint amountOfDetail)
        {
            return _amountOfDetails >= amountOfDetail;
        }
        private uint FreeVolumeForMaterial => _maxMaterialsVolume - _amountOfMaterials * _material.Volume;
        private uint FreeVolumeForDetail => _maxDetailsVolume - _amountOfDetails * _detail.Volume;
    }
}
