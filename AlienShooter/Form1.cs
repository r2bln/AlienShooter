using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlienShooter
{
    public partial class Form1 : Form
    {
        Level lv1;

        private List<Unit> enemies = new List<Unit>(25); 
       
        private int mouseX;
        private int mouseY;

        Random rnd1 = new Random();

        Player plr1 = new Player(250,250,1);
        
        public Form1()
        {
            InitializeComponent();
            lv1 = new Level(Width, Height);
            
            for (int i = 0; i < 25; i++)
            {
                enemies.Add(new Unit(rnd1.Next(600), rnd1.Next(500), rnd1.Next(16)));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Graphics canvas = e.Graphics;
            lv1.Draw(canvas);
            plr1.draw(canvas);
            foreach (Unit unit in enemies)
            {
                unit.GetFacing(plr1);
                unit.mouseX = mouseX;
                unit.mouseY = mouseY;
                unit.draw(canvas);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            plr1.processKeys(0, e.KeyData);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            plr1.processKeys(1, e.KeyData);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }
    }
}
