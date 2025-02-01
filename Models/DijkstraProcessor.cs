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
        private Dictionary<string, int> LocNameDictionary;
        public DijkstraAlgorithm DijkstraAlgorithm { get; private set; }

        public DijkstraProcessor()
        {
            LocationDictionary = [];
            LocNameDictionary = [];
            DijkstraAlgorithm = new();
        }
        public void SetLocDict(List<string[]> csv)
        {
            LocationDictionary = GraphCreator.GetNodeDictionary(csv);
            Dictionary<int, AbstractNode> nodeDict = [];
            foreach (Location location in LocationDictionary.Values)
            {
                int id = location.Id;
                string name = location.Name;
                nodeDict[id] = location as AbstractNode;
                LocNameDictionary[name] = id;
            }
            DijkstraAlgorithm.SetNodeDict(nodeDict);
        }
        public void Process(string fromLocation, string toLocation)
        {
            int startId = LocNameDictionary[fromLocation];
            int endId = LocNameDictionary[toLocation];
            DijkstraAlgorithm.Process(startId, endId);
        }
    }
}
