using ContestLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtCoder
{
	public class ABC
	{
		static public void Main(string[] args)
		{
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			Solve(args);
			Console.Out.Flush();
		}

		private const int mod = 1000000007;  // 10^9+7

		static public void Solve(string[] args)
		{
			//string s = Console.ReadLine();
			//int n = int.Parse(Console.ReadLine());
			//long x = long.Parse(Console.ReadLine());
			//int[] vs = Console.ReadLine().Split().Select(int.Parse).ToArray();
			//long[] vs = Console.ReadLine().Split().Select(long.Parse).ToArray();

			Random rand = new Random(0);

			ContestLibrary.BinarySearchTree<int> tree = new ContestLibrary.BinarySearchTree<int>();
			foreach (var item in tree) {
				System.Diagnostics.Debug.Write($"{item} ");
			}
			for (int i = 0; i < 50; i++) {
				int r = rand.Next(0, 100);
				tree.Add(r);
				System.Diagnostics.Debug.WriteLine($"# Add({r})");
			}
			System.Diagnostics.Debug.WriteLine($"Count={tree.Count}");
			tree.CheckNode();
			tree.DebugPrint();
			tree.Remove(72);
			tree.GraphvizPrint();
			System.Diagnostics.Debug.WriteLine($"Count={tree.Count}");
			tree.CheckNode();
			foreach (var item in tree) {
				System.Diagnostics.Debug.Write($"{item} ");
			}
			System.Diagnostics.Debug.WriteLine("");
			tree.Clear();
			System.Diagnostics.Debug.WriteLine("Clear()");
			System.Diagnostics.Debug.WriteLine($"Count={tree.Count}");
			foreach (var item in tree) {
				System.Diagnostics.Debug.Write($"{item} ");
			}


			List<string> list = new List<string>();
			for (int i = 0; i < 50; i++) {
				list.Add(rand.Next(0, 100).ToString());
			}
			list.Sort(string.CompareOrdinal);

			for (int i = 0; i < 50; i++) {
				System.Diagnostics.Debug.Write($"{i,2} ");
			}
			System.Diagnostics.Debug.WriteLine("");
			for (int i = 0; i < 50; i++) {
				System.Diagnostics.Debug.Write($"{list[i],2} ");
			}
			System.Diagnostics.Debug.WriteLine("");
			string key = "51";
			System.Diagnostics.Debug.WriteLine($"Array.BinarySearch({key}) = {Array.BinarySearch(list.ToArray(), key)}");
			System.Diagnostics.Debug.WriteLine($"SearchRoutines.BinarySearch({key}) = {SearchRoutines.BinarySearch(list.ToArray(), key)}");
			System.Diagnostics.Debug.WriteLine($"SearchRoutines.Lower_bound({key}) = {SearchRoutines.Lower_bound(list.ToArray(), key)}");
			System.Diagnostics.Debug.WriteLine($"SearchRoutines.Upper_bound({key}) = {SearchRoutines.Upper_bound(list.ToArray(), key)}");
			System.Diagnostics.Debug.WriteLine("");
			key = "3";
			System.Diagnostics.Debug.WriteLine($"Array.BinarySearch({key}) = {Array.BinarySearch(list.ToArray(), key)}");
			System.Diagnostics.Debug.WriteLine($"SearchRoutines.BinarySearch({key}) = {SearchRoutines.BinarySearch(list.ToArray(), key)}");
			System.Diagnostics.Debug.WriteLine($"SearchRoutines.Lower_bound({key}) = {SearchRoutines.Lower_bound(list.ToArray(), key)}");
			System.Diagnostics.Debug.WriteLine($"SearchRoutines.Upper_bound({key}) = {SearchRoutines.Upper_bound(list.ToArray(), key)}");


			long ans = 0;

			Console.WriteLine(ans);
		}

	}
}
