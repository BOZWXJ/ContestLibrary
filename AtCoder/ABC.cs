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
		static long mod = 1000000007;  // 10^9+7

		static void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });


			//int n = int.Parse(Console.ReadLine());
			ulong[] vs = Console.ReadLine().Split().Select(ulong.Parse).ToArray();
			ulong a = Math.Max(vs[0] - 1, 0);
			ulong b = vs[1];
			ulong ans = 0;
			for (int i = 0; i < 64; i++) {
				ulong c = Count1(b, i) - Count1(a, i);
				if (c % 2 == 1) {
					ans += (1UL << i);
				}
			}
			Console.WriteLine(ans);


			Console.Out.Flush();
		}

		static ulong Count1(ulong value, int pos)
		{
			ulong x = 1UL << pos;
			ulong y = x << 1;
			ulong a = 0;
			if (y > 0) {
				a = value / y * x;
			}
			ulong z = ~(0xffffffffffffffff << pos);
			ulong b = (value & x) == 0 ? 0 : (value & z) + 1;
			return a + b;
		}

	}
}
