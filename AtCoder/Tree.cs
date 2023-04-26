using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ContestLibrary
{

	class Tree
	{
		public readonly Dictionary<int, HashSet<int>> edge = new Dictionary<int, HashSet<int>>();
		public readonly int nodeMax;

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

		/// <summary>
		/// 深さ優先探索
		/// </summary>
		/// <returns></returns>
		public IEnumerable<int> Dfs(int start)
		{
			if (edge.Count == 0) {
				yield break;
			}
			bool[] flg = new bool[nodeMax + 1];
			Stack<(int i, int d)> stack = new Stack<(int i, int d)>();
			stack.Push((start, 0));
			while (stack.Count > 0) {
				(int i, int d) = stack.Peek();
				if (!flg[i]) {
					// i に入った時

					foreach (var j in edge[i]) {
						if (!flg[j]) {
							stack.Push((j, d + 1));
						}
					}
				} else {
					// i から抜ける時

					stack.Pop();
					continue;
				}
				flg[i] = true;
				yield return i;
			}
		}

		/// <summary>
		/// 再帰 深さ優先探索
		/// </summary>
		/// <param name="i"></param>
		public void Saiki(int i, ref bool[] saikiFlg)
		{
			//System.Diagnostics.Debug.WriteLine($"{i}");
			Console.Write($"{i} ");
			saikiFlg[i] = true;
			foreach (var next in edge[i]) {
				if (!saikiFlg[next]) {
					Saiki(next, ref saikiFlg);
					//System.Diagnostics.Debug.WriteLine($"{i}");
					Console.Write($"{i} ");
				}
			}
		}

	}

}
