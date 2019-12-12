using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AtCoder
{
	public class ABC
	{
		static readonly int mod = 1000000007;  // 10^9+7

		static void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			//int n = int.Parse(Console.ReadLine());
			//string s = Console.ReadLine();
			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] a = Console.ReadLine().Split().Select(long.Parse).ToArray();
			int n = vs[0];
			int m = vs[1];

			long[] primes = PrimeFactorization(m);
			//System.Diagnostics.Debug.WriteLine($"{string.Join(" ", primes)}");
			long ans = 1;
			int c = 1;
			for (int i = 0; i < primes.Length; i++) {
				if (i + 1 < primes.Length && primes[i] == primes[i + 1]) {
					c++;
				} else {
					long tmp = CombinationDP(c + n - 1, c, mod);
					//System.Diagnostics.Debug.WriteLine($"{primes[i]}:{c + n - 1} C {c} = {tmp}");
					ans *= tmp;
					ans %= mod;
					c = 1;
				}
			}
			Console.WriteLine(ans);


			Console.Out.Flush();
		}

		#region 場合の数（DP計算版） long CombinationDP(int n, int r, int mod)
		static long CombinationDP(int n, int r, int mod)
		{
			r = Math.Min(r, n - r);
			long[,] dp = new long[n + 1, r + 1];
			dp[0, 0] = 1;
			for (int i = 1; i <= n; i++) {
				dp[i, 0] = 1;
				for (int j = 1; j <= i && j <= r; j++) {
					dp[i, j] = (dp[i - 1, j - 1] + dp[i - 1, j]) % mod;
				}
			}
			return dp[n, r];
		}
		#endregion

		#region 素因数分解 long[] PrimeFactorization(long a)
		public static long[] PrimeFactorization(long a)
		{
			if (a < 2) { return new long[] { }; }

			List<long> ans = new List<long>();
			while (a % 2 == 0) {
				ans.Add(2);
				a /= 2;
			}
			for (int i = 3; i <= Math.Sqrt(a); i += 2) {
				while (a % i == 0) {
					ans.Add(i);
					a /= i;
				}
			}
			if (a > 1) {
				ans.Add(a);
			}
			return ans.ToArray();
		}
		#endregion

	}
}
