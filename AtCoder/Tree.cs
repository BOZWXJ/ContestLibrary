using System;
using System.Collections.Generic;
using System.Text;

namespace ContestLibrary
{

	class Tree
	{
		Dictionary<int, HashSet<int>> edge = new Dictionary<int, HashSet<int>>();
		int[] node;

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
		public IEnumerable<int> Bfs()
		{
			bool[] flg = new bool[node.Length];
			Queue<(int i, int d)> queue = new Queue<(int i, int d)>();
			queue.Enqueue((0, 0));
			while (queue.Count > 0) {
				(int i, int d) = queue.Dequeue();
				flg[i] = true;
				foreach (var j in edge[i]) {
					if (!flg[j]) {
						queue.Enqueue((j, d + 1));
					}
				}
				yield return i;
			}
		}

		// 深さ優先探索
		public IEnumerable<int> Dfs()
		{
			bool[] flg = new bool[node.Length];
			Stack<(int i, int d)> stack = new Stack<(int i, int d)>();
			stack.Push((0, 0));
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
	}

}
