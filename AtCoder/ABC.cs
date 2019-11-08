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


			int n = int.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();

			long ans = Dfs(n, "");
			Console.WriteLine(ans);


			Console.Out.Flush();
		}

		static readonly Dictionary<string, long> memo = new Dictionary<string, long>();
		static long Dfs(int len, string s)
		{
			var key = $"{len}{s}";
			if (memo.ContainsKey(key)) {
				return memo[key];
			}
			if (len == 0) {
				return 1;
			}
			long result = 0;
			foreach (var c in "ACGT") {
				string next = c + s;
				if (next.Length > 4) {
					next = next.Substring(0, 4);
				}
				if (Check(next)) {
					result = (result + Dfs(len - 1, next)) % mod;
				}
			}
			memo.Add(key, result);
			return result;
		}

		static bool Check(string s)
		{
			if (s.Length >= 3) {
				// AGC
				if (s[0] == 'A' && s[1] == 'G' && s[2] == 'C') { return false; }
				// GAC
				if (s[0] == 'G' && s[1] == 'A' && s[2] == 'C') { return false; }
				// ACG
				if (s[0] == 'A' && s[1] == 'C' && s[2] == 'G') { return false; }
			}
			if (s.Length >= 4) {
				// A.GC
				if (s[0] == 'A' && s[2] == 'G' && s[3] == 'C') { return false; }
				// AG.C
				if (s[0] == 'A' && s[1] == 'G' && s[3] == 'C') { return false; }
			}
			return true;
		}

	}
}
