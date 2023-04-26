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
			int k = int.Parse(Console.ReadLine());
			List<int[]> list = new List<int[]>();
			for (int i = 0; i < k; i++) {
				vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
				vs[0]--;
				list.Add(vs);
			}
			list.Sort((x, y) => y[1].CompareTo(x[1]));

			int[] ans = new int[n];
			for (int i = 0; i < n; i++) {
				ans[i] = -1;
			}

			Dictionary<int, HashSet<int>> hash = new Dictionary<int, HashSet<int>>();
			for (int i = 0; i < k; i++) {
				hash.Add(i, new HashSet<int>());
				int p = list[i][0];
				int d = list[i][1];
				System.Diagnostics.Debug.WriteLine($"{p + 1} {d}");
				foreach (var item in tree.Bfs(p)) {
					if (item.d < d) {
						System.Diagnostics.Debug.WriteLine($"\t{item.i + 1} {item.d}");
						if (ans[item.i] != 1) {
							ans[item.i] = 0;
						} else {
							Console.WriteLine("No");
							return;
						}
					} else if (item.d == d) {
						System.Diagnostics.Debug.WriteLine($"\t{item.i + 1} {item.d}");
						if (d == 0) {
							if (ans[item.i] != 0) {
								ans[item.i] = 1;
							} else {
								Console.WriteLine("No");
								return;
							}
						} else {
							hash[i].Add(item.i);
						}
					} else {
						break;
					}
				}
				System.Diagnostics.Debug.WriteLine(string.Join(" ", ans));
				System.Diagnostics.Debug.WriteLine(string.Join(" ", hash[i].Select(p => p + 1)));
				System.Diagnostics.Debug.WriteLine("");
			}

			System.Diagnostics.Debug.WriteLine(string.Join(" ", ans));
			foreach (var (key, value) in hash) {
				System.Diagnostics.Debug.WriteLine($"{key}:{string.Join(" ", value.Select(p => p + 1))}");
				if (value.Count > 0) {
					bool flg = true;
					foreach (var item in value) {
						if (ans[item] != 0) {
							ans[item] = 1;
							flg = false;
							break;
						}
					}
					if (flg) {
						Console.WriteLine("No");
						return;
					}
				}
			}
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
		public readonly int[] node;

		public Tree(int max)
		{
			node = new int[max + 1];
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
			bool[] flg = new bool[node.Length];
			Queue<(int i, int d)> queue = new Queue<(int i, int d)>();
			queue.Enqueue((start, 0));
			while (queue.Count > 0) {
				(int i, int d) = queue.Dequeue();
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
