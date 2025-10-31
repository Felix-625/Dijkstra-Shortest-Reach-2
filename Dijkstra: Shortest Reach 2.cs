using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'shortestReach' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. 2D_INTEGER_ARRAY edges
     *  3. INTEGER s
     */

    public static List<int> shortestReach(int n, List<List<int>> edges, int s)
    {
         // Build adjacency list with weights
    Dictionary<int, List<(int node, int weight)>> graph = new Dictionary<int, List<(int, int)>>();
    
    for (int i = 1; i <= n; i++)
    {
        graph[i] = new List<(int, int)>();
    }
    
    foreach (var edge in edges)
    {
        int u = edge[0];
        int v = edge[1];
        int w = edge[2];
        
        graph[u].Add((v, w));
        graph[v].Add((u, w));
    }
    
    // Initialize distances
    int[] distances = new int[n + 1];
    for (int i = 1; i <= n; i++)
    {
        distances[i] = -1; // -1 represents unreachable
    }
    distances[s] = 0;
    
    // Priority queue for Dijkstra (min-heap)
    var pq = new SortedSet<(int distance, int node)>();
    pq.Add((0, s));
    
    while (pq.Count > 0)
    {
        var current = pq.Min;
        pq.Remove(current);
        int currentDist = current.distance;
        int currentNode = current.node;
        
        // If we found a better path already, skip
        if (currentDist > distances[currentNode]) continue;
        
        foreach (var neighbor in graph[currentNode])
        {
            int nextNode = neighbor.node;
            int weight = neighbor.weight;
            int newDist = currentDist + weight;
            
            if (distances[nextNode] == -1 || newDist < distances[nextNode])
            {
                distances[nextNode] = newDist;
                pq.Add((newDist, nextNode));
            }
        }
    }
    
    // Prepare result excluding starting node
    List<int> result = new List<int>();
    for (int i = 1; i <= n; i++)
    {
        if (i != s)
        {
            result.Add(distances[i]);
        }
    }
    
    return result;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            List<List<int>> edges = new List<List<int>>();

            for (int i = 0; i < m; i++)
            {
                edges.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(edgesTemp => Convert.ToInt32(edgesTemp)).ToList());
            }

            int s = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> result = Result.shortestReach(n, edges, s);

            textWriter.WriteLine(String.Join(" ", result));
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
