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

		private const int mod = 1000000007;  // 10^9+7

		static public void Solve(string[] args)
		{
			//string s = Console.ReadLine();
			//int n = int.Parse(Console.ReadLine());
			//long x = long.Parse(Console.ReadLine());
			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();

			long a = 12345678900000;
			long b = 100000;
			System.Diagnostics.Debug.WriteLine($"{a} / {b} = {a / b}");
			a %= mod;
			System.Diagnostics.Debug.WriteLine($"{a} / {b} = {a * ModInv(b, mod) % mod}");


			//int n = vs[0];
			//int a = vs[1];
			//int b = vs[2];
			//long ans = ModPow(2, n, mod) - 1;
			//System.Diagnostics.Debug.WriteLine($"{ans}");
			//ans -= Combination(n, a, mod);
			//ans = (ans + mod) % mod;
			//System.Diagnostics.Debug.WriteLine($"{ans}");
			//ans -= Combination(n, b, mod);
			//ans = (ans + mod) % mod;
			//System.Diagnostics.Debug.WriteLine($"{ans}");
			//Console.WriteLine(ans);
		}

		#region 逆元（拡張Euclidの互除法） long ModInv(long a, long mod)
		static long ModInv(long a, long mod)
		{
			long result = 1;


			return result;
		}
		#endregion



		#region 繰り返し二乗法 long ModPow(long x, long y, long mod)
		static long ModPow(long x, long y, long mod)
		{
			long result = 1;
			while (y > 0) {
				if (y % 2 == 1) {
					result = result * x % mod;
				}
				x = x * x % mod;
				y /= 2;
			}
			return result;
		}
		#endregion

		#region 場合の数 long Combination(long n, long r, long mod)
		static long Combination(long n, long r, long mod)
		{
			if (n < r) { return 0; }
			r = Math.Min(r, n - r);
			long ans = 1;
			for (int i = (int)n; i > n - r; i--) {
				ans *= i;
			}
			for (int i = 2; i <= r; i++) {
				ans /= i;
			}
			return ans;
		}
		#endregion

	}
}
