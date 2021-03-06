﻿using System;
using TSP.Algorithms;

namespace TSP.Models
{
    class AlgorithmExecutionSession
    {
        public AlgorithmExecutionSession(ConstructionAlgorithm constructionAlgorithm, OptimalizationAlgorithm optimalizationAlgorithm)
        {
            ConstructionAlgorithm = constructionAlgorithm;
            OptimalizationAlgorithm = optimalizationAlgorithm;
            ConstructionStatisticsData = new ConstructionStatisticsData();
            OptimalizationStatisticsData = new OptimalizationStatisticsData();
        }
        public ConstructionAlgorithm ConstructionAlgorithm { get; set; }
        public OptimalizationAlgorithm OptimalizationAlgorithm { get; set; }
        public ConstructionStatisticsData ConstructionStatisticsData { get; set; }
        public OptimalizationStatisticsData OptimalizationStatisticsData { get; set; }
    }
}
