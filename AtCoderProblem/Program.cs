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
		[STAThread]
		static void Main(string[] args)
		{
			StringBuilder sb = new StringBuilder();
			Random rand = new Random();


			int N = 100000;
			int M = 100000;
			sb.AppendLine($"{N} {M}");
			for (int i = 0; i < N; i++) {
				int a = rand.Next(1, 100);
				int b = rand.Next(1, 100);
				sb.AppendLine($"{a} {b}");
			}
			sb.AppendLine();


			// 問題文出力
			string txt = sb.ToString();
			//Console.WriteLine(txt);
			File.AppendAllText(@"..\..\..\AtCoder\Problem.txt", txt, Encoding.ASCII);
		}

		static long PowerOf10(int x)
		{
			return (long)Math.Pow(10, x);
		}

	}
}
