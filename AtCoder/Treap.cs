using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContestLibrary
{
	// 平衝二分探索木

	#region 平衝二分探索木 Treap<T>
	public class Treap<T> : IEnumerable<T> // where T : IComparable
	{
		public Node Root = null;
		public int Count { get { return Root?.Count ?? 0; } }

		private readonly Func<T, T, int> compare;

		/// <summary>
		/// Treap<T>クラスの新しいインスタンスを初期化します。
		/// </summary>
		public Treap() : this(Comparer<T>.Default.Compare) { }
		/// <summary>
		/// Treap<T>クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="comparer"></param>
		public Treap(Comparer<T> comparer) : this(comparer.Compare) { }
		/// <summary>
		/// Treap<T>クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="comparer"></param>
		public Treap(Func<T, T, int> comparer) { compare = comparer; }

		public T this[int index]
		{
			get
			{
				if (index < 0 || (Root?.Count ?? 0) <= index) throw new IndexOutOfRangeException();
				return GetNode(Root, index).Value;
			}
		}

		/// <summary>
		/// 指定したインデックスにあるノードを返します。
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Node GetNode(int index) { return GetNode(Root, index); }
		public Node GetNode(Node node, int index)
		{
			int cnt = node.Left?.Count ?? 0;
			if (cnt == index) {
				return node;
			} else if (cnt < index) {
				return GetNode(node.Right, index - cnt - 1);
			} else {
				return GetNode(node.Left, index);
			}
		}

		/// <summary>
		/// 指定した値を持つノードを検索し、全体の中で最もインデックス番号の小さいノードを返します。
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public Node Find(T value) { return Find(Root, value); }
		public Node Find(Node node, T value)
		{
			if (node == null) { return null; }
			int r = compare(value, node.Value);
			if (r == 0) {
				return Find(node.Left, value) ?? node;
			} else if (r < 0) {
				return Find(node.Left, value);
			} else {
				return Find(node.Right, value);
			}
		}

		/// <summary>
		/// 指定した値が含まれているかどうかを判定します。
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains(T value) { return Contains(Root, value); }
		public bool Contains(Node node, T value)
		{
			return Find(node, value) != null;
		}

		/// <summary>
		/// 指定した値以上の要素の中で、最もインデックス番号の小さいノードを返します。
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int LowerBouond(T value) { return LowerBouond(Root, value); }
		public int LowerBouond(Node node, T value)
		{
			if (node == null) { return 0; }
			if (compare(value, node.Value) <= 0) {
				return LowerBouond(node.Left, value);
			} else {
				return (node.Left?.Count ?? 0) + 1 + LowerBouond(node.Right, value);
			}
		}

		/// <summary>
		/// 指定した値より大きい要素の中で、最もインデックス番号の小さいノードを返します。
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int UpperBound(T value) { return UpperBound(Root, value); }
		public int UpperBound(Node node, T value)
		{
			if (node == null) { return 0; }
			if (compare(value, node.Value) >= 0) {
				return (node.Left?.Count ?? 0) + 1 + UpperBound(node.Right, value);
			} else {
				return UpperBound(node.Left, value);
			}
		}

		public Node Merge(Node l, Node r)
		{
			if (l == null) { return r; }
			if (r == null) { return l; }
			if (l.Priority > r.Priority) {
				l.Right = Merge(l.Right, r);
				l.Update();
				return l;
			} else {
				r.Left = Merge(l, r.Left);
				r.Update();
				return r;
			}
		}

		/// <summary>
		/// 左にk個、右に残り(n-k)個に分割します。
		/// </summary>
		/// <param name="k"></param>
		/// <returns></returns>
		public (Node l, Node r) Split(int k) { return Split(Root, k); }
		public (Node l, Node r) Split(Node node, int index)
		{
			if (node == null) { return (null, null); }
			if (index <= (node.Left?.Count ?? 0)) {
				(Node l, Node r) = Split(node.Left, index);
				node.Left = r;
				node.Update();
				return (l, node);
			} else {
				(Node l, Node r) = Split(node.Right, index - (node.Left?.Count ?? 0) - 1);
				node.Right = l;
				node.Update();
				return (node, r);
			}
		}

		/// <summary>
		/// 値を追加します。
		/// </summary>
		/// <param name="value"></param>
		public void Add(T value)
		{
			(var l, var r) = Split(Root, LowerBouond(value));
			Node node = new Node(value);
			Root = Merge(l, node);
			Root = Merge(Root, r);
		}

		/// <summary>
		/// 指定した値を持つノードを検索し、全体の中で最もインデックス番号の小さいノードを削除します。
		/// </summary>
		/// <param name="value"></param>
		public void Remove(T value)
		{
			if (Contains(value)) {
				(var l, var r) = Split(Root, LowerBouond(value));
				Root = Merge(l, Split(r, 1).r);
			}
		}

		/// <summary>
		/// 指定したインデックスにあるノードを削除します。
		/// </summary>
		/// <param name="index"></param>
		public void RemoveAt(int index)
		{
			if (index < 0 || Root.Count < index - 1) throw new IndexOutOfRangeException();
			(var l, var r) = Split(Root, index);
			Root = Merge(l, Split(r, 1).r);
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < Count; i++) {
				yield return GetNode(Root, i).Value;
			}
		}
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public class Node
		{
			private static long index = 0;
			public long No { get; private set; }
			private static readonly Random Rand = new Random(0);
			public T Value;
			public double Priority;
			public Node Left;
			public Node Right;
			public int Count { get; private set; }

			public Node(T v)
			{
				No = index; index++;
				Value = v;
				Priority = Rand.NextDouble();
				Count = 1;
			}

			public void Update()
			{
				Count = 1 + (Left?.Count ?? 0) + (Right?.Count ?? 0);
			}

			public override string ToString()
			{
				return $"{Value}:{No},{Priority:f3},{Count}";
			}
		}
	}
	#endregion


	static class TreapExtension
	{

		public static void GraphvizPrint(this Treap<int>.Node root)
		{
			if (root == null) { return; }

			Dictionary<long, string> node = new Dictionary<long, string>();
			List<string> edge = new List<string>();

			var stack = new Stack<Treap<int>.Node>();
			stack.Push(root);
			while (stack.Count > 0) {
				var n = stack.Pop();
				node.Add(n.No, $"  {n.No}[label=\"{n.Value}\\nNo={n.No}\\nPri={n.Priority:f3}\\nCnt={n.Count}\"];");
				if (n.Left != null) {
					stack.Push(n.Left);
					edge.Add($"  \"{n.No}\"->\"{n.Left.No}\" [label=\"L\", tailport = sw, headport = n]");
				}
				if (n.Right != null) {
					stack.Push(n.Right);
					edge.Add($"  \"{n.No}\"->\"{n.Right.No}\" [label=\"R\", tailport = se, headport = n]");
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
