using Models.Graph.Elements.Abstract;

namespace Models.Graph.Elements.Concrete
{
    public class Route(Location location, int cost, string transportation)
        : AbstractEdge(location, cost)
    {
        public readonly string Transportation = transportation;

        public override string ToString()
        {
            return $"To {location.Name}, Cost: {Cost}, Transportation: {Transportation}";
        }
    }
}
