using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Concepts_Assignment_2
{
    class GraphMatrix
    {
        public GraphMatrix()
        {

        }
        // A class to represent a graph edge
        class Edge : IComparable<Edge>
        {
            public int src, dest, weight;

            // Comparator function used for sorting edges
            // based on their weight
            public int CompareTo(Edge compareEdge)
            {
                return this.weight
                  - compareEdge.weight;
            }
        }

        // A class to represent
        // a subset for union-find
        public class subset
        {
            public int parent, rank;
        };

        int V, E; // V-> no. of vertices & E->no.of edges
        Edge[] edge; // collection of all edges

        // Creates a graph with V vertices and E edges
        GraphMatrix(int v, int e)
        {
            V = v;
            E = e;
            edge = new Edge[E];
            for (int i = 0; i < e; ++i)
                edge[i] = new Edge();
        }

        // A utility function to find set of an element i
        // (uses path compression technique)
        int find(subset[] subsets, int i)
        {

            // find root and make root as
            // parent of i (path compression)
            if (subsets[i].parent != i)
                subsets[i].parent
                  = find(subsets, subsets[i].parent);

            return subsets[i].parent;
        }

        // A function that does union of
        // two sets of x and y (uses union by rank)
        void Union(subset[] subsets, int x, int y)
        {
            int xroot = find(subsets, x);
            int yroot = find(subsets, y);

            // Attach smaller rank tree under root of
            // high rank tree (Union by Rank)
            if (subsets[xroot].rank < subsets[yroot].rank)
                subsets[xroot].parent = yroot;
            else if (subsets[xroot].rank > subsets[yroot].rank)
                subsets[yroot].parent = xroot;

            // If ranks are same, then make one as root
            // and increment its rank by one
            else
            {
                subsets[yroot].parent = xroot;
                subsets[xroot].rank++;
            }
        }

        // The main function to construct MST
        // using Kruskal's algorithm
        void KruskalMST()
        {
            // This will store the
            // resultant MST
            Edge[] result = new Edge[V];
            int e = 0; // An index variable, used for result[]
            int i
              = 0; // An index variable, used for sorted edges
            for (i = 0; i < V; ++i)
                result[i] = new Edge();

            // Step 1: Sort all the edges in non-decreasing
            // order of their weight. If we are not allowed
            // to change the given graph, we can create
            // a copy of array of edges
            Array.Sort(edge);

            // Allocate memory for creating V subsets
            subset[] subsets = new subset[E];
            for (i = 0; i < E; ++i)
                subsets[i] = new subset();

            // Create E subsets with single elements
            for (int ee = 0; ee < E; ++ee)
            {
                subsets[ee].parent = ee;
                subsets[ee].rank = 0;
            }

            i = 0; // Index used to pick next edge

            // Number of edges to be taken is equal to V-1
            while (e < V - 1)
            {
                // Step 2: Pick the smallest edge. And increment
                // the index for next iteration
                Edge next_edge = new Edge();
                next_edge = edge[i++];

                int x = find(subsets, next_edge.src);
                int y = find(subsets, next_edge.dest);

                // If including this edge doesn't cause cycle,
                // include it in result and increment the index
                // of result for next edge
                if (x != y)
                {
                    result[e++] = next_edge;
                    Union(subsets, x, y);
                }
                // Else discard the next_edge
            }

            // print the contents of result[] to display
            // the built MST
            Console.WriteLine("Following are the edges in "
                    + "the constructed MST");

            int minimumCost = 0;
            for (i = 0; i < e; ++i)
            {
                Console.WriteLine(result[i].src + " -> "
                        + result[i].dest
                        + " == " + result[i].weight);
                minimumCost += result[i].weight;
            }

            Console.WriteLine("Minimum Cost Spanning Tree "
                    + minimumCost);
            Console.ReadLine();
        }

        // Driver Code
        public static void KruksalByMatrix()
        {
            //Graph 1

            //int V = 6; // Number of vertices in graph
            //int E = 9; // Number of edges in graph
            //GraphMatrix graph = new GraphMatrix(V, E);

            //// add edge 1-2
            //graph.edge[0].src = 1;
            //graph.edge[0].dest = 2;
            //graph.edge[0].weight = 2;

            //// add edge 1-4
            //graph.edge[1].src = 1;
            //graph.edge[1].dest = 4;
            //graph.edge[1].weight = 1;

            //// add edge 1-5
            //graph.edge[2].src = 1;
            //graph.edge[2].dest = 5;
            //graph.edge[2].weight = 4;

            //// add edge 2-3
            //graph.edge[3].src = 2;
            //graph.edge[3].dest = 3;
            //graph.edge[3].weight = 3;

            //// add edge 2-4
            //graph.edge[4].src = 2;
            //graph.edge[4].dest = 4;
            //graph.edge[4].weight = 3;

            //// add edge 2-6
            //graph.edge[5].src = 2;
            //graph.edge[5].dest = 6;
            //graph.edge[5].weight = 7;

            //// add edge 3-4
            //graph.edge[6].src = 3;
            //graph.edge[6].dest = 4;
            //graph.edge[6].weight = 5;

            //// add edge 3-6
            //graph.edge[7].src = 3;
            //graph.edge[7].dest = 6;
            //graph.edge[7].weight = 8;

            //// add edge 4-5
            //graph.edge[8].src = 4;
            //graph.edge[8].dest = 5;
            //graph.edge[8].weight = 9;

            //// Function call
            //graph.KruskalMST();


            //Graph 2

            //int V = 9; // Number of vertices in graph
            //int E = 14; // Number of edges in graph
            //GraphMatrix graph = new GraphMatrix(V, E);

            //// add edge 0-1
            //graph.edge[0].src = 0;
            //graph.edge[0].dest = 1;
            //graph.edge[0].weight = 4;

            //// add edge 0-7
            //graph.edge[1].src = 0;
            //graph.edge[1].dest = 7;
            //graph.edge[1].weight = 8;

            //// add edge 1-2
            //graph.edge[2].src = 1;
            //graph.edge[2].dest = 2;
            //graph.edge[2].weight = 8;

            //// add edge 1-7
            //graph.edge[3].src = 1;
            //graph.edge[3].dest = 7;
            //graph.edge[3].weight = 11;

            //// add edge 2-3
            //graph.edge[4].src = 2;
            //graph.edge[4].dest = 3;
            //graph.edge[4].weight = 7;

            //// add edge 2-5
            //graph.edge[5].src = 2;
            //graph.edge[5].dest = 5;
            //graph.edge[5].weight = 4;

            //// add edge 2-8
            //graph.edge[6].src = 2;
            //graph.edge[6].dest = 8;
            //graph.edge[6].weight = 2;

            //// add edge 3-4
            //graph.edge[7].src = 3;
            //graph.edge[7].dest = 4;
            //graph.edge[7].weight = 9;

            //// add edge 3-5
            //graph.edge[8].src = 3;
            //graph.edge[8].dest = 5;
            //graph.edge[8].weight = 14;

            //// add edge 4-5
            //graph.edge[9].src = 4;
            //graph.edge[9].dest = 5;
            //graph.edge[9].weight = 10;

            //// add edge 4-5
            //graph.edge[9].src = 4;
            //graph.edge[9].dest = 5;
            //graph.edge[9].weight = 10;

            //// add edge 4-5
            //graph.edge[9].src = 4;
            //graph.edge[9].dest = 5;
            //graph.edge[9].weight = 10;

            //// add edge 5-6
            //graph.edge[10].src = 5;
            //graph.edge[10].dest = 6;
            //graph.edge[10].weight = 2;

            //// add edge 6-7
            //graph.edge[11].src = 6;
            //graph.edge[11].dest = 7;
            //graph.edge[11].weight = 1;

            //// add edge 6-8
            //graph.edge[12].src = 6;
            //graph.edge[12].dest = 8;
            //graph.edge[12].weight = 6;

            //// add edge 6-8
            //graph.edge[12].src = 6;
            //graph.edge[12].dest = 8;
            //graph.edge[12].weight = 6;

            //// add edge 7-8
            //graph.edge[13].src = 7;
            //graph.edge[13].dest = 8;
            //graph.edge[13].weight = 7;

            //// Function call
            //graph.KruskalMST();

            //Graph 3
            int V = 7; // Number of vertices in graph
            int E = 9; // Number of edges in graph
            GraphMatrix graph = new GraphMatrix(V, E);

            // add edge 1-2
            graph.edge[0].src = 1;
            graph.edge[0].dest = 2;
            graph.edge[0].weight = 28;

            // add edge 1-6
            graph.edge[1].src = 1;
            graph.edge[1].dest = 6;
            graph.edge[1].weight = 10;


            // add edge 2-3
            graph.edge[2].src = 2;
            graph.edge[2].dest = 3;
            graph.edge[2].weight = 16;

            // add edge 2-7
            graph.edge[3].src = 2;
            graph.edge[3].dest = 7;
            graph.edge[3].weight = 14;

            // add edge 3-4
            graph.edge[4].src = 3;
            graph.edge[4].dest = 4;
            graph.edge[4].weight = 12;

            // add edge 4-5
            graph.edge[5].src = 4;
            graph.edge[5].dest = 5;
            graph.edge[5].weight = 22;

            // add edge 4-7
            graph.edge[6].src = 4;
            graph.edge[6].dest = 7;
            graph.edge[6].weight = 18;

            // add edge 5-6
            graph.edge[7].src = 5;
            graph.edge[7].dest = 6;
            graph.edge[7].weight = 25;

            // add edge 5-7
            graph.edge[8].src = 5;
            graph.edge[8].dest = 7;
            graph.edge[8].weight = 24;



            // Function call
            graph.KruskalMST();



        }
    }
}
