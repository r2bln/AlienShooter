using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlienShooter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lv1 = new Level(Width, Height);
           
        }
        
        Level lv1;
        Unit tank1 = new Unit(250, 250);
        
        
        private int mouseX;
        private int mouseY;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            tank1.mouseX = mouseX;
            tank1.mouseY = mouseY;
            Graphics canvas = e.Graphics;
            lv1.Draw(canvas);
            tank1.facing = trackBar2.Value;
            tank1.draw(canvas, label1,label2,trackBar1.Value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            tank1.processKeys(0, e.KeyData);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            tank1.processKeys(1, e.KeyData);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }
    }
}
