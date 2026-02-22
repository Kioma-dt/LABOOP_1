using LABOOP_1.Interface;

namespace LABOOP_1.Control
{
    internal class Regulator
    {
        Random rand = new Random();
        public T GetEntityById<T>(uint id, List<T> collection)
            where T : IEntity
        {
            if (!collection.Select(x => x.Id).Contains(id))
            {
                throw new Exception("No Such Entitty");
            }

            return collection.Find(x => x.Id == id)!;
       }

        public T ChooseEntityFromCollection<T>(List<T> collection)
            where T : IEntity
        {
            return collection[rand.Next(collection.Count)];
        }
    }
}
