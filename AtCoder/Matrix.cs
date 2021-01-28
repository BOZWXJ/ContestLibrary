using System;
using System.Collections.Generic;
using System.Text;

namespace ContestLibrary
{
	#region 行列計算
	public static class Matrix
	{
		/// <summary>x行y列の行列を作成する</summary>
		public static long[][] Create(int x, int y)
		{
			long[][] result = new long[x][];
			for (int i = 0; i < x; i++) {
				result[i] = new long[y];
			}
			return result;
		}

		/// <summary>n行n列の単位行列を作成する</summary>
		public static long[][] Identity(int n)
		{
			long[][] result = new long[n][];
			for (int i = 0; i < n; ++i) {
				result[i] = new long[n];
				result[i][i] = 1;
			}
			return result;
		}

		/// <summary>行列を文字列に変換する</summary>
		public static string AsString(long[][] matrix, string separator = ",", int width = 0)
		{
			string result = "";
			string f = $"{{0,{width}}}{separator}";
			foreach (var row in matrix) {
				foreach (var i in row) {
					result += string.Format(f, i);
				}
				result = result.Substring(0, result.Length - separator.Length);
				result += Environment.NewLine;
			}
			return result.TrimEnd();
		}

		/// <summary>行列 乗算</summary>
		public static long[][] Product(long[][] a, long[][] b)
		{
			if (a[0].Length != b.Length) { throw new Exception("Non-conformable matrices in MatrixProduct"); }
			long[][] result = new long[a.Length][];
			for (int i = 0; i < a.Length; i++) {
				result[i] = new long[b[0].Length];
				for (int j = 0; j < b[0].Length; j++) {
					for (int k = 0; k < a[0].Length; k++) {
						result[i][j] += a[i][k] * b[k][j];
					}
				}
			}
			return result;
		}
	}
	#endregion

	// (x,y) → (x',y')
	// ○一次変換
	// x' = ax + by
	// y' = cx + dy
	// ・拡大縮小
	// x' = ax
	// y' = dy
	// ⎡x'⎤=⎡a 0⎤⎡x⎤
	// ⎣y'⎦ ⎣0 d⎦⎣y⎦
	// ・回転
	// x' = cosθx - sinθy
	// y' = sinθx + cosθy
	// ⎡x'⎤=⎡cosθ -sinθ⎤⎡x⎤
	// ⎣y'⎦ ⎣sinθ  cosθ⎦⎣y⎦
	// 90°     180°     270°
	// ⎡0 -1⎤ ⎡-1  0⎤ ⎡ 0 1⎤
	// ⎣1  0⎦ ⎣ 0 -1⎦ ⎣-1 0⎦
	// ・せん断
	// x' = x
	// y' = tanθx + y
	// ⎡x'⎤   ⎡    1 0⎤⎡x⎤
	// ⎣y'⎦ = ⎣tanθ 1⎦⎣y⎦
	// x' = x + tanθy
	// y' = y
	// ⎡x'⎤   ⎡1 tanθ⎤⎡x⎤
	// ⎣y'⎦ = ⎣0     1⎦⎣y⎦
	//
	// ○アフィン変換（一次変換に平行移動を加えた物）
	// x' = ax + by + x0
	// y' = cx + dy + y0
	// ⎡x'⎤   ⎡a b x0⎤⎡x⎤
	// ⎢y'⎥ = ⎢c d y0⎥⎢y⎥
	// ⎣1 ⎦   ⎣0 0 1 ⎦⎣1⎦
	// ・平行移動
	// x' = x + x0
	// y' = y + y0
	// ⎡x'⎤   ⎡1 0 x0⎤⎡x⎤
	// ⎢y'⎥ = ⎢0 1 y0⎥⎢y⎥
	// ⎣1 ⎦   ⎣0 0 1 ⎦⎣1⎦
	// ・拡大縮小
	// x' = ax
	// y' = dy
	// ⎡x'⎤   ⎡a 0 0⎤⎡x⎤
	// ⎢y'⎥ = ⎢0 d 0⎥⎢y⎥
	// ⎣1 ⎦   ⎣0 0 1⎦⎣1⎦
	// ・回転
	// x' = cosθx - sinθy
	// y' = sinθx + cosθy
	// ⎡x'⎤   ⎡cosθ -sinθ 0⎤⎡x⎤
	// ⎢y'⎥ = ⎢sinθ  cosθ 0⎥⎢y⎥
	// ⎣1 ⎦   ⎣    0      0 1⎦⎣1⎦
	// 90°       180°       270°
	// ⎡0 -1 0⎤ ⎡-1  0 0⎤ ⎡ 0 1 0⎤
	// ⎢1  0 0⎥ ⎢ 0 -1 0⎥ ⎢-1 0 0⎥
	// ⎣0  0 1⎦ ⎣ 0  0 1⎦ ⎣ 0 0 1⎦
	// ・せん断
	// x' = x
	// y' = tanθx + y
	// ⎡x'⎤   ⎡    1 0 0⎤⎡x⎤
	// ⎢y'⎥ = ⎢tanθ 1 0⎥⎢y⎥
	// ⎣1 ⎦   ⎣    0 0 1⎦⎣1⎦
	// x' = x + tanθy
	// y' = y
	// ⎡x'⎤   ⎡1 tanθ 0⎤⎡x⎤
	// ⎢y'⎥ = ⎢0     1 0⎥⎢y⎥
	// ⎣1 ⎦   ⎣0     0 1⎦⎣1⎦
	//
	// ○アフィン変換の合成
	// A1 → A2 → A3 ... An
	// An * ... A3 * A2 * A1
}
