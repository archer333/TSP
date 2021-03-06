﻿using System;
using TSP.Models;

namespace TSP2
{
    public abstract class Algorithm
    {
        public Random RandomGenerator { get; set; } = new Random();
        public int CalculateDistance(Node node1, Node node2)
        {
            //return (int)Math.Round(Math.Sqrt(Math.Pow(node2.X - node1.X, 2) + Math.Pow(node2.Y - node1.Y, 2)));
            return (int)(Math.Sqrt(Math.Pow(node2.X - node1.X, 2) + Math.Pow(node2.Y - node1.Y, 2)) + 0.5);
        }             
    }
}
