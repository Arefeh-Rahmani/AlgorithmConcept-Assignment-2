using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Concepts_Assignment_2
{
    public class KrushkalMST
    {
        class Edge
        {
            public int source;
            public int destination;
            public int weight;
            public Edge(int source, int destination, int weight)
            {
                this.source = source;
                this.destination = destination;
                this.weight = weight;
            }
        }
        class Graph
        {
            public int vertices;
            public List<Edge> allEdges = new List<Edge>();
            public Graph(int vertices)
            {
                this.vertices = vertices;
            }
            public void addEgde(int source, int destination, int weight)
            {
                var edge = new Edge(source, destination, weight);
                this.allEdges.Add(edge);
            }
            public void kruskalMST()
            {
                var pq = new PriorityQueue<Edge,int>(new (Edge Element, int Priority)[allEdges.Count]);
                // add all the edges to priority queue, //sort the edges on weights
                for (int i = 0; i < this.allEdges.Count; i++)
                {
                    Edge edge = this.allEdges[i];
                    pq.Enqueue(edge,i);
                }
                // create a parent []
                int[] parent = new int[this.vertices];
                // makeset
                this.makeSet(parent);
                var mst = new List<Edge>();
                // process vertices - 1 edges
                var index = 0;
                while (index < this.vertices - 1)
                {
                    Edge medge = this.allEdges[index];
                    var edge = pq.Dequeue();

                    // check if adding this edge creates a cycle
                    var x_set = this.find(parent, medge.source);
                    var y_set = this.find(parent, medge.destination);
                    if (x_set == y_set)
                    {
                    }
                    else
                    {
                        // add it to our final result
                        mst.Add(medge);
                        index++;
                        this.union(parent, x_set, y_set);
                    }
                }
                // print MST
                Console.WriteLine("Minimum Spanning Tree: ");
                this.printGraph(mst);
            }
            public void makeSet(int[] parent)
            {
                // Make set- creating a new element with a parent pointer to itself.
                for (int i = 0; i < this.vertices; i++)
                {
                    parent[i] = i;
                }
            }
            public int find(int[] parent, int vertex)
            {
                // chain of parent pointers from x upwards through the tree
                // until an element is reached whose parent is itself
                if (parent[vertex] != vertex)
                {
                    return this.find(parent, parent[vertex]);
                }
                ;
                return vertex;
            }
            public void union(int[] parent, int x, int y)
            {
                var x_set_parent = this.find(parent, x);
                var y_set_parent = this.find(parent, y);
                // make x as parent of y
                parent[y_set_parent] = x_set_parent;
            }
            public void printGraph(List<Edge> edgeList)
            {
                for (int i = 0; i < edgeList.Count; i++)
                {
                    var edge = edgeList[i];
                    Console.WriteLine("Edge-" + i.ToString() + " source: " + edge.source.ToString() + " destination: " + edge.destination.ToString() + " weight: " + edge.weight.ToString());
                }
            }
        }
        public static void KeuskalAdjacencyList()
        {
            var vertices = 9;
            var graph = new Graph(vertices);
            graph.addEgde(1, 2, 2);
            graph.addEgde(1, 4, 1);
            graph.addEgde(1, 5, 4);
            graph.addEgde(2, 3, 3);
            graph.addEgde(2, 4, 3);
            graph.addEgde(2, 6, 7);
            graph.addEgde(3, 4, 5);
            graph.addEgde(3, 6, 8);
            graph.addEgde(4, 5, 9);
            graph.kruskalMST();
        }
    }
}
