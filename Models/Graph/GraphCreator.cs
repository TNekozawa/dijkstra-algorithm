using Models.Graph.Elements.Concrete;
using System.Collections.Generic;

namespace Models.Graph
{
    public static class GraphCreator
    {
        public static Dictionary<int, Location> GetNodeDictionary(List<string[]> csv)
        {
            // TODO 枠の実装のみしかできていないので, 実装をやり直す
            HashSet<int> nodeIdList = [];
            foreach (string[] row in csv)
            {
                int id1 = int.Parse(row[0]);
                int id2 = int.Parse(row[1]);
                nodeIdList.Add(id1);
                nodeIdList.Add(id2);
            }

            Dictionary<int, Location> NodeDictionary = new();
            foreach (int id in nodeIdList)
            {
                Location node = new(id, "");
                NodeDictionary[id] = node;
            }

            foreach (string[] row in csv)
            {
                int id1 = int.Parse(row[0]);
                int id2 = int.Parse(row[1]);
                int cost = int.Parse(row[2]);

                Location from = NodeDictionary[id1];
                Location to = NodeDictionary[id2];

                Route edge = new(to, cost, "");
                from.SetEdge(edge);
            }
            return NodeDictionary;
        }
    }
}
