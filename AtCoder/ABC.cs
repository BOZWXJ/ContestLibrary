using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AtCoder
{
	public class ABC
	{
		static public void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			Solve();
			Console.Out.Flush();
		}

		const int mod = 998244353;
		//const int mod = 1000000007;  // 1,000,000,007  10^9+7
		static int Mod(long x, int m) { return (int)((x % m + m) % m); }
		static long Mod(long x) { return (x % mod + mod) % mod; }
		static long Sq(long x) { return x * x; }

		static public void Solve()
		{
			//string s = Console.ReadLine();
			//string[] vs = Console.ReadLine().Split();
			//int n = int.Parse(Console.ReadLine());
			//long x = long.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();

			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int n = vs[0];
			int m = vs[1];
			Tree tree = new Tree(n);
			for (int i = 0; i < m; i++) {
				vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
				int u = vs[0] - 1;
				int v = vs[1] - 1;
				tree.AddEdge(u, v);
			}
			// 最短距離計算
			int[,] distance = new int[n, n];
			for (int i = 0; i < n; i++) {
				foreach (var (p, d) in tree.Bfs(i)) {
					distance[i, p] = d;
				}
			}

			int[] ans = new int[n];
			for (int i = 0; i < n; i++) {
				ans[i] = -1;
			}

			int k = int.Parse(Console.ReadLine());
			List<(int p, int d)> list = new List<(int p, int d)>();
			for (int i = 0; i < k; i++) {
				vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
				int p = vs[0] - 1;
				int d = vs[1];
				list.Add((p, d));
				// 白
				for (int j = 0; j < n; j++) {
					if (distance[p, j] < d) {
						ans[j] = 0;
					}
				}
				//System.Diagnostics.Debug.WriteLine($"{p} {d} : {string.Join(" ", ans)}");
			}
			// 黒
			foreach (var (p, d) in list) {
				bool flg = false;
				for (int j = 0; j < n; j++) {
					if (distance[p, j] == d) {
						if (ans[j] != 0) {
							flg = true;
							ans[j] = 1;
						}
					}
				}
				if (!flg) {
					Console.WriteLine("No");
					return;
				}
				//System.Diagnostics.Debug.WriteLine($"{p} {d} : {string.Join(" ", ans)}");
			}
			// 残
			for (int i = 0; i < n; i++) {
				if (ans[i] < 0) {
					ans[i] = 1;
				}
			}
			Console.WriteLine("Yes");
			Console.WriteLine(string.Concat(ans));

		} // Solve()

	} // class ABC

	class Tree
	{
		public readonly Dictionary<int, HashSet<int>> edge = new Dictionary<int, HashSet<int>>();
		private readonly int nodeMax;

		public Tree(int max)
		{
			nodeMax = max;
		}

		public void AddEdge(int a, int b)
		{
			edge.TryAdd(a, new HashSet<int>());
			edge[a].Add(b);
			edge.TryAdd(b, new HashSet<int>());
			edge[b].Add(a);
		}

		/// <summary>
		/// 幅優先探索
		/// </summary>
		/// <returns></returns>
		public IEnumerable<(int i, int d)> Bfs(int start)
		{
			if (edge.Count == 0) {
				yield break;
			}
			bool[] flg = new bool[nodeMax + 1];
			Queue<(int i, int d)> queue = new Queue<(int i, int d)>();
			queue.Enqueue((start, 0));
			while (queue.Count > 0) {
				(int i, int d) = queue.Dequeue();
				if (flg[i]) {
					continue;
				}
				flg[i] = true;
				foreach (var j in edge[i]) {
					if (!flg[j]) {
						queue.Enqueue((j, d + 1));
					}
				}
				yield return (i, d);
			}
		}
	}

}
