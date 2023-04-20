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

		#region 1～n の合計
		public static long Total(long n)
		{
			return n * (n + 1) / 2;
		}
		#endregion

		#region a～b の合計
		public static long Total(long a, long b)
		{
			return (a + b) * (b - a + 1) / 2;
		}
		#endregion

		#region 等差数列の和
		public static long ArithmeticSeries(long a, long l, long n)
		{
			return (a + l) * n / 2;
		}
		#endregion

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

		#region 約数 long[] Divisor(long a)
		public static long[] Divisor(long a)
		{
			List<long> result = new List<long>();
			for (long i = 1; i * i <= a; i++) {
				if (a % i == 0) {
					result.Add(i);
				}
			}
			int c = result.Count - 1;
			if (result[c] == a / result[c]) {
				c--;
			}
			for (int i = c; i >= 0; i--) {
				result.Add(a / result[i]);
			}
			return result.ToArray();
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
		// s = (1/2)n(2a+(n-1)d)

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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="n"></param>
		/// <param name="r"></param>
		/// <param name="mod"></param>
		/// <returns></returns>
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


		// 順列をすべて列挙する
		// 10P10 00:00:53.8844800
		public static IEnumerable<T[]> GetPermutation<T>(IEnumerable<T> collection)
		{
			// 未確定要素が一個のみ
			if (collection.Count() == 1) {
				yield return new T[] { collection.First() };
			}
			foreach (var item in collection) {
				var selected = new T[] { item };
				var unused = collection.Except(selected);
				// 確定した要素以外の組み合わせを再帰で取り出し連結
				foreach (var rightside in GetPermutation(unused)) {
					yield return selected.Concat(rightside).ToArray();
				}
			}
		}

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

	// 順列の生成
	// 10,6 00:00:00.0667722
	#region 順列の生成
	public class Permutation
	{
		public int N { get; private set; }
		public int K { get; private set; }
		public int[] Index { get; private set; }
		public int[] Result { get; private set; }

		public Permutation(int n) : this(n, n) { }
		public Permutation(int n, int k)
		{
			N = n;
			K = k;
		}

		public void Reset()
		{
			Index = new int[N];
			for (int i = 0; i < N; i++) {
				Index[i] = -1;
			}
			Result = new int[K];
			for (int i = 0; i < K; i++) {
				Index[i] = i;
				Result[i] = i;
			}
		}

		public int[] Next()
		{
			if (Result == null) {
				Reset();
			} else {
				int i = K - 1;
				int j = Result[i];
				Index[j] = -1;

				// 次のパターンの開始位置
				while (true) {
					j++;
					if (j < N) {
						if (Index[j] == -1) {
							break;
						}
					} else {
						i--;
						if (i >= 0) {
							j = Result[i];
							Index[j] = -1;
						} else {
							return null;
						}
					}
				}
				Result[i] = j;
				Index[j] = i;
				// 次のパターンの残りを埋める
				j = 0;
				while (i < K - 1) {
					i++;
					while (Index[j] != -1) {
						j++;
					}
					Result[i] = j;
					Index[j] = i;
				}
			}
			return Result;
		}
	}
	#endregion

	// 組合せの生成
	// 10,6 00:00:00.0017999
	#region 組合せの生成
	public class Combination
	{
		public int N { get; private set; }
		public int K { get; private set; }
		public int[] Result { get; private set; }

		public Combination(int n, int k)
		{
			N = n;
			K = k;
		}

		public int[] Next()
		{
			if (Result == null) {
				Result = new int[K];
				for (int j = 0; j < K; j++) {
					Result[j] = j;
				}
				return Result;
			}
			int i = K - 1;
			while (0 <= i) {
				int x = Result[i];
				if (x + (K - i) < N) {
					for (int j = i; j < K; j++) {
						x++;
						Result[j] = x;
					}
					return Result;
				}
				i--;
			}
			return null;
		}
	}
	#endregion

	// 順列の生成
	// 10,6 00:00:00.7835979
	#region 順列の生成
	public class Permutation2
	{
		public int N { get; private set; }
		public int K { get; private set; }
		private readonly PriorityQueue<(int[] pattern, int[] flgs)> queue;
		public Permutation2(int n, int k)
		{
			N = n;
			K = k;
			int[] all = new int[N];
			for (int i = 0; i < N; i++) {
				all[i] = i;
			}

			queue = new PriorityQueue<(int[] pattern, int[] flgs)>((x, y) => y.pattern.Length.CompareTo(x.pattern.Length));
			for (int i = 0; i < N; i++) {
				var (pattern, flgs) = MakePattern(new int[] { }, all, i);
				queue.Enqueue((pattern, flgs));
			}
		}

		public int[] Next()
		{
			if (queue.Count == 0) {
				return null;
			}
			while (true) {
				var (pattern, flgs) = queue.Dequeue();
				if (pattern.Length == K) {
					return pattern;
				} else {
					for (int i = 0; i < flgs.Length; i++) {
						var next = MakePattern(pattern, flgs, flgs[i]);
						queue.Enqueue(next);
					}
				}
			}
		}

		private (int[], int[]) MakePattern(int[] pattern, int[] flgs, int i)
		{
			int[] p = pattern.Append(i).ToArray();
			int[] f = flgs.Where(p => p != i).ToArray();
			return (p, f);
		}

		#region 優先度付きキュー PriorityQueue<T>
		/// <summary>
		/// 優先度付きキュー
		/// 優先度が同じ場合、挿入順に取出される。
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		class PriorityQueue<T> // where T : IComparable<T>
		{
			public int Count => buffer.Count;
			public T Top => buffer[0].value;

			private int id = 0;
			private readonly List<(T value, int id)> buffer;
			private readonly Func<(T value, int id), (T value, int id), int> compare;

			public PriorityQueue() : this(Comparer<T>.Default.Compare) { }
			public PriorityQueue(Func<T, T, int> compare)
			{
				buffer = new List<(T value, int id)>();
				this.compare = (x, y) => {
					int result = compare(x.value, y.value);
					if (result == 0) {
						result = -x.id.CompareTo(y.id);
					}
					return result;
				};
			}

			public void Enqueue(T elem)
			{
				int n = Count;
				buffer.Add((elem, id++));
				while (n != 0) {
					int i = (n - 1) / 2;
					if (compare(buffer[n], buffer[i]) > 0) {
						(buffer[i], buffer[n]) = (buffer[n], buffer[i]);
					}
					n = i;
				}
			}

			public T Dequeue()
			{
				T result = buffer[0].value;

				int n = Count - 1;
				buffer[0] = buffer[n];
				buffer.RemoveAt(n);
				for (int i = 0, j; (j = 2 * i + 1) < n;) {
					if ((j != n - 1) && (compare(buffer[j], buffer[j + 1]) < 0)) {
						j++;
					}
					if (compare(buffer[i], buffer[j]) < 0) {
						(buffer[i], buffer[j]) = (buffer[j], buffer[i]);
					}
					i = j;
				}
				return result;
			}

			public T this[int i]
			{
				get { return buffer[i].value; }
			}
		}
		#endregion
	}
	#endregion

	// 順列の生成
	// 10,6 00:00:00.2586927
	// 組合せの生成
	// 10,6 00:00:00.0147173
	#region 順列、組合せの生成
	public static class IEnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> items, int? k = null)
		{
			k ??= items.Count();
			if (k == 0) {
				yield return Enumerable.Empty<T>();
			} else {
				var i = 0;
				foreach (var x in items) {
					var xs = items.Where((_, index) => i != index);
					foreach (var c in Perm(xs, k - 1)) {
						yield return c.Before(x);
					}
					i++;
				}
			}
		}

		// 要素をシーケンスに追加するユーティリティ
		public static IEnumerable<T> Before<T>(this IEnumerable<T> items, T first)
		{
			yield return first;
			foreach (var i in items) {
				yield return i;
			}
		}

		public static IEnumerable<IEnumerable<T>> Comb<T>(this IEnumerable<T> items, int r)
		{
			if (r == 0) {
				yield return Enumerable.Empty<T>();
			} else {
				var i = 1;
				foreach (var x in items) {
					var xs = items.Skip(i);
					foreach (var c in Comb(xs, r - 1)) {
						yield return c.Before(x);
					}
					i++;
				}
			}
		}
	}
	#endregion

}
