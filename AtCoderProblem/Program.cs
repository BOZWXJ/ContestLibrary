using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AtCoderProblem
{
	class Program
	{
		static Random rand = new Random();

		const int mod = 998244353;
		//const int mod = 1000000007;  // 1,000,000,007  10^9+7
		static int Mod(long x, int m) { return (int)((x % m + m) % m); }
		static long Mod(long x) { return (x % mod + mod) % mod; }
		static long Sq(long x) { return x * x; }

		[STAThread]
		static void Main(string[] args)
		{
			StringBuilder sb = new StringBuilder();

			//int N = 1000000000; //  10^9 1000000000
			//int N = 1000000; //  10^6 1000000
			//int N = 300000; // 3*10^5  300000
			//int N = 200000; // 2*10^5  200000
			//int N = 100000; //   10^5  100000

			int N = 100000;
			Queue<int> queue = new Queue<int>();
			List<string> ans = new List<string>();
			queue.Enqueue(1);

			sb.AppendLine($"{N}");
			for (int i = 0; i < N; i++) {
				int q = rand.Next(10);
				if (q == 0) {
					ans.Add(string.Concat(queue));
					sb.AppendLine($"3");
				} else if (q % 3 == 0 && queue.Count >= 2) {
					queue.Dequeue();
					sb.AppendLine($"2");
				} else {
					int x = rand.Next(1, 10);
					queue.Enqueue(x);
					sb.AppendLine($"1 {x}");
				}
			}
			sb.AppendLine();
			foreach (var s in ans) {
				long x = 0;
				foreach (var c in s) {
					x = Mod(x * 10 + int.Parse($"{c}"), mod);
				}
				sb.AppendLine($"{x}\t{s}");
			}

			//sb.AppendLine(string.Join(" ", s));
			//sb.AppendLine(string.Join(" ", t));

			// 問題文出力
			string txt = sb.ToString();
			//Console.WriteLine(txt);
			File.AppendAllText(@"..\..\..\..\AtCoder\Problem.txt", txt, Encoding.ASCII);
		}

		static long PowerOf10(int x)
		{
			return (long)Math.Pow(10, x);
		}

		static string MakeSequence(int n)
		{
			int[] num = new int[n];
			for (int i = 0; i < n; i++) {
				num[i] = i + 1;
			}
			return string.Join(" ", num.OrderBy(p => Guid.NewGuid()).ToArray());
		}

		static void Shuffle(int[] array)
		{
			for (int i = 0; i < array.Length - 1; i++) {
				int j = rand.Next(i, array.Length);
				(array[i], array[j]) = (array[j], array[i]);
			}
		}

		// n 頂点, n-1 辺の木
		static string MakeTree(int n)
		{
			List<int> n1 = new List<int>();
			List<int> n2 = new List<int>();
			List<string> s = new List<string>();
			for (int i = 1; i <= n; i++) {
				n1.Add(i);
			}
			while (n1.Count > 0) {
				int x, xi;
				if (n2.Count == 0) {
					xi = rand.Next(n1.Count);
					x = n1[xi];
					n1.RemoveAt(xi);
					n2.Add(x);
				} else {
					xi = rand.Next(n2.Count);
					x = n2[xi];
				}

				int yi = rand.Next(n1.Count);
				int y = n1[yi];
				n1.RemoveAt(yi);
				n2.Add(y);

				if (rand.Next(2) == 0) {
					s.Add($"{x} {y}");
				} else {
					s.Add($"{y} {x}");
				}
			}
			return string.Join("\r\n", s.OrderBy(p => Guid.NewGuid()).ToArray());
		}

	}
}
