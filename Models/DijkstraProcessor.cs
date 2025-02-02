using Models.Utils;
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
        public void SetLocDict(List<string[]> csv, CostType costType)
        {
            LocationDictionary = GraphCreator.GetNodeDictionary(csv, costType);
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
        public (List<Location>, List<Route>) Process(string fromLocation, string toLocation)
        {
            // 出発地点のIdと目的地のIdを取得し, Dijkstra法を実行する
            int startId = LocNameDictionary[fromLocation];
            int endId = LocNameDictionary[toLocation];
            DijkstraAlgorithm.Process(startId, endId);

            // 逆探査を行う
            (List<Location>, List<Route>) result = BackPropagate(endId);
            return result;
        }

        private (List<Location>, List<Route>) BackPropagate(int endId)
        {
            List<Location> locations = [];
            List<Route> routes = [];

            // 目的地を取得する
            Location dst = LocationDictionary[endId];
            // 逆探査を行い, 逆順でリストに格納する
            locations.Add(dst);
            Location? prev = dst.PreviousNode as Location;
            Route? route = dst.PreviousEdge as Route;

            if (route != null)
            {
                routes.Add(route);
            }
            while (prev != null)
            {
                locations.Add(prev);
                route = prev.PreviousEdge as Route;
                if (route != null)
                {
                    routes.Add(route);
                }
                prev = prev.PreviousNode as Location;
            }

            // リストを逆順にする
            locations.Reverse();
            routes.Reverse();

            return (locations, routes);
        }
    }
}
