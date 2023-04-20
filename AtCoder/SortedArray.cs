using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContestLibrary
{

	#region ソート済み配列（要素重複可）
	public class SortedArray<T> : IEnumerable<T>
	{
		public int Count => buffer.Count;

		private readonly List<T> buffer;
		private readonly Comparison<T> comparison;

		public SortedArray() : this(0, Comparer<T>.Default.Compare) { }
		public SortedArray(IComparer<T> comparer) : this(0, comparer.Compare) { }
		public SortedArray(Comparison<T> compare) : this(0, compare) { }
		public SortedArray(int capacity) : this(capacity, Comparer<T>.Default.Compare) { }
		public SortedArray(int capacity, IComparer<T> comparer) : this(capacity, comparer.Compare) { }
		public SortedArray(int capacity, Comparison<T> compare)
		{
			buffer = new List<T>(capacity);
			comparison = compare;
		}
		public SortedArray(IEnumerable<T> collection) : this(collection, Comparer<T>.Default.Compare) { }
		public SortedArray(IEnumerable<T> collection, IComparer<T> comparer) : this(collection, comparer.Compare) { }
		public SortedArray(IEnumerable<T> collection, Comparison<T> compare)
		{
			buffer = new List<T>(collection);
			comparison = compare;
			buffer.Sort(comparison);
		}

		public void Add(T item) => Add(item, (Comparison<T>)null);
		public void Add(T item, IComparer<T> comparer) => Add(item, comparer.Compare);
		public void Add(T item, Comparison<T> compare)
		{
			compare ??= comparison;

			int i = BinarySearchLast(item, compare);
			if (i < 0) {
				i = ~i;
			}
			buffer.Insert(i, item);
		}

		#region BinarySearchFirst 二分探索（同じ要素は先頭）
		public int BinarySearchFirst(T item) { return BinarySearchFirst(0, buffer.Count, item, null); }
		public int BinarySearchFirst(T item, IComparer<T> comparer) { return BinarySearchFirst(0, buffer.Count, item, comparer.Compare); }
		public int BinarySearchFirst(T item, Comparison<T> compare) { return BinarySearchFirst(0, buffer.Count, item, compare); }
		public int BinarySearchFirst(int index, int count, T item, Comparison<T> compare)
		{
			compare ??= comparison;

			int l = index;
			int r = index + count;

			if (l > r) {
				return ~(r + 1);
			}
			int mid;
			while (l < r) {
				mid = (l + r) / 2;
				if (compare(buffer[mid], item) < 0) {
					l = mid + 1;
				} else {
					r = mid;
				}
			}
			if (l == index + count || compare(buffer[l], item) != 0) {
				l = ~l;
			}
			return l;
		}
		#endregion

		#region BinarySearchLast 二分探索（同じ要素は末尾）
		public int BinarySearchLast(T item) { return BinarySearchLast(0, buffer.Count, item, null); }
		public int BinarySearchLast(T item, IComparer<T> comparer) { return BinarySearchLast(0, buffer.Count, item, comparer.Compare); }
		public int BinarySearchLast(T item, Comparison<T> compare) { return BinarySearchLast(0, buffer.Count, item, compare); }
		public int BinarySearchLast(int index, int count, T item, Comparison<T> compare)
		{
			compare ??= comparison;

			int l = index;
			int r = index + count;

			if (l > r) {
				return ~(r + 1);
			}
			int mid;
			while (l < r) {
				mid = (l + r) / 2;
				if (compare(buffer[mid], item) <= 0) {
					l = mid + 1;
				} else {
					r = mid;
				}
			}
			if (l == 0 || compare(buffer[l - 1], item) != 0) {
				l = ~l;
			} else {
				l -= 1;
			}
			return l;
		}
		#endregion

		public T this[int i]
		{
			get { return buffer[i]; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return buffer.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	#endregion

}
