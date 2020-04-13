using System;
using System.Collections.Generic;
using System.Linq;

namespace ContestLibrary
{
	class SearchRoutines
	{

		#region 二分探索 int BinarySearch<T>(T[] array, int lower, int upper, T value, Comparison<T> compare)
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
			int r = compare(array[lower], value);
			if (r != 0) {
				if (lower == upper && r < 0) {
					lower++;
				}
				lower = ~lower;
			}
			return lower;
		}
		#endregion

	}
}
