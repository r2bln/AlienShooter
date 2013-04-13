using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlienShooter
{
    internal class Player
    {
        
        public int posX;
        public int posY;
        public int frameCount = 10;
        public int facing = 5;
        public int turretFacing = 0;
        public int speed = 3;
        public int mouseX;
        public int mouseY;

        public double mouseAngle = 0;

        private int centerOffset = 32;

        public int count;

        public bool KeyUp;
        public bool KeyLeft;
        public bool KeyDown;
        public bool KeyRight;

        public List<Bitmap> animChain = new List<Bitmap>();
        public UnitFrames frameSource = new UnitFrames(Resource1.zergl_full);
        private List<Bitmap> turretAnim;

        public const int UP = 0;
        public const int UP_RIGHT = 2;
        public const int RIGHT = 4;
        public const int DOWN_RIGHT = 6;
        public const int DOWN = 8;
        public const int DOWN_LEFT = 16;
        public const int LEFT = 14;
        public const int UP_LEFT = 12;

        public Player(int _posX, int _posY, int _frameCount)
        {
            posX = _posX;
            posY = _posY;
            frameCount = _frameCount;
            frameSource = new UnitFrames(Resource1.tank_full);
            frameSource.DumpAllFrames();
            animChain = frameSource.GetAnim(facing);
            turretAnim = frameSource.GetAnim(27, 36);
        }

        public void ProcessKeys(bool eventType, Keys key) // true - keyDown, false - keyUp
        {
            speed = 3;
            switch (key)
            {
                case Keys.W:
                    KeyUp = eventType;
                    break;
                case Keys.A:
                    KeyLeft = eventType;
                    break;
                case Keys.S:
                    KeyDown = eventType;
                    break;
                case Keys.D:
                    KeyRight = eventType;
                    break;
            }

            if (key == Keys.Space)
            {
                speed = 5;
            }

            if (!KeyUp && !KeyDown && !KeyLeft && !KeyRight)
            {
                speed = 0;
            }

            if (KeyUp && KeyRight) facing = UP_RIGHT;
            else if (KeyRight && KeyDown) facing = DOWN_RIGHT;
            else if (KeyLeft && KeyDown) facing = DOWN_LEFT;
            else if (KeyUp && KeyLeft) facing = UP_LEFT;
            else if (KeyUp) facing = UP;
            else if (KeyRight) facing = RIGHT;
            else if (KeyLeft) facing = LEFT;
            else if (KeyDown) facing = DOWN;

            animChain = frameSource.GetAnim(facing);
        }

        public void DrawTurret(Graphics canvas, int _facing)
        {
            if (_facing > 10)
            {
                _facing -= 10;
                var tmpFrame = new Bitmap(turretAnim[_facing]);
                tmpFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
                canvas.DrawImage(tmpFrame,posX,posY);
                _facing += 10;
            }
            else
            {
                canvas.DrawImage(turretAnim[_facing], posX, posY);
            }
        }

        /// <summary>
        /// Movement and draw routines
        /// </summary>
        /// <param name="canvas">>GDI+ Graphics Object</param>
        public void Draw(Graphics canvas)
        {
            switch (facing)
            {
                case UP:
                    posY -= speed;
                    break;
                case RIGHT:
                    posX += speed;
                    break;
                case DOWN:
                    posY += speed;
                    break;
                case UP_RIGHT:
                    posX += speed;
                    posY -= speed;
                    break;
                case DOWN_RIGHT:
                    posX += speed;
                    posY += speed;
                    break;
                case DOWN_LEFT:
                    posX -= speed;
                    posY += speed;
                    break;
                case UP_LEFT:
                    posX -= speed;
                    posY -= speed;
                    break;
                case LEFT:
                    posX -= speed;
                    break;
            }

            if (posX > 640 - 64) posX = 640 - 64;
            if (posY > 512 - 64) posY = 512 - 64;
            if (posX < 0) posX = 0;
            if (posY < 0) posY = 0;

            canvas.DrawRectangle(new Pen(Color.Red), posX, posY, 64, 64);
            canvas.DrawImage(animChain[count], posX, posY);
            DrawTurret(canvas, turretFacing);
            count++;
            if (count > frameCount) count = 0;
        }

        public void GetTurretFacing(MouseEventArgs e)
        {
            double dX = e.X - (posX + centerOffset);
            double dY = e.Y - (posY + centerOffset);

            if (dY < 0 && dX > 0)
                mouseAngle = Math.Asin(dX/Math.Sqrt(dX*dX + dY*dY)) / Math.PI * 180;
            else if (dY < 0 && dX < 0)
                mouseAngle = Math.Asin(dX / Math.Sqrt(dX * dX + dY * dY)) / Math.PI * 180 + 360;
            else if (dY > 0)
                mouseAngle = Math.Acos(dX / Math.Sqrt(dX * dX + dY * dY)) / Math.PI * 180 + 90;

            if (mouseAngle > 359 && mouseAngle < 1)
                turretFacing = 0;
            if (mouseAngle > 1 && mouseAngle < 44)
                turretFacing = 1;
            if (mouseAngle > 44 && mouseAngle < 46)
                turretFacing = 2;
            if (mouseAngle > 46 && mouseAngle < 89)
                turretFacing = 3;
            if (mouseAngle > 89 && mouseAngle < 91)
                turretFacing = 4;
            if (mouseAngle > 91 && mouseAngle < 134)
                turretFacing = 5;
            if (mouseAngle > 134 && mouseAngle < 136)
                turretFacing = 6;
            if (mouseAngle > 136 && mouseAngle < 179)
                turretFacing = 7;
            if (mouseAngle > 179 && mouseAngle < 181)
                turretFacing = 8;
            if (mouseAngle > 181 && mouseAngle < 224)
                turretFacing = 17;
            if (mouseAngle > 224 && mouseAngle < 226)
                turretFacing = 16;
            if (mouseAngle > 226 && mouseAngle < 269)
                turretFacing = 15;
            if (mouseAngle > 269 && mouseAngle < 271)
                turretFacing = 14;
            if (mouseAngle > 271 && mouseAngle < 314)
                turretFacing = 13;
            if (mouseAngle > 314 && mouseAngle < 316)
                turretFacing = 12;
            if (mouseAngle > 316 && mouseAngle < 359)
                turretFacing = 11;
        }
    }
}
