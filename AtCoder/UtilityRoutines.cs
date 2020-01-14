using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestLibrary
{
	class UtilityRoutines
	{

		#region ビットカウント int PopCount(int x)
		static int PopCount(int x)
		{
			x = (x & 0x55555555) + (x >> 1 & 0x55555555);
			x = (x & 0x33333333) + (x >> 2 & 0x33333333);
			x = (x & 0x0f0f0f0f) + (x >> 4 & 0x0f0f0f0f);
			x = (x & 0x00ff00ff) + (x >> 8 & 0x00ff00ff);
			x = (x & 0x0000ffff) + (x >> 16 & 0x0000ffff);
			return x;
		}
		#endregion

		#region ビットカウント int PopCount(long x)
		static int PopCount(long x)
		{
			x = (x & 0x5555555555555555) + (x >> 1 & 0x5555555555555555);
			x = (x & 0x3333333333333333) + (x >> 2 & 0x3333333333333333);
			x = (x & 0x0f0f0f0f0f0f0f0f) + (x >> 4 & 0x0f0f0f0f0f0f0f0f);
			x = (x & 0x00ff00ff00ff00ff) + (x >> 8 & 0x00ff00ff00ff00ff);
			x = (x & 0x0000ffff0000ffff) + (x >> 16 & 0x0000ffff0000ffff);
			x = (x & 0x00000000ffffffff) + (x >> 32 & 0x00000000ffffffff);
			return (int)x;
		}
		#endregion

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
