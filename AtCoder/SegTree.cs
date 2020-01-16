using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestLibrary
{

	#region セグメント木
	public class SegTree<T> : IEnumerable<T>
	{
		private readonly T INF;
		private readonly T[] nodes;
		private readonly int length;
		private readonly int size;
		private readonly Func<T, T, T> update;
		private readonly Func<T, T, T> operation;

		public SegTree(int capacity, T inf, Func<T, T, T> query) : this(capacity, inf, query, (x, y) => y) { }
		public SegTree(int capacity, T inf, Func<T, T, T> query, Func<T, T, T> update)
		{
			long n = 1;
			while (n < capacity) {
				n *= 2;
			}
			INF = inf;
			nodes = new T[2 * n - 1];
			length = capacity;
			size = (int)n;
			this.update = update;
			operation = query;
			if (!INF.Equals(nodes[0])) {
				for (int i = 0; i < nodes.Length; i++) {
					nodes[i] = INF;
				}
			}
		}

		public T this[int i]
		{
			set { Update(i, value); }
			get { return nodes[size - 1 + i]; }
		}
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < length; i++) {
				yield return nodes[size - 1 + i];
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Update(int index, T value)
		{
			int i = size - 1 + index;
			nodes[i] = update(nodes[i], value);
			while (i > 0) {
				i = (i - 1) / 2;
				nodes[i] = operation(nodes[i * 2 + 1], nodes[i * 2 + 2]);
			}
		}

		public T Query()
		{
			return Query(0, length, 0, 0, size);
		}
		public T Query(int a, int b)
		{
			return Query(a, b, 0, 0, size);
		}
		public T Query(int a, int b, int k, int l, int r)
		{
			if (r <= a || b <= l) { return INF; }
			if (a <= l && r <= b) { return nodes[k]; }
			var c1 = Query(a, b, k * 2 + 1, l, (r + l) / 2);
			var c2 = Query(a, b, k * 2 + 2, (r + l) / 2, r);
			return operation(c1, c2);
		}
	}
	#endregion

}
