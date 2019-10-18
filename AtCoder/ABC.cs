using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AtCoder
{
	public class ABC
	{
		static void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int x = vs[0], y = vs[1], z = vs[2], n = vs[3];

			long[] a = Console.ReadLine().Split().Select(long.Parse).OrderByDescending(p => p).ToArray();
			long[] b = Console.ReadLine().Split().Select(long.Parse).OrderByDescending(p => p).ToArray();
			long[] c = Console.ReadLine().Split().Select(long.Parse).OrderByDescending(p => p).ToArray();

			PriorityQueue<KeyValuePair<long, string>> queue = new PriorityQueue<KeyValuePair<long, string>>((p1, p2) => p1.Key.CompareTo(p2.Key));
			queue.Enqueue(new KeyValuePair<long, string>(a[0] + b[0] + c[0], "0,0,0"));
			for (int i = 0; i < n; i++) {

			}
			while (queue.Count > 0) {
				System.Diagnostics.Debug.WriteLine(queue.Dequeue());
			}


			Console.Out.Flush();
		}
	}


}

#region 優先度付きキュー PriorityQueue<T>

namespace AtCoder
{
	public class PriorityQueue<T> // where T : IComparable
	{
		private readonly List<T> _Heap;
		private readonly Comparison<T> _Compare;

		public int Count { get { return _Heap.Count; } }

		public PriorityQueue() : this(Comparer<T>.Default.Compare) { }
		public PriorityQueue(bool reverse) : this(reverse ? (Comparison<T>)((x, y) => Comparer<T>.Default.Compare(y, x)) : Comparer<T>.Default.Compare) { }
		public PriorityQueue(Comparer<T> comparer) : this(comparer.Compare) { }
		public PriorityQueue(Comparison<T> compare)
		{
			_Heap = new List<T>();
			_Compare = compare;
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
