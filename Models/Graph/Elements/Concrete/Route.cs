using Models.Graph.Elements.Abstract;

namespace Models.Graph.Elements.Concrete
{
    public class Route(Location toLocation, int cost, string transportation, int fare, int time)
        : AbstractEdge(toLocation, cost)
    {
        public readonly string Transportation = transportation;
        public readonly int Fare = fare;
        public readonly int Time = time;
        public override string ToString()
        {
            return $"To {toLocation.Name}, Cost: {Cost}, Transportation: {Transportation}";
        }
    }
}
