using System;
using System.Collections.Generic;
using System.Linq;

namespace ContestLibrary
{

	#region MultiMap<TKey, TValue>
	public class MultiMap<TKey, TValue> : Dictionary<TKey, List<TValue>>
	{
		public void Add(TKey key, TValue value)
		{
			if (!ContainsKey(key)) {
				Add(key, new List<TValue>());
			}
			this[key].Add(value);
		}
		private new void Add(TKey key, List<TValue> values)
		{
			base.Add(key, values);
		}
	}
	#endregion

}

