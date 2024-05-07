using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public class PriorityQueue<T>
    {
        private List<(T item, float priority)> _elements = new List<(T item, float priority)>();

        public int Count => _elements.Count;

        public void Enqueue(T item, float priority)
        {
            _elements.Add((item, priority));
            _elements.Sort((a, b) => a.priority.CompareTo(b.priority));
        }

        public T Dequeue()
        {
            var item = _elements[0].item;
            _elements.RemoveAt(0);
            return item;
        }

        public bool Contains(T item)
        {
            return _elements.Any(el => el.item.Equals(item));
        }
    }
}