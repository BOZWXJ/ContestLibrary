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

		[STAThread]
		static void Main(string[] args)
		{
			StringBuilder sb = new StringBuilder();


			int N = 20;
			sb.AppendLine($"{N} 40");
			for (int i = 0; i < N; i++) {
				sb.Append(rand.Next(-100, 101) + " ");
			}
			sb.AppendLine();


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
