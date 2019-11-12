using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestLibrary
{
	class UtilityRoutines
	{

		#region 0～n の mビット目の 1の個数を返す ulong Count1(ulong n, int m)
		/// <summary>
		/// 0～n の mビット目の 1の個数を返す
		/// </summary>
		/// <param name="value"></param>
		/// <param name="bit">ビット位置 0～63</param>
		/// <returns></returns>
		static ulong Count1(ulong value, int bit)
		{
			ulong x = 1UL << bit;
			ulong y = x << 1;
			ulong a = 0;
			if (y > 0) {
				a = value / y * x;
			}
			ulong z = ~(0xffffffffffffffff << bit);
			ulong b = (value & x) == 0 ? 0 : (value & z) + 1;
			return a + b;
		}
		#endregion

	}
}
