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


			long mod = 1000000000 + 7;

			int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			int n = vs[0];	// 総数
			int k = vs[1];	// 青

			long ans = 0;

			// 順列
			// nPr = n! / (n-r)! = n*(n-1)*(n-2)*...*(n-(r-1))

			// 組み合わせ
			// nCr = nPr / r! = n! / r!(n-r)! = n*(n-1)*(n-2)*...*(n-(r-1)) / r*(r-1)*...*1
			// nCr = nCn-r
			// nCr = n-1Cr-1 + n-1Cr


			Console.WriteLine(ans);


			Console.Out.Flush();
		}
	}
}
