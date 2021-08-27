using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
		const int mod = 1000000007;  // 1,000,000,007  10^9+7
		static int Mod(long x, int m) { return (int)((x % m + m) % m); }
		static long Mod(long x, long m) { return (x % m + m) % m; }
		static long Sq(long x) { return x * x; }

		static public void Solve()
		{
			//string s = Console.ReadLine();
			//int n = int.Parse(Console.ReadLine());
			//long x = long.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();

			string k = Console.ReadLine();
			int d = int.Parse(Console.ReadLine());

			long[,,] dp = new long[k.Length + 1, 2, d + 1];
			桁DP(k, d, dp);

			long ans = dp[k.Length, 0, 0] + dp[k.Length, 1, 0] - 1;
			ans = Mod(ans, mod);

			Console.WriteLine(ans);

		} // Solve()


		private static void 桁DP(string s, int d, long[,,] dp)
		{
			dp[0, 1, 0] = 1;
			for (int i = 0; i < s.Length; i++) {
				int x = int.Parse(s[i].ToString());
				//System.Diagnostics.Debug.WriteLine($"n[{i}]={x}");
				for (int j = 0; j < d; j++) {   // i桁目まで決めた時の、状態の数
					for (int k = 0; k <= 9; k++) {  // 
						dp[i + 1, 0, (j + k) % d] += dp[i, 0, j];
						dp[i + 1, 0, (j + k) % d] %= mod;
						//System.Diagnostics.Debug.WriteLine($"dp[{i + 1}, 0, {(j + k) % d}]{dp[i + 1, 0, (j + k) % d]} += dp[{i}, 0, {j}]{dp[i, 0, j]}");
					}
					for (int k = 0; k <= x; k++) {
						dp[i + 1, (k == x ? 1 : 0), (j + k) % d] += dp[i, 1, j];
						dp[i + 1, (k == x ? 1 : 0), (j + k) % d] %= mod;
						//System.Diagnostics.Debug.WriteLine($"dp[{i + 1}, {(k == x ? 1 : 0)}, {(j + k) % d}]{dp[i + 1, (k == x ? 1 : 0), (j + k) % d]} += dp[{i}, 1, {j}]{dp[i, 1, j]}");
					}
				}
				//System.Diagnostics.Debug.WriteLine("");
			}
		}


	} // class ABC
}
