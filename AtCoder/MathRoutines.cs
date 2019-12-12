using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestLibrary
{
	class MathRoutines
	{

		#region 素数判定 bool IsPrime(long a)
		public static bool IsPrime(long a)
		{
			if (a == 2) { return true; }
			if (a < 2 || a % 2 == 0) { return false; }

			double sqrt = Math.Sqrt(a);
			for (int i = 3; i <= sqrt; i += 2) {
				if (a % i == 0) { return false; }
			}
			return true;
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

		#region 最大公約数 long GCD(long a, long b)
		public static long GCD(long a, long b)
		{
			if (b == 0) { return a; }

			long tmp = a % b;
			while (tmp != 0) {
				a = b;
				b = tmp;
				tmp = a % b;
			}
			return b;
		}
		#endregion

		#region 最小公倍数 long LCM(long a, long b)
		public static long LCM(long a, long b)
		{
			long gcd;
			if (b == 0) {
				gcd = a;
			} else {
				long x = a, y = b;
				long tmp = x % y;
				while (tmp != 0) {
					x = y;
					y = tmp;
					tmp = x % y;
				}
				gcd = y;
			}
			return a / gcd * b;
		}
		#endregion

		// 順列
		// nPr = n! / (n-r)!
		// 組み合わせ
		// nCr = nPr / r! = n! / r!(n-r)!
		// 公式
		// nCr = nCn-r
		// nCr = n-1Cr + n-1Cr-1
		// nC0 = 1
		// nC1 = n

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

	}
}
