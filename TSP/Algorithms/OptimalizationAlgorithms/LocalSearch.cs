﻿using System;
using System.Collections.Generic;
using TSP.Models;

namespace TSP.Algorithms.OptimalizationAlgorithms
{
    class LocalSearch : OptimalizationAlgorithm
    {
        public int SwapPathsFirstIndex { get; set; }
        public int SwapPathsSecondIndex { get; set; }
        public int SwapVerticesPathNodeIndex { get; set; }
        public int SwapVerticesUnusedNodeIndex { get; set; }
        public int BestSwapPathsDistance { get; set; } = int.MaxValue;
        public int BestSwapVerticesDistance { get; set; } = int.MaxValue;
        public bool PathsChangeMade { get; set; } = true;
        public bool VerticesChangeMade { get; set; } = true;

        protected void CheckSwapPaths( int firstIndex, int secondIndex )
        {
            var totalDistance = OperatingData.Distance;

            var oldDistance1 = CalculateDistance(OperatingData.PathNodes[firstIndex], OperatingData.PathNodes[firstIndex + 1]);
            var oldDistance2 = CalculateDistance(OperatingData.PathNodes[secondIndex],
                OperatingData.PathNodes[secondIndex >= OperatingData.PathNodes.Count - 1 ? 0 : secondIndex + 1]);
            totalDistance -= ( oldDistance1 + oldDistance2 );

            var newDistance1 = CalculateDistance(OperatingData.PathNodes[firstIndex], OperatingData.PathNodes[secondIndex]);
            var newDistance2 = CalculateDistance(OperatingData.PathNodes[firstIndex + 1],
                OperatingData.PathNodes[secondIndex >= OperatingData.PathNodes.Count - 1 ? 0 : secondIndex + 1]);
            totalDistance += ( newDistance1 + newDistance2 );

            if ( totalDistance >= OperatingData.Distance || totalDistance >= BestSwapPathsDistance ) return;
            BestSwapPathsDistance = totalDistance;
            SwapPathsFirstIndex = firstIndex;
            SwapPathsSecondIndex = secondIndex;
            PathsChangeMade = true;
        }

        protected void FindBestSwapPaths()
        {
            for ( var i = 0; i < OperatingData.PathNodes.Count - 2; i++ )
            {
                for ( var j = i + 2; j < OperatingData.PathNodes.Count; j++ )
                {
                    if ( i == 0 && j == OperatingData.PathNodes.Count - 1 ) continue;
                    CheckSwapPaths(i, j);
                }
            }
        }

        protected void CheckSwapVertices( int pathNodeIndex, int unusedNodeIndex )
        {
            var totalDistance = OperatingData.Distance;
            var previousPathNodeIndex = pathNodeIndex - 1 < 0 ? OperatingData.PathNodes.Count - 1 : pathNodeIndex - 1;
            var nextPathNodeIndex = pathNodeIndex + 1 > OperatingData.PathNodes.Count - 1 ? 0 : pathNodeIndex + 1;

            totalDistance -=
                CalculateDistance(OperatingData.PathNodes[previousPathNodeIndex], OperatingData.PathNodes[pathNodeIndex]) +
                CalculateDistance(OperatingData.PathNodes[pathNodeIndex], OperatingData.PathNodes[nextPathNodeIndex]);

            totalDistance +=
                CalculateDistance(OperatingData.PathNodes[previousPathNodeIndex], OperatingData.UnusedNodes[unusedNodeIndex]) +
                CalculateDistance(OperatingData.UnusedNodes[unusedNodeIndex], OperatingData.PathNodes[nextPathNodeIndex]);

            if ( totalDistance >= OperatingData.Distance || totalDistance >= BestSwapVerticesDistance ) return;
            BestSwapVerticesDistance = totalDistance;
            SwapVerticesPathNodeIndex = pathNodeIndex;
            SwapVerticesUnusedNodeIndex = unusedNodeIndex;
            VerticesChangeMade = true;
        }

        protected void FindBestSwapVertices()
        {
            for ( var i = 0; i < OperatingData.PathNodes.Count; i++ )
            {
                for ( var j = 0; j < OperatingData.UnusedNodes.Count; j++ )
                {
                    CheckSwapVertices(i, j);
                }
            }
        }

        protected void SwapPaths()
        {
            var newPath = (List<Node>)OperatingData.PathNodes;
            newPath.Reverse(SwapPathsFirstIndex + 1, Math.Abs(SwapPathsSecondIndex - SwapPathsFirstIndex));
            OperatingData.Distance = BestSwapPathsDistance;
        }

        protected void SwapVertices()
        {
            var newNode = OperatingData.UnusedNodes[SwapVerticesUnusedNodeIndex];
            var oldNode = OperatingData.PathNodes[SwapVerticesPathNodeIndex];
            OperatingData.PathNodes[SwapVerticesPathNodeIndex] = newNode;
            OperatingData.UnusedNodes[SwapVerticesUnusedNodeIndex] = oldNode;
            OperatingData.Distance = BestSwapVerticesDistance;
        }

        public override void ResetAlgorithm()
        {
            VerticesChangeMade = true;
            PathsChangeMade = true;
            BestSwapPathsDistance = int.MaxValue;
            BestSwapVerticesDistance = int.MaxValue;
        }

        public override void Optimize()
        {
            while ( VerticesChangeMade || PathsChangeMade )
            {
                if ( PathsChangeMade )
                {
                    PathsChangeMade = false;
                    FindBestSwapPaths();
                }
                if ( VerticesChangeMade )
                {
                    VerticesChangeMade = false;
                    FindBestSwapVertices();
                }

                if ( !VerticesChangeMade && !PathsChangeMade ) break;
                if ( BestSwapPathsDistance < BestSwapVerticesDistance )
                    SwapPaths();
                else
                    SwapVertices();
            }

            //ResetAlgorithm();
            OperatingData.Distance = BestSwapPathsDistance < BestSwapVerticesDistance
                ? BestSwapPathsDistance
                : BestSwapVerticesDistance;
        }
    }
}
