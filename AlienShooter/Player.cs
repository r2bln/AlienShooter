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
        public int speed = 3;
        public int mouseX;
        public int mouseY;

        public int count;
        public bool dead = false;

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

        public void DrawTurret(Graphics canvas, int facing)
        {
            if (facing > 10)
            {
                facing -= 10;
                var tmpFrame = new Bitmap(turretAnim[facing]);
                tmpFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
                canvas.DrawImage(tmpFrame,posX,posY);
                facing += 10;
            }
            else
            {
                canvas.DrawImage(turretAnim[facing], posX, posY);
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
            DrawTurret(canvas, facing);
            count++;
            if (count > frameCount) count = 0;
        }
    }
}
