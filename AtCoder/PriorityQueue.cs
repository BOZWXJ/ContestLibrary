using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestLibrary
{

	#region 優先度付きキュー PriorityQueue<T>
	public class PriorityQueue<T> // where T : IComparable
	{
		private readonly List<T> heap;
		private readonly Func<T, T, int> compare;

		public int Count { get { return heap.Count; } }

		public PriorityQueue() : this(Comparer<T>.Default.Compare) { }
		public PriorityQueue(bool reverse) : this((x, y) => Comparer<T>.Default.Compare(reverse ? y : x, reverse ? x : y)) { }
		public PriorityQueue(Comparer<T> comparer) : this(comparer.Compare) { }
		public PriorityQueue(Func<T, T, int> compare)
		{
			heap = new List<T>();
			this.compare = compare;
		}

		public void Enqueue(T item)
		{
			heap.Add(item);
			int i = heap.Count - 1;
			while (i > 0) {
				int p = (i - 1) / 2;
				if (compare(heap[p], item) <= 0) {
					break;
				}
				heap[i] = heap[p];
				i = p;
			}
			heap[i] = item;
		}

		public T Dequeue()
		{
			int size = heap.Count - 1;
			T ret = heap[0];
			T x = heap[size];
			int i = 0;
			while (i * 2 + 1 < size) {
				var a = i * 2 + 1;
				var b = i * 2 + 2;
				if (b < size && compare(heap[b], heap[a]) < 0) {
					a = b;
				}
				if (compare(heap[a], x) >= 0) {
					break;
				}
				heap[i] = heap[a];
				i = a;
			}
			heap[i] = x;
			heap.RemoveAt(size);
			return ret;
		}

		public T Peek()
		{
			return heap[0];
		}

		public void Clear()
		{
			heap.Clear();
		}

		public List<T>.Enumerator GetEnumerator()
		{
			return heap.GetEnumerator();
		}

		public override string ToString()
		{
			return string.Join(" ", heap);
		}
	}
	#endregion

}
