using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtCoder
{
	class MathLib
	{
		public static bool IsPrime(long x)
		{
			if (x == 2) {
				return true;
			} else if (x < 2 || x % 2 == 0) {
				return false;
			}
			double sqrt = Math.Sqrt(x);
			for (int i = 3; i <= sqrt; i += 2) {
				if (x % i == 0) {
					return false;
				}
			}
			return true;
		}
	}
}
