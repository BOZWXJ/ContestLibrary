using System;
using System.Collections.Generic;
using System.Linq;

namespace ContestLibrary
{
	class MathRoutines
	{
		static int Mod(long x, int m) { return (int)((x % m + m) % m); }
		static long Mod(long x, long m) { return (x % m + m) % m; }
		static long Squared(long x) { return x * x; }
		static long Cubed(long x) { return x * x * x; }

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

		#region 素数 GetPrimeNumbers(int max)
		private static List<int> GetPrimeNumbers(int max)
		{
			bool[] sieve = new bool[max / 2];
			for (int i = 3; i < sieve.Length; i += 2) {
				int j = i / 2 - 1 + i;
				while (j < sieve.Length) {
					sieve[j] = true;
					j += i;
				}
			}
			List<int> prime = new List<int>();
			prime.Add(2);
			for (int i = 0; i < sieve.Length; i++) {
				if (!sieve[i]) {
					prime.Add((i + 1) * 2 + 1);
				}
			}
			return prime;
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
			while (b != 0) {
				a %= b;
				(a, b) = (b, a);
			}
			return a;
		}
		#endregion

		#region 最小公倍数 long LCM(long a, long b)
		public static long LCM(long a, long b)
		{
			long x = a, y = b;
			while (y != 0) {
				x %= y;
				(x, y) = (y, x);
			}
			return a / x * b;
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

		#region 階乗(メモ付) long Factorial(int a, long mod)
		static long Factorial(int a, long mod)
		{
			if (a < _Factorial.Count) {
				return _Factorial[a];
			}
			int st = _Factorial.Count;
			long result = st > 0 ? _Factorial[st - 1] : 1;
			for (int i = st; i <= a; i++) {
				result *= i;
				result = Mod(result, mod);
				_Factorial.Add(result);
			}
			return result;
		}
		static List<long> _Factorial = new List<long>(new long[] { 1 });
		#endregion

		// ax + py = 1
		//	b = p
		// ax + by = 1
		//	a=qb+r
		// (qb+r)x + by = 1
		// qbx + rx + by = 1
		// b(qx+y) + rx = 1
		// b(qx+y) + rx = 1
		//	a=b, b=r=a%b, x=x-qy=x-a/b*y ?, y=x
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

		// a^p−1≡1(mod p)
		#region 逆元(Fermatの小定理) long ModInv1(long a, long mod)
		static long ModInv1(long a, long m)
		{
			long b = m - 2;
			long result = 1;
			while (b > 0) {
				if (b % 2 != 0) {
					result = Mod(result * a, m);
				}
				a = Mod(a * a, m);
				b /= 2;
			}
			return result;
		}
		#endregion

		#region 拡張Euclidの互除法(再帰) ax + by = GCD(a,b)
		static long ExtGCD(long a, long b, ref long x, ref long y)
		{
			if (b == 0) {
				x = 1;
				y = 0;
				return a;
			}
			long d = ExtGCD(b, a % b, ref y, ref x);
			y -= a / b * x;
			return d;
		}
		#endregion

		// 等差数列
		// 和

		// 順列
		// nPr = n! / (n-r)!
		// 組み合わせ
		// nCr = nPr / r! = n! / r!(n-r)!
		// nCr = (n * (n-1) * ... * (n-(r-1))) / (r * r-1 * ... * 2 * 1)
		//     = (n から n-(r-1) まで r個の総乗) / (r から 1 まで r個の総乗)
		// 公式
		// nCr = nCn-r
		// nCr = n-1Cr + n-1Cr-1
		// nC0 = 1
		// nC1 = n

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

		#region 場合の数(階乗) long CombinationFactorial(long n, long r, long mod)
		static long CombinationFactorial(int n, int r, long mod)
		{
			if (n < r) { return 0; }
			if (n == r) { return 1; }
			// n! / r!(n-r)!
			long result = Factorial(n, mod);
			result *= ModInv(Factorial(r, mod), mod);
			result = Mod(result, mod);
			result *= ModInv(Factorial(n - r, mod), mod);
			result = Mod(result, mod);
			return result;
		}
		#endregion

		#region 場合の数(rが一桁程度) long Combination(int n, int r)
		static long Combination(int n, int r)
		{
			if (n < r) { return 0; }
			r = Math.Min(r, n - r);
			long ans = 1;
			for (int i = n; i > n - r; i--) {
				ans *= i;
			}
			for (int i = 2; i <= r; i++) {
				ans /= i;
			}
			return ans;
		}
		#endregion

		#region 場合の数(DP計算版) long CombinationDP(int n, int r, int mod)
		static long CombinationDP(int n, int r, int mod)
		{
			if (n < r) { return 0; }
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

		#region 場合の数(Cache付きDP計算版 long CombinationCacheDP(int n, int r, int mod)
		static long CombinationCacheDP(int n, int r, int mod)
		{
			if (n < r) { return 0; }
			r = Math.Min(r, n - r);
			for (int i = _CombinationCacheDP.Count; i <= n; i++) {
				_CombinationCacheDP.Add(new long[i / 2 + 1]);
				_CombinationCacheDP[i][0] = 1;
				for (int j = 1; j < _CombinationCacheDP[i].Length; j++) {
					int k = j < _CombinationCacheDP[i - 1].Length ? j : _CombinationCacheDP[i - 1].Length - 1;
					_CombinationCacheDP[i][j] = (_CombinationCacheDP[i - 1][j - 1] + _CombinationCacheDP[i - 1][k]) % mod;
				}
				//System.Diagnostics.Debug.WriteLine($"{i,2}:{string.Join(" ", _CombinationCacheDP[i].Select(p => p.ToString().PadLeft(3)))}");
			}
			return _CombinationCacheDP[n][r];
		}
		static readonly List<long[]> _CombinationCacheDP = new List<long[]>(new long[][] { new long[] { 1 } });
		#endregion

		// マンハッタン距離
		// |xi - xj| + |yi - yj|
		// 45度回転
		// =max( xi-xj, xj-xi ) + max( yi-yj, yj-yi )
		// =max( (xi-xj)+(yi-yj), (xi-xj)+(yj-yi),  (xj-xi)+(yi-yj),  (xj-xi)+(yj-yi) )
		// =max( (xi+yi)-(xj+yj), (xi-yi)-(xj-yj), -(xi-yi)+(xj-yj), -(xi+yi)+(xj+yj) )
		// =max( (xi+yi)-(xj+yj), (xj+yj)-(xi+yi),  (xi-yi)-(xj-yj),  (xj-yj)-(xi-yi) )
		// =max( |(xi+yi)-(xj+yj)|, |(xi-yi)-(xj-yj)| )
		// z = x + y
		// w = x - y
		// = max( | zi - zj |, | wi - wj | )


	}
}
