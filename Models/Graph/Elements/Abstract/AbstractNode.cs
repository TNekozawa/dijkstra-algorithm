using System;
using System.Collections.Generic;

namespace Models.Graph.Elements.Abstract
{
    public abstract class AbstractNode(int id) : IComparable<AbstractNode>
    {
        public readonly int Id = id;
        public double Cost { get; private set; }
        public bool IsDecided { get; private set; }
        public AbstractNode? PreviousNode { get; private set; } = null;

        public List<AbstractEdge> EdgeList { get; private set; } = [];

        public void SetEdge(AbstractEdge edge)
        {
            EdgeList.Add(edge);
        }

        public void SetCost(double cost)
        {
            Cost = cost;
        }

        public void SetDecision(bool decided)
        {
            IsDecided = decided;
        }

        public void SetPreviousNode(AbstractNode? node)
        {
            PreviousNode = node;
        }

        public int CompareTo(AbstractNode? other)
        {
            if (other == null)
            {
                string? parameterName = "other is null.";
                throw new ArgumentNullException(parameterName);
            }
            else
            {
                return Cost.CompareTo(other.Cost);
            }
        }

        public override string ToString()
        {
            string msg = $"Id: {Id}, {EdgeList.Count} Edges, Decision is {IsDecided}, Cost is {Cost}, Prev is ";
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
