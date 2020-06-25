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
			Solve(args);
			Console.Out.Flush();
		}

		const int mod = 1000000007;  // 10^9+7
		static int Mod(long x, int m) { return (int)((x % m + m) % m); }
		static long Mod(long x, long m) { return (x % m + m) % m; }

		static public void Solve(string[] args)
		{
			//string s = Console.ReadLine();
			//int n = int.Parse(Console.ReadLine());
			//long x = long.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();

			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int n = vs[0];
			int a = vs[1];
			int b = vs[2];

			long ans = ModPow(2, n, mod) - 1;
			long x = Combination(n, a, mod);
			long y = Combination(n, b, mod);
			//System.Diagnostics.Debug.WriteLine($"{ans} {x} {y}");
			ans = Mod(ans - x - y, mod);
			//System.Diagnostics.Debug.WriteLine($"{ans}");
			Console.WriteLine(ans);
		}

		#region 場合の数 O(r) long Combination(long n, long r, long mod)
		static long Combination(int n, int r, long mod)
		{
			if (n < r) { return 0; }
			if (n == r) { return 1; }
			// nCr = (n * (n-1) * ... * (n-(r-1))) / (r * r-1 * ... * 2 * 1)
			long a = 1;
			long b = 1;
			for (int i = 0; i < r; i++) {
				a = Mod(a * (n - i), mod);
				b = Mod(b * (i + 1), mod);
			}
			return Mod(a * ModInv(b, mod), mod);
		}
		#endregion

		#region 逆元(拡張Euclidの互除法) long ModInv(long a, long mod)
		static long ModInv(long a, long m)
		{
			long b = m, x = 1, y = 0;
			while (b > 0) {
				long t = a / b;
				a = Mod(a, b);
				x = x - t * y;
				(a, b, x, y) = (b, a, y, x);
			}
			x = Mod(x, m);
			return x;
		}
		#endregion

		#region 繰り返し二乗法 long ModPow(long x, long y, long mod)
		static long ModPow(long x, long y, long mod)
		{
			long result = 1;
			while (y > 0) {
				if (y % 2 != 0) {
					result = Mod(result * x, mod);
				}
				x = Mod(x * x, mod);
				y /= 2;
			}
			return result;
		}
		#endregion

	}
}
