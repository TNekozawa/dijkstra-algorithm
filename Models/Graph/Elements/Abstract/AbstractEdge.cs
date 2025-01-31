namespace Models.Graph.Elements.Abstract
{
    public class AbstractEdge(AbstractNode to, double cost)
    {
        public readonly AbstractNode To = to;
        public readonly double Cost = cost;

        public override string ToString()
        {
            return $"To {To.Id}, Cost: {Cost}";
        }
    }
}
