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
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();
			long A = vs[0];
			long B = vs[1];

			long a = 100000 - 1;
			long b = 100000 - 7;
			long gcd = GCD(a, b);
			long lcm = LCM(a, b);
			System.Diagnostics.Debug.WriteLine($"{a}:{string.Join(" ", PrimeFactorization(a))}");
			System.Diagnostics.Debug.WriteLine($"{b}:{string.Join(" ", PrimeFactorization(b))}");
			System.Diagnostics.Debug.WriteLine($"gcd {gcd}:{string.Join(" ", PrimeFactorization(gcd))}");
			System.Diagnostics.Debug.WriteLine($"lcm {lcm}:{string.Join(" ", PrimeFactorization(lcm))}");


			Console.Out.Flush();
		}

		#region 素因数分解 long[] PrimeFactorization(long a)
		static long[] PrimeFactorization(long a)
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
		static long GCD(long a, long b)
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
		static long LCM(long a, long b)
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

	}
}