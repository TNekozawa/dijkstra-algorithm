using Models.Graph;
using Models.Graph.Algorithms;
using Models.Graph.Elements.Abstract;
using Models.Graph.Elements.Concrete;
using System.Collections.Generic;

namespace Models
{
    public class DijkstraProcessor
    {
        public Dictionary<int, Location> LocationDictionary { get; private set; }
        public DijkstraAlgorithm DijkstraAlgorithm { get; private set; }

        public DijkstraProcessor()
        {
            LocationDictionary = [];
            DijkstraAlgorithm = new();
        }
        public void SetLocDict(List<string[]> csv)
        {
            LocationDictionary = GraphCreator.GetNodeDictionary(csv);
            Dictionary<int, AbstractNode> nodeDict = [];
            foreach (Location location in LocationDictionary.Values)
            {
                int id = location.Id;
                nodeDict[id] = location as AbstractNode;
            }
            DijkstraAlgorithm.SetNodeDict(nodeDict);
        }
        public void Process(int startId)
        {
            DijkstraAlgorithm.Process(startId);
        }
    }
}
