using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

		const int mod = 998244353;
		//const int mod = 1000000007;  // 1,000,000,007  10^9+7
		static int Mod(long x, int m) { return (int)((x % m + m) % m); }
		static long Mod(long x, long m) { return (x % m + m) % m; }
		static long Sq(long x) { return x * x; }

		static HashSet<string> T = new HashSet<string>();

		static public void Solve()
		{
			//string s = Console.ReadLine();
			//string[] vs = Console.ReadLine().Split();
			//int n = int.Parse(Console.ReadLine());
			//long x = long.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();

			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int N = vs[0];
			int M = vs[1];
			int len = 0;
			string[] S = new string[N];
			for (int i = 0; i < N; i++) {
				S[i] = Console.ReadLine();
				len += S[i].Length;
			}
			for (int i = 0; i < M; i++) {
				T.Add(Console.ReadLine());
			}

			Permutation prmu = new Permutation(N);
			while (true) {
				int[] p = prmu.Next();
				if (p == null) {
					break;
				}

				string[] array = new string[N + N - 1];
				for (int i = 0; i < N; i++) {
					array[i * 2] = S[p[i]];
				}
				string ans = saiki(array, 17 - len - N, 0);
				if (!string.IsNullOrEmpty(ans)) {
					Console.WriteLine(ans);
					return;
				}
			}
			Console.WriteLine("-1");

		} // Solve()

		static string saiki(string[] array, int n, int i)
		{
			i++;
			if (i == array.Length) {
				string str = string.Concat(array);
				//System.Diagnostics.Debug.WriteLine($"{n}:{str}");
				if (!T.Contains(str)) {
					return str;
				} else {
					return null;
				}
			}

			string result = null;
			for (int j = 0; j <= n; j++) {
				array[i] = new string('_', j + 1);
				result = saiki(array, n - j, i + 1);
				if (!string.IsNullOrEmpty(result)) {
					break;
				}
			}
			return result;
		}

	} // class ABC

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

}
