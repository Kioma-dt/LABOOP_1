using LABOOP_1.Interface;
namespace LABOOP_1.Domain
{
    internal abstract class Detail : IStorable
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

    internal class Pipe : Detail
    {
        public Pipe()
            :base("Pipe", 10)
        { }

        public Pipe(Pipe other)
            :base(other)
        { }
    }

    internal class Block : Detail
    {
        public Block()
            : base("Block", 10)
        { }

        public Block(Block other)
            : base(other)
        { }
    }
}
