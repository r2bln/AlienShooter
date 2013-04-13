namespace AlienShooter
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Player
    {
        private const int Up = 0;
        private const int UpRight = 2;
        private const int Right = 4;
        private const int DownRight = 6;
        private const int Down = 8;
        private const int DownLeft = 16;
        private const int Left = 14;
        private const int UpLeft = 12;
        private const int CenterOffset = 32;

        private readonly int frameCount = 10;
        private readonly UnitFrames frameSource = new UnitFrames(Resource1.zergl_full);
        private readonly List<Bitmap> turretAnim;

        private bool keyUp;
        private bool keyLeft;
        private bool keyDown;
        private bool keyRight;

        private int facing = 5;
        private int turretFacing;
        private int speed = 3;
        private int count;

        private List<Bitmap> animChain = new List<Bitmap>();

        public Player(int posX, int posY, int frameCount)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.frameCount = frameCount;
            this.frameSource = new UnitFrames(Resource1.tank_full);
            this.frameSource.DumpAllFrames();
            this.animChain = this.frameSource.GetAnim(facing);
            turretAnim = this.frameSource.GetAnim(27, 36);
        }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public double MouseAngle { get; set; }

        public void ProcessKeys(bool eventType, Keys key) // true - keyDown, false - keyUp
        {
            speed = 3;
            switch (key)
            {
                case Keys.W:
                    this.keyUp = eventType;
                    break;
                case Keys.A:
                    this.keyLeft = eventType;
                    break;
                case Keys.S:
                    this.keyDown = eventType;
                    break;
                case Keys.D:
                    this.keyRight = eventType;
                    break;
            }

            if (key == Keys.Space)
            {
                speed = 5;
            }

            if (!this.keyUp && !this.keyDown && !this.keyLeft && !this.keyRight)
            {
                speed = 0;
            }

            if (this.keyUp && this.keyRight) facing = UpRight;
            else if (this.keyRight && this.keyDown) facing = DownRight;
            else if (this.keyLeft && this.keyDown) facing = DownLeft;
            else if (this.keyUp && this.keyLeft) facing = UpLeft;
            else if (this.keyUp) facing = Up;
            else if (this.keyRight) facing = Right;
            else if (this.keyLeft) facing = Left;
            else if (this.keyDown) facing = Down;

            this.animChain = this.frameSource.GetAnim(facing);
        }

        public void DrawTurret(Graphics canvas)
        {
            if (turretFacing > 8)
            {
                var facing = Math.Abs(turretFacing - 16);
                var tmpFrame = new Bitmap(turretAnim[facing]);
                tmpFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
                canvas.DrawImage(tmpFrame, this.PosX, this.PosY);
            }
            else
            {
                canvas.DrawImage(turretAnim[turretFacing], this.PosX, this.PosY);
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
                case Up:
                    this.PosY -= speed;
                    break;
                case Right:
                    this.PosX += speed;
                    break;
                case Down:
                    this.PosY += speed;
                    break;
                case UpRight:
                    this.PosX += speed;
                    this.PosY -= speed;
                    break;
                case DownRight:
                    this.PosX += speed;
                    this.PosY += speed;
                    break;
                case DownLeft:
                    this.PosX -= speed;
                    this.PosY += speed;
                    break;
                case UpLeft:
                    this.PosX -= speed;
                    this.PosY -= speed;
                    break;
                case Left:
                    this.PosX -= speed;
                    break;
            }

            if (this.PosX > 640 - 64) this.PosX = 640 - 64;
            if (this.PosY > 512 - 64) this.PosY = 512 - 64;
            if (this.PosX < 0) this.PosX = 0;
            if (this.PosY < 0) this.PosY = 0;

            canvas.DrawRectangle(new Pen(Color.Red), this.PosX, this.PosY, 64, 64);
            canvas.DrawImage(this.animChain[this.count], this.PosX, this.PosY);
            DrawTurret(canvas);
            this.count++;
            if (this.count > frameCount)
            {
                this.count = 0;
            }
        }

        public void GetTurretFacing(MouseEventArgs e)
        {
            double dX = e.X - (this.PosX + CenterOffset);
            double dY = e.Y - (this.PosY + CenterOffset);

            var mouseAngle = Math.Asin(dX / Math.Sqrt(dX * dX + dY * dY)) / Math.PI * 180;

            if (dY < 0 && dX > 0)
            {
                this.MouseAngle = mouseAngle;
            }
            else if (dY < 0 && dX < 0)
            {
                this.MouseAngle = mouseAngle + 360;
            }
            else if (dY > 0)
            {
                this.MouseAngle = -mouseAngle + 180;
            }

            turretFacing = (int)(this.MouseAngle / 22.5);
        }
    }
}
