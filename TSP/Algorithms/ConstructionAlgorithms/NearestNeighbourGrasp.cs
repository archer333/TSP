﻿using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class NearestNeighbourGrasp : NearestNeighbour
    {
        public override Node FindNearestNeighbour(Node sourceNode)
        {
            var minimalDistanceList = new SortedDictionary< Node, int>();

            foreach ( var node in InputNodes )
            {
                var distance = CalculateDistance(sourceNode, node);
                minimalDistanceList.Add(node, distance);
            }

            var kvp = minimalDistanceList.OrderBy(i => i.Value);
            var number = RandomObject.Next(0, 2);
            var val = kvp.ElementAt(number);
            var nearestNode = val.Key;
            var minimalDistance = val.Value;

            Distance += minimalDistance;

            return nearestNode;
        }
    }
}
