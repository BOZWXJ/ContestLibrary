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


			double[] vs = Console.ReadLine().Split().Select(double.Parse).ToArray();
			double a = vs[0];
			double b = vs[1];
			double x = vs[2] / a;
			double diff = x / 100000000000;
			double s = a * b / 2;
			bool f = s < x;

			double max = f ? b : a;
			double min = 0;
			double c;
			double deg;
			while (true) {
				c = (max + min) / 2;
				if (f) {
					// 対角より多い
					s = a * c + a * (b - c) / 2;
				} else {
					// 対角より少ない
					s = c * b / 2;
		}

				if (Math.Abs(s - x) < diff) {
					if (f) {
						// 対角より多い
						deg = Math.Atan((b - c) / a) * 180 / Math.PI;
				} else {
						// 対角より少ない
						deg = Math.Atan(b / c) * 180 / Math.PI;
				}
					Console.WriteLine(deg);
					break;
				} else if (s < x) {
					min = c;
				} else {
					max = c;
			}
		}


			Console.Out.Flush();
		}
	}
}
