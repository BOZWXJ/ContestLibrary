using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContestLibrary
{
	#region 優先度付きキュー PriorityQueue<T>
	/// <summary>
	/// 優先度付きキュー
	/// 優先度が同じ場合、挿入順に取出される。
	/// </summary>
	/// <typeparam name="T">要素の型</typeparam>
	class PriorityQueue<T> // where T : IComparable<T>
	{
		public int Count => buffer.Count;
		public T Top => buffer[0].value;

		private int id = 0;
		private readonly List<(T value, int id)> buffer;
		private readonly Func<(T value, int id), (T value, int id), int> compare;

		public PriorityQueue() : this(Comparer<T>.Default.Compare) { }
		public PriorityQueue(Func<T, T, int> compare)
		{
			buffer = new List<(T value, int id)>();
			this.compare = (x, y) => {
				int result = compare(x.value, y.value);
				if (result == 0) {
					result = -x.id.CompareTo(y.id);
				}
				return result;
			};
		}

		public void Enqueue(T elem)
		{
			int n = Count;
			buffer.Add((elem, id++));
			while (n != 0) {
				int i = (n - 1) / 2;
				if (compare(buffer[n], buffer[i]) > 0) {
					(buffer[i], buffer[n]) = (buffer[n], buffer[i]);
				}
				n = i;
			}
		}

		public T Dequeue()
		{
			T result = buffer[0].value;

			int n = Count - 1;
			buffer[0] = buffer[n];
			buffer.RemoveAt(n);
			for (int i = 0, j; (j = 2 * i + 1) < n;) {
				if ((j != n - 1) && (compare(buffer[j], buffer[j + 1]) < 0)) {
					j++;
				}
				if (compare(buffer[i], buffer[j]) < 0) {
					(buffer[i], buffer[j]) = (buffer[j], buffer[i]);
				}
				i = j;
			}
			return result;
		}

		public T this[int i]
		{
			get { return buffer[i].value; }
		}
	}
	#endregion

	static class PriorityQueue
	{
		public static void GraphvizPrint(this PriorityQueue<int> queue)
		{
			Dictionary<long, string> node = new Dictionary<long, string>();
			List<string> edge = new List<string>();
			for (int i = 0; i < queue.Count; i++) {
				node.Add(i, $"  {i}[label=\"{queue[i]}\\nIndex={i}\"];");
				int left = i * 2 + 1;
				int right = i * 2 + 2;
				if (left < queue.Count) {
					edge.Add($"  \"{i}\"->\"{left}\" [label=\"L\", tailport = sw, headport = n]");
				}
				if (right < queue.Count) {
					edge.Add($"  \"{i}\"->\"{right}\" [label=\"R\", tailport = se, headport = n]");
				}
			}

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("digraph g {");
			foreach (var i in node.Keys.OrderBy(p => p)) {
				sb.AppendLine(node[i]);
			}
			foreach (var s in edge) {
				sb.AppendLine(s);
			}
			sb.AppendLine("}");

			Clipboard.SetText(sb.ToString());
			System.Diagnostics.Debug.WriteLine(sb.ToString());
		}
	}
}
