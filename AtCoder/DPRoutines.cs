using System;
using System.Collections.Generic;
using System.Text;

namespace ContestLibrary
{
	class DPRoutines
	{
		// long[,,] dp = new long[s.Length + 1, 2, 40];
		// 桁DP(s, dp);
		// long ans -= dp[s.Length, 0, 1] + dp[s.Length, 1, 1];

		private static void 桁DP(string s, long[,,] dp)
		{
			dp[0, 1, 0] = 1;
			for (int i = 0; i < s.Length; i++) {
				int x = int.Parse(s[i].ToString());
				//System.Diagnostics.Debug.WriteLine($"n[{i}]={x}");
				for (int j = 0; j < 2; j++) {   // i桁目まで決めた時の、状態の数
					for (int k = 0; k <= 9; k++) {  // i+1桁目が k の時
						if (k == 4 || k == 9) {
							dp[i + 1, 0, 1] += dp[i, 0, j];
							//System.Diagnostics.Debug.WriteLine($"  {k}:dp[{i + 1}, 0, 1]{dp[i + 1, 0, 1]} += dp[{i}, 0, {j}]{dp[i, 0, j]}");
						} else {
							dp[i + 1, 0, j] += dp[i, 0, j];
							//System.Diagnostics.Debug.WriteLine($"  {k}:dp[{i + 1}, 0, {j}]{dp[i + 1, 0, j]} += dp[{i}, 0, {j}]{dp[i, 0, j]}");
						}
					}
					for (int k = 0; k <= x; k++) {
						if (k == 4 || k == 9) {
							dp[i + 1, (k == x ? 1 : 0), 1] += dp[i, 1, j];
							//System.Diagnostics.Debug.WriteLine($"  {k}:dp[{i + 1}, {(k == x ? 1 : 0)}, 1]{dp[i + 1, (k == x ? 1 : 0), 1]} += dp[{i}, 1, {j}]{dp[i, 1, j]}");
						} else {
							dp[i + 1, (k == x ? 1 : 0), j] += dp[i, 1, j];
							//System.Diagnostics.Debug.WriteLine($"  {k}:dp[{i + 1}, {(k == x ? 1 : 0)}, {j}]{dp[i + 1, (k == x ? 1 : 0), j]} += dp[{i}, 1, {j}]{dp[i, 1, j]}");
						}
					}
				}
				//System.Diagnostics.Debug.WriteLine("");
			}
		}

	}
}
