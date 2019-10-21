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


			int n = int.Parse(Console.ReadLine());
			Console.WriteLine(MakeString(n, "", 0));


			Console.Out.Flush();
		}

		static int MakeString(int n, string s, int cnt)
		{
			if (n > 0) {
				cnt = MakeString(n - 1, "A" + s, cnt);
				cnt = MakeString(n - 1, "C" + s, cnt);
				cnt = MakeString(n - 1, "G" + s, cnt);
				cnt = MakeString(n - 1, "T" + s, cnt);
			} else {
				if (Regex.IsMatch(s, "AGC")) {
				} else if (Regex.IsMatch(s, "GAC")) {
				} else if (Regex.IsMatch(s, "ACG")) {
				} else if (Regex.IsMatch(s, "A.GC")) {
				} else if (Regex.IsMatch(s, "AG.C")) {
				} else {
					cnt++;
				}
			}
			return cnt;
		}

	}
}
