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

        Player plr1 = new Player(250,250,2);
        
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
            
            foreach (Unit unit in enemies)
            {
                //unit.GetFacing(plr1);
                unit.mouseX = mouseX;
                unit.mouseY = mouseY;
                unit.Draw(canvas);

                collisionDetector(plr1, unit);
                
                if (unit.dead && unit.count == unit.deathChain.Count - 1)
                {
                    enemies.Remove(unit);
                    // Это костыль.
                    break;                    
                }      
            }
            plr1.Draw(canvas);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            plr1.ProcessKeys(true, e.KeyData);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            plr1.ProcessKeys(false, e.KeyData);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            plr1.GetTurretFacing(e);
            label1.Text = plr1.mouseAngle.ToString();

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Unit unit in enemies)
            {
                selectionCheck(e, unit);
            }
        }

        private void selectionCheck(MouseEventArgs e, Unit unit)
        {
            int mouseX = e.X;
            int mouseY = e.Y;
            const int unitCenterOffset = 32;
            int innerRadius = unitCenterOffset - unitCenterOffset / 5;

            if (Math.Pow(mouseX - unit.posX - unitCenterOffset, 2) + Math.Pow(mouseY - unit.posY - unitCenterOffset, 2) < innerRadius * innerRadius)
            {
                unit.dead = true;
                unit.count = 0;
            }
        }

        private void collisionDetector(Player plr1, Unit unit)
        {
            int x = plr1.posX + 32;
            int y = plr1.posY + 32;
            const int unitCenterOffset = 32;
            int innerRadius = unitCenterOffset - unitCenterOffset / 5;

            if (Math.Pow(x - unit.posX - unitCenterOffset, 2) + Math.Pow(y - unit.posY - unitCenterOffset, 2) < innerRadius * innerRadius)
            {
                unit.dead = true;
                unit.count = 0;
            }
        }

        
    }
}
