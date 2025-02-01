using Models.Graph.Elements.Abstract;
using System;
using System.Collections.Generic;

namespace Models.Graph.Algorithms
{
    public class DijkstraAlgorithm()
    {
        public Dictionary<int, AbstractNode> NodeDictionary { get; private set; } = [];
        public SortedQueue<AbstractNode> SortedQueue { get; private set; } = new();

        public void SetNodeDict(Dictionary<int, AbstractNode> nodeDictionary)
        {
            NodeDictionary = nodeDictionary;
        }

        private void InitializeProcessor(int startId)
        {
            SortedQueue.Clear();

            foreach (AbstractNode node in NodeDictionary.Values)
            {
                node.SetCost(double.MaxValue);
                node.SetDecision(false);
                node.SetPreviousNode(null);
                SortedQueue.Add(node);
            }
            AbstractNode startNode = NodeDictionary[startId];
            startNode.SetCost(0);

            SortedQueue.Sort();
        }

        public void Process(int startId, int endId)
        {
            InitializeProcessor(startId);
            while (SortedQueue.Count() > 0)
            {
                AbstractNode node = SortedQueue.Pop();
                node.SetDecision(true);
                if (node.Id == endId)
                {
                    break;
                }

                double nowCost = node.Cost;
                foreach (AbstractEdge edge in node.EdgeList)
                {
                    AbstractNode next = edge.To;
                    if (next.IsDecided)
                    {
                        continue;
                    }
                    else
                    {
                        double nextCost = next.Cost;
                        double edgeCost = edge.Cost;

                        if (nowCost + edgeCost < nextCost)
                        {
                            next.SetCost(nowCost + edgeCost);
                            next.SetPreviousNode(node);
                        }
                        else
                        {
                            // 処理なし
                        }
                    }
                }
                SortedQueue.Sort();
            }
        }

        public List<int> GetPath(int startId)
        {
            List<int> path = [];
            AbstractNode start = NodeDictionary[startId];

            path.Add(startId);
            AbstractNode? next = start.PreviousNode;
            while (next != null)
            {
                path.Add(next.Id);
                next = next.PreviousNode;
            }
            path.Reverse();

            return path;
        }
    }
}
