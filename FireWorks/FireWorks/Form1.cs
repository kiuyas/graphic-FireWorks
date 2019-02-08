using System;
using System.Drawing;
using System.Windows.Forms;

namespace FireWorks
{
    public partial class Form1 : Form
    {
        private int count = 0;

        private Spark[] _Sparks = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Init()
        {
            _Sparks = new Spark[8];
            for (int i = 0; i < 8; i++)
            {
                _Sparks[i] = new Spark(new Point(320, 400));
                _Sparks[i].V.Y = -8;
            }
            count = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnFire.Enabled = false;
            Init();
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_Sparks == null)
            {
                return;
            }
            foreach (Spark s in _Sparks)
            {
                e.Graphics.FillEllipse(Brushes.White, s.P.X, s.P.Y, 8, 8);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count < 220)
            {
                for (int i = 0; i < 8; i++)
                {
                    _Sparks[i].Move();
                }

                if (count == 100)
                {
                    Explosion();
                }
            }
            else if (count == 220)
            {
                timer1.Stop();
                btnFire.Enabled = true;
            }

            count++;

            pictureBox1.Refresh();
        }

        private void Explosion()
        {
            Console.WriteLine("Explosion!");
            float v = 2;
            for (int i = 0; i < 8; i++)
            {
                double radian = i * 45 * Math.PI / 180F;
                _Sparks[i].V = new Point((float)Math.Cos(radian) * v, (float)Math.Sin(radian) * v);
            }
        }
    }

    class Spark
    {
        public Point P { get; set; }

        public Point V { get; set; }

        public Spark(Point p)
        {
            P = p;
            V = new Point(0, 0);
        }

        public void Move()
        {
            P.X += V.X;
            P.Y += V.Y;
            V.Y += 0.1F;
        }
    }

    class Point
    {
        public float X { get; set; }

        public float Y { get; set; }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

}
