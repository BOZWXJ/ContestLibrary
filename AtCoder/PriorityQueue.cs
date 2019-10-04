using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region 優先度付きキュー PriorityQueue<T>

namespace AtCoder
{
	public class PriorityQueue<T> where T : IComparable
	{
		private readonly List<T> _Heap;
		private readonly Comparison<T> _Compare;

		public int Count { get { return _Heap.Count; } }

		public PriorityQueue() : this(false) { }
		public PriorityQueue(bool reverse) : this(reverse, Comparer<T>.Default) { }
		public PriorityQueue(Comparer<T> Compare) : this(false, Compare) { }
		public PriorityQueue(bool reverse, Comparer<T> Compare)
		{
			_Heap = new List<T>();
			if (!reverse) {
				_Compare = Compare.Compare;
			} else {
				_Compare = (T x, T y) => Compare.Compare(y, x);
			}
		}

		public void Enqueue(T item)
		{
			_Heap.Add(item);
			int i = _Heap.Count - 1;
			while (i > 0) {
				int p = (i - 1) / 2;
				if (_Compare(_Heap[p], item) <= 0) {
					break;
				}
				_Heap[i] = _Heap[p];
				i = p;
			}
			_Heap[i] = item;
		}

		public T Dequeue()
		{
			int size = _Heap.Count - 1;
			T ret = _Heap[0];
			T x = _Heap[size];
			int i = 0;
			while (i * 2 + 1 < size) {
				var a = i * 2 + 1;
				var b = i * 2 + 2;
				if (b < size && _Compare(_Heap[b], _Heap[a]) < 0) {
					a = b;
				}
				if (_Compare(_Heap[a], x) >= 0) {
					break;
				}
				_Heap[i] = _Heap[a];
				i = a;
			}
			_Heap[i] = x;
			_Heap.RemoveAt(size);
			return ret;
		}

		public T Peek()
		{
			return _Heap[0];
		}

		public void Clear()
		{
			_Heap.Clear();
		}

		public List<T>.Enumerator GetEnumerator()
		{
			return _Heap.GetEnumerator();
		}

		public override string ToString()
		{
			return string.Join(" ", _Heap);
		}
	}
}

#endregion
