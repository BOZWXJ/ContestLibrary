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

			long[] fa = PrimeFactorization(A);
			long[] fb = PrimeFactorization(B);

			long tmp;
			List<long> pa = new List<long>();
			if (fa.Length > 0) {
				pa.Add(fa[0]);
				tmp = fa[0];
				for (int i = 1; i < fa.Length; i++) {
					if (tmp != fa[i]) {
						pa.Add(fa[i]);
						tmp = fa[i];
					}
				}
			}
			List<long> pb = new List<long>();
			if (fb.Length > 0) {
				pb.Add(fb[0]);
				tmp = fb[0];
				for (int i = 1; i < fb.Length; i++) {
					if (tmp != fb[i]) {
						pb.Add(fb[i]);
						tmp = fb[i];
					}
				}
			}
			int ans = 1;
			int ia = 0, ib = 0;
			while (ia < pa.Count && ib < pb.Count) {
				if (pa[ia] == pb[ib]) {
					ans++;
					ia++;
					ib++;
				} else if (pa[ia] < pb[ib]) {
					ia++;
				} else {
					ib++;
				}
			}
			Console.WriteLine(ans);


			Console.Out.Flush();
		}

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