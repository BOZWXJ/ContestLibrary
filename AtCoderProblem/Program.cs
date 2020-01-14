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


			int N = 5;
			sb.AppendLine($"{N}");
			for (int i = 0; i < N; i++) {
				for (int j = 0; j < N; j++) {
					if (j > 0) {
						sb.Append(" ");
					}
					sb.Append(rand.Next(1, 4) % 2);
				}
				sb.AppendLine();
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
