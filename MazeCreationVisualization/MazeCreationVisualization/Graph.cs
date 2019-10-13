using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCreationVisualization
{
    public class Graph<T>
    {

        public List<Vertex<T>> Vertices = new List<Vertex<T>>();
        public int EdgeCount { get; private set; }

        public Graph<Point> this[int index]
        {
            get
            {
                return null;
            }
        }

        public Graph()
        {
        }

        public void AddVertex(Vertex<T> vert)
        {
            Vertices.Add(vert);
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            foreach (var vert in Vertices)
            {
                vert.Edges.Remove(vertex);
            }
            return Vertices.Remove(vertex);
        }

        public void AddEdge(T a, T b, double weight)
        {
            //find the vertex with value a and b;
            var x = GetVertex(a);
            var y = GetVertex(b);

            if (x == null || y == null)
            {
                throw new Exception("verticies not found");
            }

            AddEdge(x, y, weight);
        }

        public void AddEdge(Vertex<T> a, Vertex<T> b, double weight)
        {
            a.Edges.Add(b, weight);
            b.Edges.Add(a, weight);
        }
        public void AddDirectedEdge(Vertex<T> start, Vertex<T> end, double weight)
        {
            start.Edges.Add(end, weight);
        }
        public Vertex<T> GetVertex(T value)
        {
            foreach (var vert in Vertices)
            {
                if (vert.Value.Equals(value))
                {
                    return vert;
                }
            }
            return null;
        }
        public void RemoveEdge(Vertex<T> A, Vertex<T> B)
        {
            A.Edges.Remove(B);
            B.Edges.Remove(A);
        }

        public static (Graph<Point>, List<(Vertex<Point> first, Vertex<Point> second)>) Maze(int height, int width, int seed)
        {
            var graph = new Graph<Point>();

            var maze = new List<HashSet<Vertex<Point>>>(width * height);

            var walls = new List<(Vertex<Point> first, Vertex<Point> second)>(((width - 1) * height) + ((height - 1) * width));

            Random gen = new Random(seed);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    graph.AddVertex(new Vertex<Point>(new Point(x, y)));
                    maze.Add(new HashSet<Vertex<Point>>());
                    //maze[maze.Count - 1].Add(graph[graph.EdgeCount - 1]);
                    maze[maze.Count - 1].Add(graph.Vertices[graph.Vertices.Count - 1]);
                }
            }



            for (int i = 0; i < maze.Count - 1; i++)
            {
                if ((i + 1) % width == 0)
                {
                    i++;
                }

                walls.Add((maze[i].First(), maze[i + 1].First()));
            }

            for (int i = 0; i < maze.Count - width; i++)
            {
                walls.Add((maze[i].First(), maze[i + width].First()));
            }

            while (maze.Count > 1)
            {
                var RandomWall = walls[gen.Next(walls.Count)];

                var First = maze.Where(x => x.Contains(RandomWall.first)).First();

                var Second = maze.Where(x => x.Contains(RandomWall.second)).First();

                if (First == Second)
                {
                    continue;
                }

                graph.AddEdge(RandomWall.first, RandomWall.second, 0);
                graph.EdgeCount++;

                First.UnionWith(Second);

                maze.Remove(Second);
                walls.Remove(RandomWall);
            }

            return (graph, walls);
        }
    }
}
