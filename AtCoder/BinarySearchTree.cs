using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ContestLibrary
{
	// 二分探索木 BinarySearchTree
	// 

	#region 二分探索木 BinarySearchTree
	public class BinarySearchTree<T> : IEnumerable<T> // where T : IComparable
	{
		public Node Root = null;
		public int Count { get; private set; } = 0;

		private readonly Func<T, T, int> compare;

		public BinarySearchTree() : this(Comparer<T>.Default.Compare) { }
		public BinarySearchTree(bool reverse) : this((x, y) => reverse ? Comparer<T>.Default.Compare(y, x) : Comparer<T>.Default.Compare(x, y)) { }
		public BinarySearchTree(Comparer<T> comparer) : this(comparer.Compare) { }
		public BinarySearchTree(Func<T, T, int> comparer)
		{
			compare = comparer;
		}

		public bool Contains(T value)
		{
			return Find(value) != null;
		}

		public void Clear()
		{
			Root = null;	// ?
			Count = 0;
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="value"></param>
		/// <returns>存在しない場合 null</returns>
		public Node Find(T value)
		{
			Node n = Root;
			while (n != null) {
				int i = compare(n.Value, value);
				if (i > 0) {
					n = n.Left;
				} else if (i < 0) {
					n = n.Right;
				} else {
					break;
				}
			}
			return n;
		}

		/// <summary>
		/// 追加
		/// </summary>
		/// <param name="value"></param>
		public void Add(T value)
		{
			if (Root == null) {
				Root = new Node(value);
				Count++;
			} else {
				Node n = Root;
				Node p = null;
				while (n != null) {
					p = n;
					if (compare(n.Value, value) > 0) {
						n = n.Left;
					} else {
						n = n.Right;
					}
				}
				n = new Node(value, p);
				Count++;
				if (compare(p.Value, value) > 0) {
					p.Left = n;
				} else {
					p.Right = n;
				}
			}
		}

		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="value"></param>
		public void Remove(T value)
		{
			Remove(Find(value));
		}

		// n を削除する
		public void Remove(Node n)
		{
			if (n != null) {
				if (n.Right == null) {
					// 両方 null または 左の子のみ → 左の子を親に繋ぐ
					ConnectParent(n, n.Left);
				} else if (n.Left == null) {
					// 右の子のみ → 右の子を親に繋ぐ
					ConnectParent(n, n.Right);
				} else {
					// 両方の子が有り → 右の部分木の最小値（左端）を n の位置に入れる。最小値の右の子は最小値の親に繋ぐ
					Node min = n.Right.Min;
					ConnectParent(min, min.Right);
					ConnectParent(n, min);
					min.Left = n.Left;
					min.Right = n.Right;
				}
				n.Parent = null;
				n.Left = null;
				n.Right = null;
				Count--;
			}
		}

		// n の親に m を繋ぐ
		void ConnectParent(Node n, Node m)
		{
			if (Root == n) {
				Root = m;
				m.Parent = null;
			} else {
				Node p = n.Parent;
				if (p.Left == n) {
					p.Left = m;
				} else {
					p.Right = m;
				}
				if (m != null) {
					m.Parent = p;
				}
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (var n = Root?.Min; n != null; n = n.Next) {
				yield return n.Value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Node
		{
			private static long index = 0;
			public long No { get; private set; }
			public T Value;
			public Node Parent;
			public Node Left;
			public Node Right;
			public Node(T value) : this(value, null) { }
			public Node(T value, Node parent)
			{
				No = index; index++;
				Value = value;
				Parent = parent;
				Left = null;
				Right = null;
			}
			/// <summary>
			/// 最小値（左端のノード）
			/// </summary>
			public Node Min
			{
				get
				{
					Node n = this;
					while (n.Left != null) {
						n = n.Left;
					}
					return n;
				}
			}
			/// <summary>
			/// 最大値（右端のノード）
			/// </summary>
			public Node Max
			{
				get
				{
					Node n = this;
					while (n.Right != null) {
						n = n.Right;
					}
					return n;
				}
			}
			/// <summary>
			/// 次（次に大きなノード）
			/// </summary>
			public Node Next
			{
				get
				{
					Node n = this;
					if (n.Right != null) {
						n = n.Right.Min;
					} else {
						while (n.Parent != null && n.Parent.Left != n) {
							n = n.Parent;
						}
						n = n.Parent;
					}
					return n;
				}
			}
			/// <summary>
			/// 前（次に小さなノード）
			/// </summary>
			public Node Previous
			{
				get
				{
					Node n = this;
					if (n.Left != null) {
						n = n.Left.Max;
					} else {
						while (n.Parent != null && n.Parent.Right != n) {
							n = n.Parent;
						}
						n = n.Parent;
					}
					return n;
				}
			}
		}
	}
	#endregion

	#region 二分探索木 BinarySearchTree デバッグ用
	static class BinarySearchTreeExtension
	{
		public static void CheckNode(this BinarySearchTree<int> tree)
		{
			if (tree.Root == null) {
				return;
			}
			bool f = true;
			var stack = new Stack<BinarySearchTree<int>.Node>();
			stack.Push(tree.Root);
			while (stack.Count > 0) {
				var n = stack.Pop();
				if (!((n.Left != null ? n.Left.Value < n.Value : true) && (n.Right != null ? n.Value <= n.Right?.Value : true))) {
					f = false;
					System.Diagnostics.Debug.WriteLine($"error {n.No}:{n.Left.Value} < {n.Value} <= {n.Right.Value} ");
				}
				if (n.Left != null) {
					stack.Push(n.Left);
				}
				if (n.Right != null) {
					stack.Push(n.Right);
				}
			}
			System.Diagnostics.Debug.WriteLine($"Check {(f ? "OK" : "NG")}");
		}

		public static void DebugPrint(this BinarySearchTree<int> tree)
		{
			if (tree.Root == null) {
				System.Diagnostics.Debug.WriteLine("null");
				return;
			}
			var stack = new Stack<(int, BinarySearchTree<int>.Node)>();
			stack.Push((0, tree.Root));
			while (stack.Count > 0) {
				(int l, var n) = stack.Pop();
				string s = $"({n.Value}) ";
				//string s = $"({n.Value} {(n.Left != null ? n.Left.Value : default)} {(n.Right != null ? n.Right.Value : default)}) ";
				if (n.Right != null) {
					stack.Push((l + s.Length, n.Right));
				}
				if (n.Left != null) {
					stack.Push((l + s.Length, n.Left));
				}
				System.Diagnostics.Debug.Write(s);
				if (n.Left == null) {
					System.Diagnostics.Debug.WriteLine("");
					if (stack.Count > 0) {
						(int nl, var nn) = stack.Peek();
						System.Diagnostics.Debug.Write(new string(' ', nl));
					}
				}
			}
			System.Diagnostics.Debug.WriteLine("");
		}

		public static void GraphvizPrint(this BinarySearchTree<int> tree)
		{
			if (tree.Root == null) {
				return;
			}
			System.Diagnostics.Debug.WriteLine("digraph g {");
			var stack = new Stack<BinarySearchTree<int>.Node>();
			stack.Push(tree.Root);
			while (stack.Count > 0) {
				var n = stack.Pop();
				if (n.Left != null) {
					stack.Push(n.Left);
					System.Diagnostics.Debug.WriteLine($"  \"{n.No}:{n.Value}\"->\"{n.Left.No}:{n.Left.Value}\" [label=\"L\", tailport = sw, headport = n]");
				}
				if (n.Right != null) {
					stack.Push(n.Right);
					System.Diagnostics.Debug.WriteLine($"  \"{n.No}:{n.Value}\"->\"{n.Right.No}:{n.Right.Value}\" [label=\"R\", tailport = se, headport = n]");
				}
			}
			System.Diagnostics.Debug.WriteLine("}");
		}
	}
	#endregion

}
