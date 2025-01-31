using Models.Graph.Elements.Abstract;

namespace Models.Graph.Elements.Concrete
{
    public class Location(int id, string name) : AbstractNode(id)
    {
        public readonly string Name = name;

        public override string ToString()
        {
            string msg = $"{Id}: {Name}, {EdgeList.Count} Edges, Decision is {IsDecided}, Cost is {Cost}, Prev is ";
            if (PreviousNode == null)
            {
                msg += "null";
            }
            else
            {
                msg += PreviousNode.Id;
            }
            return msg;
        }
    }
}
