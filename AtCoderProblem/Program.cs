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

			long N = PowerOf10(12);
			long Q = PowerOf10(12);
			long R = PowerOf10(5);
			sb.AppendLine($"{N} {Q}");




			// 問題文出力
			string txt = sb.ToString();
			Console.WriteLine(txt);
			File.AppendAllText(@"..\..\..\AtCoder\Problem.txt", txt, Encoding.ASCII);
		}

		static long PowerOf10(int x)
		{
			return (long)Math.Pow(10, x);
		}

	}
}
