﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TSP
{
    internal class Algorithm
    {
        public Algorithm(IList<Node> nodes)
        {
            ClonedNodes = nodes.CloneList();
            InputNodes = nodes.CloneList();
            OutputNodes = new List<Node>();
            Distance = 0;
        }

        public virtual void FindRoute(Node node) {}

        public int CalculateDistance(Node node1, Node node2)
        {
            return (int)Math.Round(Math.Sqrt(Math.Pow(node2.X - node1.X, 2) + Math.Pow(node2.Y - node1.Y, 2)));
        }

        public int CalculatePath(int distance, Node node1, Node node2, Node startNode)
        {
            return distance + CalculateDistance(node1, node2) + CalculateDistance(node2, startNode);
        }

        public void ResetAlgorithm()
        {
            InputNodes = ClonedNodes.CloneList();
            OutputNodes.Clear();
            Distance = 0;
        }

        public IList<Node> ClonedNodes { get; set; }
        public IList<Node> InputNodes { get; set; }
        public IList<Node> OutputNodes { get; set; }
        public int Distance { get; set; }
        public const int OutputNodesLimit = 50;
        public Random RandomObject { get; set; } = new Random();
    }
}
