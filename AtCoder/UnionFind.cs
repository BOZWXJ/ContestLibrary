using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestLibrary
{

	#region Union-Find木 UnionFind
	public class UnionFind
	{
		private readonly UnionFindNode[] _Parent;

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

		public void Unite(int x, int y)
		{
			int rx = Find(x);
			int ry = Find(y);
			if (rx != ry) {
				_Parent[ry].Parent = rx;
				_Parent[rx].Size += _Parent[ry].Size;
			}
		}

		public bool Same(int x, int y)
		{
			int rx = Find(x);
			int ry = Find(y);
			return rx == ry;
		}

		public override string ToString()
		{
			return string.Join(" ", _Parent.Select(p => $"({p.Parent},{p.Size})"));
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
