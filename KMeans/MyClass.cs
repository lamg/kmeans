using System;

namespace KMeans
{
	public class Clustering
	{
		public static int[] KMeans(Point[] ps, int k, out Point[] cs){
			cs = GenerateK(ps, k);
			bool b = true;
			int[] asg = null;
			while (b) {
				asg = AssignCenters (ps, cs);
				var ncs = UpdateCenters (ps, asg, k);
				b = false;
				for (int i = 0; i < k; i++) {
					b = b || !(cs [i].X == ncs [i].X && cs [i].Y == ncs [i].Y);
				}
				cs = ncs;
			}
			return asg;
		}

		static double Distance(Point p0, Point p1) {
			return Math.Sqrt(Math.Pow(p0.X- p1.X,2)+Math.Pow(p0.Y-p1.Y,2));
		}

		static int[] AssignCenters(Point[] ps, Point[] cs){
			var r = new int[ps.Length];
			int min_ind = 0;
			for (int i = 0; i < ps.Length; i++) {
				double min = double.MaxValue;
				for (int j = 0; j < cs.Length; j++) {
					double v = Distance (ps [i], cs [j]);
					if (v < min) {
						min = v;
						min_ind = j;
					}
				}
				r [i] = min_ind;
			}
			// r[i] es el indice del centroide de cs correspondiente a ps[i]
			return r;
		}


		static Point[] UpdateCenters(Point[] ps, int[] a, int k){
			var r = new Point[k];
			var am = new int[k]; //am[i] es la cantidad de puntos asociados al centroide i
			for (int i = 0; i < k; i++) {
				r [i] = new Point (0, 0);
				for (int j = 0; j < ps.Length; j++) {
					if (a[j] == i) {
						r [i].X += ps [j].X;
						r [i].Y += ps [j].Y;
						am [i]++;
					}
				}
			}
			for (int i = 0; i < k; i++) {
				r [i].X = r [i].X / (double)am [i];
				r [i].Y = r [i].Y / (double)am [i];
			}
			// r[i] es el centroide actualizado para cada ps[j] que lo tiene como centroide
			// lo tiene como centroide si a[j] = i
			return r;
		}

		static Point[] GenerateK(Point[] ps, int k){
			var r = new Point[k];
			for (int i = 0; i < k; i++) {
				r [i] = ps [i];
			}
			return r;
		}

		public static Point[] Generate(int n, int xmax, int ymax){
			var r = new Point[n];
			var rn = new Random ();
			for (int i = 0; i < n; i++) {
				r [i] = new Point ((double)rn.Next (xmax), (double)rn.Next (ymax));
			}
			return r;
		}
	}

	public class Point
	{
		public Point (double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}

		public double X, Y;
	}
}

