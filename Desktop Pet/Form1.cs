using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Pet
{
    public partial class Form1 : Form
    {
        private FrameDimension dimension;
        private int frameCount;
        private int indexToPaint;
        private Timer timer = new Timer();

        private int formLeft;
        private int formTop;
        private int clickX;
        private int clickY;

        public Form1()
        {
            InitializeComponent();

            dimension = new FrameDimension(this.pictureBox1.Image.FrameDimensionsList[0]);
            frameCount = this.pictureBox1.Image.GetFrameCount(dimension);
            //this.pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            timer.Interval = 50;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            indexToPaint++;
            if (indexToPaint >= frameCount)
                indexToPaint = 0;
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            this.pictureBox1.Image.SelectActiveFrame(dimension, indexToPaint);
            e.Graphics.DrawImage(this.pictureBox1.Image, Point.Empty);
        }

        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            formLeft = this.Left;
            formTop = this.Top;
            clickX = e.X;
            clickY = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Capture)
            {
                this.Top = e.Y + formTop - clickY;
                this.Left = e.X + formLeft - clickX;
                formLeft = this.Left;
                formTop = this.Top;
            }
        }
    }
}
