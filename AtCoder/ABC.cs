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

			int n = int.Parse(Console.ReadLine());
			long sum = 1;
			Queue<long> queue = new Queue<long>();
			queue.Enqueue(1);
			Dictionary<int, long> pairs = new Dictionary<int, long> {
				{ 1, 1 }
			};
			for (int i = 0; i < n; i++) {
				int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
				if (vs[0] == 1) {
					long x = vs[1];
					sum = Mod(sum * 10 + x);
					queue.Enqueue(x);
					pairs.TryAdd(queue.Count, Mod(pairs[queue.Count - 1] * 10));
				} else if (vs[0] == 2) {
					long x = queue.Dequeue();
					sum = Mod(sum - x * pairs[queue.Count + 1]);
				} else if (vs[0] == 3) {
					Console.WriteLine(sum);
				}
			}

		} // Solve()

	} // class ABC
}
