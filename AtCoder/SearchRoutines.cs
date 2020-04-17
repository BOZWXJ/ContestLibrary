using System;
using System.Collections.Generic;
using System.Linq;

namespace ContestLibrary
{
	class SearchRoutines
	{
		// 二分探索（存在しない場合は負、複数存在する場合は先頭）
		#region BinarySearch<T>()
		public static int BinarySearch<T>(T[] array, T value) { return BinarySearch(array, 0, array.Length - 1, value, Comparer<T>.Default.Compare); }
		public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer) { return BinarySearch(array, 0, array.Length - 1, value, comparer.Compare); }
		public static int BinarySearch<T>(T[] array, T value, Func<T, T, int> compare) { return BinarySearch(array, 0, array.Length - 1, value, compare); }
		public static int BinarySearch<T>(T[] array, int lower, int upper, T value) { return BinarySearch(array, lower, upper, value, Comparer<T>.Default.Compare); }
		public static int BinarySearch<T>(T[] array, int lower, int upper, T value, IComparer<T> comparer) { return BinarySearch(array, lower, upper, value, comparer.Compare); }
		public static int BinarySearch<T>(T[] array, int lower, int upper, T value, Func<T, T, int> compare)
		{
			if (lower > upper) {
				return ~(upper + 1);
			}
			int up = upper, mid;
			while (lower < up) {
				mid = (lower + up) / 2;
				if (compare(array[mid], value) < 0) {
					lower = mid + 1;
				} else {
					up = mid;
				}
			}
			if (compare(array[lower], value) != 0) {
				lower = ~lower;
			}
			return lower;
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
