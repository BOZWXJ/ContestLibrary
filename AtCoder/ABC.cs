using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

		//const int mod = 998244353;
		const int mod = 1000000007;  // 10^9+7
		static int Mod(long x, int m) { return (int)((x % m + m) % m); }
		static long Mod(long x, long m) { return (x % m + m) % m; }

		static public void Solve()
		{
			//string s = Console.ReadLine();
			//int n = int.Parse(Console.ReadLine());
			//long x = long.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();


		} // Solve()
	} // class ABC
}
