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
		static void Main(string[] args)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			long mod = 1000000000 + 7;

			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int n = vs[0];  // 総数
			int b = vs[1];  // 青
			int r = n - b;  // 赤

			// 順列
			// nPr = n! / (n-r)!
			// 組み合わせ
			// nCr = nPr / r! = n! / r!(n-r)!
			// 公式
			// nCr = nCn-r
			// nCr = n-1Cr + n-1Cr-1
			// nC0 = 1
			// nC1 = n
			long[,] dp = new long[n + 2, n + 2];
			for (int i = 0; i <= n; i++) {
				dp[i, 0] = 1;
			}
			for (int j = 1; j <= b; j++) {
				for (int i = j; i <= n; i++) {
					dp[i, j] = (dp[i - 1, j] + dp[i - 1, j - 1]) % mod;
				}
				// r+1Cj * b-1Cj-1
				long ans = dp[r + 1, j] * dp[b - 1, j - 1] % mod;
				Console.WriteLine(ans);
			}


			Console.Out.Flush();

			sw.Stop();
			System.Diagnostics.Debug.WriteLine(sw.Elapsed);
		}
	}
}
