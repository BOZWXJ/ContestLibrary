using System;
using System.Collections.Generic;
using System.Linq;

namespace ContestLibrary
{
	// Union-Find 木
	//   グループ分けを木構造で管理する

	#region Union-Find 木 UnionFind
	public class UnionFind
	{
		private readonly UnionFindNode[] _Parent;

		/// <summary>
		/// Union-Find 木
		/// </summary>
		/// <param name="n">サイズ</param>
		public UnionFind(int n)
		{
			_Parent = new UnionFindNode[n];
			for (int i = 0; i < n; i++) {
				_Parent[i] = new UnionFindNode(i, 1);
			}
		}

		public int Find(int x)
		{
			if (_Parent[x].Parent == x) {
				return _Parent[x].Parent;
			}
			return _Parent[x].Parent = Find(_Parent[x].Parent);
		}

		public int Size(int x)
		{
			return _Parent[Find(x)].Size;
		}

		/// <summary>
		/// グループの個数
		/// </summary>
		/// <returns></returns>
		public int Count()
		{
			int result = 0;
			foreach ((var item, var index) in _Parent.Select((p, i) => (p, i))) {
				if (item.Parent == index) {
					result++;
				}
			}
			return result;
		}

		/// <summary>
		/// x と y のグループを併合
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void Unite(int x, int y)
		{
			int rx = Find(x);
			int ry = Find(y);
			if (rx != ry) {
				_Parent[ry].Parent = rx;
				_Parent[rx].Size += _Parent[ry].Size;
			}
		}

		/// <summary>
		/// x と y が同じグループに属しているか判定
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool Same(int x, int y)
		{
			int rx = Find(x);
			int ry = Find(y);
			return rx == ry;
		}

		public override string ToString()
		{
			return string.Join(" ", _Parent.Select((p, i) => $"{i}({p.Parent}, {p.Size})"));
		}
	}
	public class UnionFindNode
	{
		public int Parent;
		public int Size;
		public UnionFindNode(int parent, int size)
		{
			Parent = parent;
			Size = size;
		}
	}
	#endregion

}
