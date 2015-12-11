using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using K = KMeans;


namespace GUI
{
	public class MWindow: Form
	{
		PictureBox pbox;
		Button ld, gen;
		NumericUpDown cAm;

		K.Point[] ps, cs;
		int[] asg;

		Brush[] bs = new Brush[] { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Black, Brushes.Brown };

		public MWindow ()
		{


			pbox = new PictureBox ();
			ld = new Button ();
			gen = new Button ();
			cAm = new NumericUpDown ();

			pbox.Size = new Size (390, 300);
			pbox.Location = new Point (10, 10);
			pbox.Paint += HandlePaint;

			ld.Size = new Size (60, 20);
			ld.Location = new Point (130, 330);
			ld.Click += HandleClick;
			ld.Text = "Agrupar";
			ld.Enabled = false;

			gen.Size = new Size (60, 20);
			gen.Location = new Point (10, 330);
			gen.Text = "Inicializar";
			gen.Click += HandleClick1;

			cAm.Size = new Size (40, 20);
			cAm.Location = new Point (80, 330);
			cAm.Maximum = bs.Length;
			cAm.Minimum = 1;

			Size = new Size (400, 400);
			StartPosition = FormStartPosition.CenterScreen;

			Controls.Add (pbox);
			Controls.Add (ld);
			Controls.Add (gen);
			Controls.Add (cAm);
			PerformLayout ();

		}

		void HandleClick1 (object sender, EventArgs e)
		{
			ps = K.Clustering.Generate (10, pbox.Size.Width, pbox.Size.Height);
			ld.Enabled = true;
			asg = K.Clustering.KMeans (ps, 1, out cs);
			pbox.Refresh ();
		}

		void HandleClick (object sender, EventArgs e)
		{
//			ps = new K.Point[] {new K.Point(1,2), new K.Point(2,1),
//				new K.Point(3,1), new K.Point(5,4), new K.Point(5, 5),
//				new K.Point(6, 5), new K.Point(10, 8), new K.Point(7, 9),
//				new K.Point(11, 5), new K.Point(14, 9), new K.Point(14, 14)
//			};
//			ps = new K.Point[] {new K.Point(1,1), new K.Point(2, 1), new K.Point(4, 3),
//						new K.Point(5,4)
//			};
			asg = K.Clustering.KMeans (ps, (int)cAm.Value, out cs);
			pbox.Refresh ();
		}

		void HandlePaint (object sender, PaintEventArgs e)
		{
			if (ps != null && cs != null) {
				for (int i = 0; i < cs.Length; i++) {
					for (int j = 0; j < ps.Length; j++) {
						if (asg[j] == i) {
							e.Graphics.FillEllipse (bs [i], (float)ps [j].X, (float)ps [j].Y, 5, 5);
						}
					}
				}
			}
		}
	}
}

