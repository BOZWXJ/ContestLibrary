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
		static long mod = 1000000007;  // 10^9+7

		static void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			//int n = int.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			string s = Console.ReadLine();
			int[] list = new int[s.Length + 1];
			PriorityQueue<KeyValuePair<int, int>> queue = new PriorityQueue<KeyValuePair<int, int>>((x, y) => y.Key.CompareTo(x.Key));
			for (int i = 0, j = 1; i < s.Length; i++, j++) {
				if (s[i] == '<') {
					list[j] = list[j - 1] + 1;
					if (i == 0 || s[i - 1] == '>') {
						queue.Enqueue(new KeyValuePair<int, int>(list[j - 1], j - 1));
					}
				}
				if (s[i] == '>') {
					list[j] = list[j - 1] - 1;
					if (i == s.Length - 1) {
						queue.Enqueue(new KeyValuePair<int, int>(list[j], j));
					}
				}
			}

			while (queue.Count > 0) {
				var item = queue.Dequeue();
				int index = item.Value;
				int val = -item.Value;
				list[index] = 0;
				// 左
				for (int i = index - 1; i >= 0; i--) {
					list[i] = list[i + 1] + 1;
					if (i > 0 && s[i - 1] == '<') {
						break;
					}
				}
				// 右
				for (int i = index + 1; i < list.Length; i++) {
					list[i] = list[i - 1] + 1;
					if (i < s.Length && s[i] == '>') {
						break;
					}
				}
				//System.Diagnostics.Debug.WriteLine($"index={item.Value}, value={item.Key} ");
				//for (int i = 0; i < s.Length; i++) {
				//	System.Diagnostics.Debug.Write($"{list[i]} {s[i]} ");
				//}
				//System.Diagnostics.Debug.WriteLine(list.Last());
			}

			long ans = 0;
			foreach (var i in list) {
				ans += i;
			}
			Console.WriteLine(ans);


			Console.Out.Flush();
		}

	}

	#region 優先度付きキュー PriorityQueue<T>
	public class PriorityQueue<T> // where T : IComparable
	{
		private readonly List<T> _Heap;
		private readonly Func<T, T, int> _Compare;

		public int Count { get { return _Heap.Count; } }

		public PriorityQueue() : this(Comparer<T>.Default.Compare) { }
		public PriorityQueue(bool reverse) : this((x, y) => Comparer<T>.Default.Compare(reverse ? y : x, reverse ? x : y)) { }
		public PriorityQueue(Comparer<T> comparer) : this(comparer.Compare) { }
		public PriorityQueue(Func<T, T, int> compare)
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
	#endregion

}
