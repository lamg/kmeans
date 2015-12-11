using System;
using KMeans;

namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var ps = new Point[] {new Point(-1,2), new Point(2,-4),
				new Point(-2,-5), new Point(2,7)
			};
//			var ps = new Point[] {new Point(1,1), new Point(2, 1), new Point(4, 3),
//				new Point(5,4)
//			};

			Point[] cs;
			var asg = Clustering.KMeans (ps, 2, out cs);
			for (int i = 0; i < cs.Length; i++) {
				Console.WriteLine ("Centroid {0} at X: {1} Y: {2}", i, cs[i].X, cs[i].Y);
				for (int j = 0; j < ps.Length; j++) {
					if (asg[j] == i) {
						Console.WriteLine ("X: {0}, Y: {1}", ps [j].X, ps [j].Y);
					}
				}
				Console.WriteLine ();
			}

		}
	}
}
