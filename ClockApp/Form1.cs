using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockApp
{
    public partial class Form1 : Form
    {
        int clockRadius = 200, secondLength = 170, minuteLength = 130, hourLength = 90;
        public Form1()
        {
            InitializeComponent();
            Paint += Draw;
            Timer timer = new Timer { Interval = 1000 };
            timer.Tick += TickEvent;
            timer.Start();
        }

        private void TickEvent(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;
            graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graph.Clear(Color.White);

            int offset = 9;

            graph.TranslateTransform(Width / 2 - offset, Height / 2 - offset);
            float help = Math.Min(Width, Height) - 9 * offset;
            graph.ScaleTransform(
                help / (2 * clockRadius), help / (2 * clockRadius)
            );

            Point center = new Point(0, 0);

            Rectangle clockRectangle = new Rectangle(
                Point.Subtract(center, new Size(clockRadius, clockRadius)),
                new Size(clockRadius * 2, clockRadius * 2)
            );
            graph.DrawEllipse(new Pen(new SolidBrush(Color.DarkViolet), 4), clockRectangle);

            GraphicsState save = graph.Save();

            int[] numbers = new int[12];
            numbers[0] = 12;
            for (int i = 1; i < 12; i++)
            {
                numbers[i] = i;
            }
            for (int i = 0; i < 12; i++)
            {
                graph.DrawLine(
                    new Pen(new SolidBrush(Color.DarkViolet), 4),
                    new Point(0, -clockRadius + 30),
                    new Point(0, -clockRadius)
                );
                graph.DrawString(
                    numbers[i].ToString(),
                    new Font("Colibri", 16),
                    Brushes.DarkViolet,
                    Point.Add(center, new Size(-10, -clockRadius + 45))
                );
                for (int j = 0; j < 5; j++)
                {
                    graph.RotateTransform(6);
                    graph.DrawLine(
                        new Pen(new SolidBrush(Color.DarkViolet), 4),
                        new Point(0, -clockRadius + 10),
                        new Point(0, -clockRadius)
                   );
                }
            }

            graph.Restore(save);

            var timeVal = DateTime.Now;
            float secondAngle = timeVal.Second * 6;
            float minuteAngle = timeVal.Minute * 6;
            float hourAngle = timeVal.Hour * 30;

            save = graph.Save();

            graph.RotateTransform(secondAngle);
            graph.DrawLine(new Pen(new SolidBrush(Color.Violet), 4), center, Point.Add(center, new Size(0, -secondLength)));
            graph.Restore(save);
            save = graph.Save();

            graph.RotateTransform(minuteAngle);
            graph.DrawLine(new Pen(new SolidBrush(Color.LightBlue), 4), center, Point.Add(center, new Size(0, -minuteLength)));
            graph.Restore(save);

            save = graph.Save();
            graph.RotateTransform(hourAngle);
            graph.DrawLine(new Pen(new SolidBrush(Color.Coral), 4), center, Point.Add(center, new Size(0, -hourLength)));
            graph.Restore(save);

        }
    }
}
