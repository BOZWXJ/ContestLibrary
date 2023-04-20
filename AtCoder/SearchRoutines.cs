using System;
using System.Collections.Generic;
using System.Linq;

namespace ContestLibrary
{
	class SearchRoutines
	{
		#region 二分探索
		/// <summary>
		/// 二分探索
		/// </summary>
		/// <param name="ok">func の結果が true</param>
		/// <param name="ng">func の結果が false</param>
		/// <param name="func"></param>
		/// <returns></returns>
		static (long ok, long ng) BinarySearch(long ok, long ng, Func<long, bool> func)
		{
			while (Math.Abs(ng - ok) > 1) {
				long n = (ok + ng) / 2;
				if (func(n)) {
					ok = n;
				} else {
					ng = n;
				}
			}
			return (ok, ng);
		}
		#endregion

		#region BinarySearchFirst<T> 二分探索（同じ要素は先頭）
		public static int BinarySearchFirst<T>(T[] array, T value) { return BinarySearchFirst(array, 0, array.Length, value, Comparer<T>.Default.Compare); }
		public static int BinarySearchFirst<T>(T[] array, T value, IComparer<T> comparer) { return BinarySearchFirst(array, 0, array.Length, value, comparer.Compare); }
		public static int BinarySearchFirst<T>(T[] array, T value, Comparison<T> compare) { return BinarySearchFirst(array, 0, array.Length, value, compare); }
		public static int BinarySearchFirst<T>(T[] array, int index, int count, T value) { return BinarySearchFirst(array, index, count, value, Comparer<T>.Default.Compare); }
		public static int BinarySearchFirst<T>(T[] array, int index, int count, T value, IComparer<T> comparer) { return BinarySearchFirst(array, index, count, value, comparer.Compare); }
		public static int BinarySearchFirst<T>(T[] array, int index, int count, T value, Comparison<T> compare)
		{
			int l = index;
			int r = index + count;
			if (l > r) {
				return ~(r + 1);
			}
			int mid;
			while (l < r) {
				mid = (l + r) / 2;
				if (compare(array[mid], value) < 0) {
					l = mid + 1;
				} else {
					r = mid;
				}
			}
			if (l == index + count || compare(array[l], value) != 0) {
				l = ~l;
			}
			return l;
		}
		#endregion

		#region BinarySearchLast<T> 二分探索（同じ要素は末尾）
		public int BinarySearchLast<T>(T[] array, T item) { return BinarySearchLast(array, 0, array.Length, item, null); }
		public int BinarySearchLast<T>(T[] array, T item, IComparer<T> comparer) { return BinarySearchLast(array, 0, array.Length, item, comparer.Compare); }
		public int BinarySearchLast<T>(T[] array, T item, Comparison<T> compare) { return BinarySearchLast(array, 0, array.Length, item, compare); }
		public int BinarySearchLast<T>(T[] array, int index, int count, T item, Comparison<T> compare)
		{
			int l = index;
			int r = index + count;
			if (l > r) {
				return ~(r + 1);
			}
			int mid;
			while (l < r) {
				mid = (l + r) / 2;
				if (compare(array[mid], item) <= 0) {
					l = mid + 1;
				} else {
					r = mid;
				}
			}
			if (l == 0 || compare(array[l - 1], item) != 0) {
				l = ~l;
			} else {
				l -= 1;
			}
			return l;
		}
		#endregion

		// value 以上
		#region lower_bound<T>()
		public static int Lower_bound<T>(T[] array, T value) { return lower_bound(array, 0, array.Length - 1, value, Comparer<T>.Default.Compare); }
		public static int lower_bound<T>(T[] array, T value, IComparer<T> comparer) { return lower_bound(array, 0, array.Length - 1, value, comparer.Compare); }
		public static int lower_bound<T>(T[] array, T value, Func<T, T, int> compare) { return lower_bound(array, 0, array.Length - 1, value, compare); }
		public static int lower_bound<T>(T[] array, int lower, int upper, T value) { return lower_bound(array, lower, upper, value, Comparer<T>.Default.Compare); }
		public static int lower_bound<T>(T[] array, int lower, int upper, T value, IComparer<T> comparer) { return lower_bound(array, lower, upper, value, comparer.Compare); }
		public static int lower_bound<T>(T[] array, int lower, int upper, T value, Func<T, T, int> compare)
		{
			if (lower > upper) { return -1; }
			int up = upper, mid;
			while (lower < up) {
				mid = (lower + up) / 2;
				if (compare(array[mid], value) < 0) {
					lower = mid + 1;
				} else {
					up = mid;
				}
			}
			return lower;
		}
		#endregion

		// value より大きい
		#region Upper_bound<T>()
		public static int Upper_bound<T>(T[] array, T value) { return Upper_bound(array, 0, array.Length - 1, value, Comparer<T>.Default.Compare); }
		public static int Upper_bound<T>(T[] array, T value, IComparer<T> comparer) { return Upper_bound(array, 0, array.Length - 1, value, comparer.Compare); }
		public static int Upper_bound<T>(T[] array, T value, Func<T, T, int> compare) { return Upper_bound(array, 0, array.Length - 1, value, compare); }
		public static int Upper_bound<T>(T[] array, int lower, int upper, T value) { return Upper_bound(array, lower, upper, value, Comparer<T>.Default.Compare); }
		public static int Upper_bound<T>(T[] array, int lower, int upper, T value, IComparer<T> comparer) { return Upper_bound(array, lower, upper, value, comparer.Compare); }
		public static int Upper_bound<T>(T[] array, int lower, int upper, T value, Func<T, T, int> compare)
		{
			if (lower > upper) { return -1; }
			int up = upper, mid;
			while (lower < up) {
				mid = (lower + up) / 2;
				if (compare(array[mid], value) <= 0) {
					lower = mid + 1;
				} else {
					up = mid;
				}
			}
			return lower;
		}
		#endregion

	}
}
