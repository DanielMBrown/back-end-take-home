using System;
using System.Collections.Generic;

namespace FlightInformationService.Models
{
    public class Graph<T>
    {
        public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

        public Graph(List<T> vertices, List<Tuple<T,T>> edges)
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }

            foreach(var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddVertex(T vertext)
        {
            AdjacencyList[vertext] = new HashSet<T>();
        }

        public void AddEdge(Tuple<T,T> edge)
        {
            // Pointing vertices at each other, assuming my usage of this is undirected/bi-directional
            if(AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }
}
