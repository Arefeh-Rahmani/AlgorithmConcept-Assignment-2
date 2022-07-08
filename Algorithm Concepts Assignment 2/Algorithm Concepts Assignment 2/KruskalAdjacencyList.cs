// Include namespace system
using System;
using System.Collections;
using System.Collections.Generic;

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

                IComparer<Edge> comparer =  Comparer<Edge>.Default;
                // comparator.comparingInt(o->o.weight)
                var pq = new PriorityQueue<Edge>(this.allEdges.Count, new MyComparer());//,comparer.Compare(Edge => Edge.weight));
                // add all the edges to priority queue, //sort the edges on weights
                for (int i = 0; i < this.allEdges.Count; i++)
                {
                    pq.push(this.allEdges[i]);
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
                    Edge edge ;
                    if (pq.Count > 0)
                    {
                        edge = pq.pop();
                        // check if adding this edge creates a cycle
                        var x_set = this.find(parent, edge.source);
                        var y_set = this.find(parent, edge.destination);
                        if (x_set == y_set)
                        {
                        }
                        else
                        {
                            // add it to our final result
                            mst.Add(edge);
                            index++;
                            this.union(parent, x_set, y_set);
                        }

                    }
                    else
                        break;
                   
                }
                // print MST
                Console.WriteLine("Minimum Spanning Tree: ");
                this.printGraph(mst);
                Console.ReadLine();

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
                    Console.WriteLine("Edge-" + i.ToString() + " source: " + edge.source.ToString()+ "--->"+ " dest: " + edge.destination.ToString() + " weight: " + edge.weight.ToString());
                }
            }
        }


        class MyComparer : IComparer<Edge>
        {
            public int Compare(Edge x, Edge y)
            {
                // "inverted" comparison
                // direct comparison of integers should return x - y
                return y.weight - x.weight;
            }
        }
        class PriorityQueue<T>
        {
            IComparer<T> comparer;
            T[] heap;
            public int Count { get; private set; }
            public PriorityQueue() : this(null) { }
            public PriorityQueue(int capacity) : this(capacity, null) { }
            public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }
            public PriorityQueue(int capacity, IComparer<T> comparer)
            {
                this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
                this.heap = new T[capacity];
            }
            public void push(T v)
            {
                if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
                heap[Count] = v;
                SiftUp(Count++);
            }
            public T pop()
            {
                var v = top();
                heap[0] = heap[--Count];
                if (Count > 0) SiftDown(0);
                return v;
            }
            public T top()
            {
                if (Count > 0) return heap[0];
                throw new InvalidOperationException("");
            }
            void SiftUp(int n)
            {
                var v = heap[n];
                for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2) heap[n] = heap[n2];
                heap[n] = v;
            }
            void SiftDown(int n)
            {
                var v = heap[n];
                for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
                {
                    if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
                    if (comparer.Compare(v, heap[n2]) >= 0) break;
                    heap[n] = heap[n2];
                }
                heap[n] = v;
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