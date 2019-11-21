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
		static int mod = 1000000007;  // 10^9+7

		static void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			// int n = int.Parse(Console.ReadLine());
			// string s = Console.ReadLine();
			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int n = vs[0];
			int m = vs[1];
			int[][] bridges = new int[m][];
			for (int i = 0; i < m; i++) {
				bridges[i] = Console.ReadLine().Split().Select(p => int.Parse(p) - 1).ToArray();
			}
			Array.Reverse(bridges);
			UnionFind union = new UnionFind(n);
			long[] ans = new long[m];
			ans[0] = n * ((long)n - 1) / 2;
			for (int i = 0; i < m - 1; i++) {
				int a = bridges[i][0];
				int b = bridges[i][1];
				if (!union.Same(a, b)) {
					ans[i + 1] = ans[i] - union.Size(a) * union.Size(b);
				} else {
					ans[i + 1] = ans[i];
				}
				union.Unite(a, b);
			}
			Array.Reverse(ans);
			foreach (var item in ans) {
				Console.WriteLine(item);
			}


			Console.Out.Flush();
		}
	}


	#region
	public class UnionFind
	{
		private readonly UnionFindNode[] _Parent;

		public UnionFind(int n)
		{
			_Parent = new UnionFindNode[n];
			for (int i = 0; i < n; i++) {
				_Parent[i] = new UnionFindNode(i, 1);
			}
		}

		public int Find(int x)
		{
			if (_Parent[x].Parent == x) {
				return _Parent[x].Parent;
			}
			return _Parent[x].Parent = Find(_Parent[x].Parent);
		}

		public int Size(int x)
		{
			return _Parent[Find(x)].Size;
		}

		public void Unite(int x, int y)
		{
			int rx = Find(x);
			int ry = Find(y);
			if (rx != ry) {
				_Parent[ry].Parent = rx;
				_Parent[rx].Size += _Parent[ry].Size;
			}
		}

		public bool Same(int x, int y)
		{
			int rx = Find(x);
			int ry = Find(y);
			return rx == ry;
		}

		public override string ToString()
		{
			return string.Join(" ", _Parent.Select(p => $"({p.Parent},{p.Size})"));
		}
	}
	public class UnionFindNode
	{
		public int Parent;
		public int Size;
		public UnionFindNode(int parent, int size)
		{
			Parent = parent;
			Size = size;
		}
	}
	#endregion


}
