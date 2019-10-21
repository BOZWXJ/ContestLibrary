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


			string[] c = new string[] { "A", "C", "G", "T" };
			foreach (var i in c) {
				foreach (var j in c) {
					foreach (var k in c) {
						foreach (var l in c) {
							sb.AppendLine($"{i}{j}{k}{l}");

						}
					}
				}
			}


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
