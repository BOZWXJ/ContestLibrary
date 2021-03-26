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
		const int mod = 1000000007;  // 10^9+7
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

			long offset = 10000;
			long[] vs = Console.ReadLine().Split().Select(p => (long)(double.Parse(p) * offset)).ToArray();
			long x = vs[0];
			long y = vs[1];
			long r = vs[2];

			long xMin = (x - r) / offset * offset;
			long xMax = (x + r) / offset * offset;
			long yMin = y / offset * offset;
			long yMax = yMin;
			long ans = 0;
			//System.Diagnostics.Debug.WriteLine($"{(double)x / offset}:{xMin / offset}～{xMax / offset}");
			for (long i = xMin; i <= xMax; i += offset) {
				if (i <= x) {
					while (Sq(i - x) + Sq(yMin - offset - y) <= r * r) {
						yMin -= offset;
					}
					while (Sq(i - x) + Sq(yMax + offset - y) <= r * r) {
						yMax += offset;
					}
				} else {
					while (Sq(i - x) + Sq(yMin - y) > r * r) {
						yMin += offset;
					}
					while (Sq(i - x) + Sq(yMax - y) > r * r) {
						yMax -= offset;
					}
				}
				ans += (yMax - yMin) / offset + 1;
				//System.Diagnostics.Debug.WriteLine($"x={i / offset}:y={yMin / offset}～{yMax / offset}");
			}
			Console.WriteLine(ans);

		} // Solve()
	} // class ABC
}
