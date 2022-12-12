using System.Collections.Generic;
using System;
using System.Linq;

namespace _2022_Day_12
{
    public class Traversal<T>
    {
        private Graph<T> _graph;

        public Traversal(Graph<T> graph)
        {
            _graph = graph;
        }

        public Dictionary<T, T> AStar(T start, T goal, Func<T, T, int> weightFunction)
        {
            Dictionary<T, double> dist = new Dictionary<T, double>();
            Dictionary<T, T> prev = new Dictionary<T, T>();

            MinPriorityQueue<T> queue = new MinPriorityQueue<T>();

            queue.Enqueue(start, weightFunction(start, goal));
            dist.Add(start, 0);

            foreach (T node in _graph.GetAllNodes())
            {
                if (!Equals(node, start))
                {
                    dist.Add(node, double.MaxValue);
                    queue.Enqueue(node, double.MaxValue);
                }
            }


            while (queue.Size > 0)
            {
                T current = queue.Dequeue();
                if (Equals(current, goal)) return prev;

                foreach (T neighbor in _graph.GetNode(current))
                {
                    double tentative = dist[current] + 1;
                    if (tentative < dist[neighbor])
                    {
                        dist[neighbor] = tentative;
                        if (prev.ContainsKey(neighbor)) prev[neighbor] = current;
                        else prev.Add(neighbor, current);
                        queue.ChangePriority(neighbor, tentative + weightFunction(neighbor, goal));
                    }
                }
            }


            return new Dictionary<T, T>();
        }

        public Dictionary<T, T> Dijkstra(T start)
        {
            Dictionary<T, double> dist = new Dictionary<T, double>();
            Dictionary<T, T> prev = new Dictionary<T, T>();
            dist.Add(start, 0);

            MinPriorityQueue<T> queue = new MinPriorityQueue<T>();

            T[] nodes = _graph.GetAllNodes();
            foreach (T node in nodes)
            {
                if (_graph.GetNode(node).Count > 0)
                {
                    if (!Equals(start, node)) dist.Add(node, double.MaxValue);
                    queue.Enqueue(node, dist[node]);
                }
            }

            while (queue.Size > 0)
            {
                T minVertex = queue.Dequeue();

                List<T> adjacent = _graph.GetNode(minVertex);

                foreach (var neighbor in adjacent)
                {

                    if (queue.Contains(neighbor))
                    {
                        double alternateWeight = dist[minVertex] + 1;
                        if (alternateWeight < dist[neighbor])
                        {
                            dist[neighbor] = alternateWeight;
                            if (prev.ContainsKey(neighbor)) prev[neighbor] = minVertex;
                            else prev.Add(neighbor, minVertex);
                            queue.ChangePriority(neighbor, alternateWeight);
                        }
                    }
                }
            }

            return prev;
        }
    }

    public class MinPriorityQueue<T>
    {
        private List<double> _priorityQueue = new List<double>();
        private List<T> _queue = new List<T>();

        public int Size => _priorityQueue.Count;
        private int _size => _priorityQueue.Count - 1;

        public MinPriorityQueue() { }

        private int Parent(int index) => (index - 1) / 2;
        private int Left(int index) => (2 * index) + 1;
        private int Right(int index) => (2 * index) + 2;

        public void Enqueue(T value, double priority)
        {
            int oldSize = Size;

            _queue.Add(value);
            _priorityQueue.Add(priority);

            while (oldSize != 0 && _priorityQueue[oldSize] < _priorityQueue[Parent(oldSize)])
            {
                Swap(oldSize, Parent(oldSize));
                oldSize = Parent(oldSize);
            }
        }

        public void ChangePriority(T item, double newPriority)
        {
            int index = _queue.FindIndex(i => Equals(i, item));
            if (index > -1)
            {
                if (_priorityQueue[index] > newPriority)
                {
                    _priorityQueue[index] = newPriority;

                    while (index != 0 && _priorityQueue[index] < _priorityQueue[Parent(index)])
                    {
                        Swap(index, Parent(index));
                        index = Parent(index);
                    }
                }
                else
                {
                    _priorityQueue[index] = newPriority;
                    MinifyHeap(index);
                }
            }

        }

        public T Dequeue()
        {
            if (Size == 1)
            {
                T val = _queue[0];

                _queue.RemoveAt(0);
                _priorityQueue.RemoveAt(0);

                return val;
            }

            T res = _queue[0];

            int oldSize = _size;

            _queue[0] = _queue[oldSize];
            _queue.RemoveAt(oldSize);
            _priorityQueue[0] = _priorityQueue[oldSize];
            _priorityQueue.RemoveAt(oldSize);

            MinifyHeap(0);

            return res;
        }

        private void MinifyHeap(int index)
        {
            int left = Left(index);
            int right = Right(index);

            int smallest = index;

            if (left < Size && _priorityQueue[left] < _priorityQueue[smallest]) smallest = left;
            if (right < Size && _priorityQueue[right] < _priorityQueue[smallest]) smallest = right;
            if (smallest != index)
            {
                Swap(index, smallest);
                MinifyHeap(smallest);
            }
        }

        private void Swap(int indexX, int indexY)
        {
            T tempValue = _queue[indexX];
            _queue[indexX] = _queue[indexY];
            _queue[indexY] = tempValue;

            double tempPriority = _priorityQueue[indexX];
            _priorityQueue[indexX] = _priorityQueue[indexY];
            _priorityQueue[indexY] = tempPriority;
        }

        public bool Contains(T neighbor) => _queue.Contains(neighbor);
    }

    public class Graph<T>
    {
        public Dictionary<T, List<T>> _data = new Dictionary<T, List<T>>();

        public Graph() { }

        public Graph(Dictionary<T, List<T>> graph)
        {
            _data = graph;
        }

        public void AddNode(T key)
        {
            if (_data.ContainsKey(key)) throw new Exception($"Failed to add node {key} to the graph, the node already exists.");
            _data.Add(key, new List<T>());
        }

        public void RemoveNode(T key)
        {
            if (!_data.ContainsKey(key)) throw new Exception($"Failed to remove node {key} from the graph, the node does not exist.");
            _data.Remove(key);
        }

        public void AddConnection(T key, T value)
        {
            if (!_data.ContainsKey(key)) throw new Exception($"Cannot add connection between {value} and {key} the parent node does not exist in the graph.");
            if (_data[key].Contains(value)) throw new Exception($"Cannot add connection between {value} and {key} the connection already exists.");
            _data[key].Add(value);
        }

        public List<T> GetNode(T key)
        {
            if (!_data.ContainsKey(key)) throw new Exception($"Failed to get node {key} form graph because it does not exist.");
            return _data[key];
        }

        public T[] GetAllNodes() => _data.Keys.ToArray();

        public bool ContainsNode(T node) => _data.ContainsKey(node);

        public void Clear() => _data.Clear();
    }
}