using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtCoder
{
	class MathRoutines
	{

		#region 最大公約数
		static long GCD(long a, long b)
		{
			long tmp;
			if (a < b) {
				tmp = a;
				a = b;
				b = tmp;
			}
			if (b == 0) {
				return a;
			}
			do {
				tmp = a % b;
				a = b;
				b = tmp;
			} while (b != 0);
			return a;
		}
		#endregion

		#region 素因数分解
		static long[] PrimeFactorization(long x)
		{
			if (x < 2) { return new long[] { }; }

			List<long> ans = new List<long>();
			while (x % 2 == 0) {
				ans.Add(2);
				x /= 2;
			}
			for (int i = 3; i <= Math.Sqrt(x); i += 2) {
				while (x % i == 0) {
					ans.Add(i);
					x /= i;
				}
			}
			if (x > 1) {
				ans.Add(x);
			}
			return ans.ToArray();
		}
		#endregion

	}
}
