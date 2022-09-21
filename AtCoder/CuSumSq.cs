using System;
using System.Collections.Generic;
using System.Text;

namespace ContestLibrary
{
	// 二次元累積和

	#region 二次元累積和 CuSumSq
	class CuSumSq
	{
		public readonly long[,] sum;

		public CuSumSq(int h, int w)
		{
			sum = new long[h, w];
		}

		public long this[int index1, int index2]
		{
			get { return sum[index1, index2]; }
			set { sum[index1, index2] = value; }
		}

		public void Clear()
		{
			Array.Clear(sum, 0, sum.Length);
		}

		public void Build()
		{
			for (int i = 0; i < sum.GetLength(0); i++) {
				for (int j = 0; j < sum.GetLength(1); j++) {
					if (0 < i) {
						sum[i, j] += sum[i - 1, j];
					}
					if (0 < j) {
						sum[i, j] += sum[i, j - 1];
					}
					if (0 < i && 0 < j) {
						sum[i, j] -= sum[i - 1, j - 1];
					}
				}
			}
		}

		public long Get(int x, int y, int w, int h)
		{
			w--;
			h--;
			long result = sum[y + h, x + w];
			if (0 < y) {
				result -= sum[y - 1, x + w];
			}
			if (0 < x) {
				result -= sum[y + h, x - 1];
			}
			if (0 < y && 0 < x) {
				result += sum[y - 1, x - 1];
			}
			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < sum.GetLength(0); i++) {
				sb.Append($"{i}:");
				for (int j = 0; j < sum.GetLength(1); j++) {
					if (j > 0) {
						sb.Append(" ");
					}
					sb.Append(sum[i, j]);
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}
	}
	#endregion

	static class CuSumSqExtension
	{

	}

}
