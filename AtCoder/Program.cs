using System;
using System.Diagnostics;
using System.IO;

namespace AtCoder
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = Console.In;
			var output = Console.Out;
			try {
				Console.SetIn(File.OpenText("Problem.txt"));
				var sw = new StringWriter();
				Console.SetOut(sw);
				var stopwatch = new Stopwatch();
				stopwatch.Start();

				ABC.Solve();

				stopwatch.Stop();
				Debug.WriteLine(stopwatch.Elapsed);
				Debug.Write(sw.ToString());
			} finally {
				Console.SetIn(input);
				Console.SetOut(output);
			}
			//Console.ReadKey();
		}
	}
}
