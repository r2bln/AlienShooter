using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AlienShooter
{
    class Unit
    {
        public int posX;
        public int posY;
        public int frameCount = 16;
        public int facing = 5; // 0 - N, 1 - NE, 3 - E, and so on
        public int speed = 3;
        public int mouseX;
        public int mouseY;

        int count;

        public bool up;
        public bool left;
        public bool down;
        public bool right;

        public List<Bitmap> animChain = new List<Bitmap>();
        public UnitFrames frameSource;

        public Unit(int _posX, int _posY)
        {
            posX = _posX;
            posY = _posY;
            frameSource = new UnitFrames(Resource1.zergl_full);
            animChain = frameSource.GetFrames(facing, frameCount);
        }

        public Unit(int _posX, int _posY, int _facing)
        {
            posX = _posX;
            posY = _posY;
            frameSource = new UnitFrames(Resource1.zergl_full);
            animChain = frameSource.GetFrames(_facing, frameCount);
            facing = _facing;
        }
        
        public Unit() 
        {
            // Это использовать нельзя иначе пиздец
        }

        public void GetFacing(Player plr)
        {
            int oldFacing = facing;

            if (plr.posX > posX && plr.posY > posY)
            {
                facing = 6;
            }

            else if (plr.posX < posX && plr.posY < posY)
            {
                facing = 16;
            }

            else if (plr.posX > posX && plr.posY < posY)
            {
                facing = 2;
            }

            else if (plr.posX < posX && plr.posY > posY)
            {
                facing = 12;
            }

            else if (plr.posX > posX)
            {
                facing = 4;
            }

            else if (plr.posX < posX)
            {
                facing = 14;
            }

            else if (plr.posY > posY)
            {
                facing = 8;
            }

            else if (plr.posY < posY)
            {
                facing = 0;
            }

            if (facing != oldFacing)
            {
                animChain = frameSource.GetFrames(facing, frameCount);
            }
        }

        public void draw(Graphics canvas)
        {
            //----------------------------------------------------------
            //------------------MOVEMENT-------------------------------

            switch (facing)
            {
                case 0:
                    posY -= speed;
                    break;
                case 4:
                    posX += speed;
                    break;
                case 8:
                    posY += speed;
                    break;
                case 2:
                    posX += speed;
                    posY -= speed;
                    break;
                case 6:
                    posX += speed;
                    posY += speed;
                    break;
                case 16:
                    posX -= speed;
                    posY += speed;
                    break;
                case 12:
                    posX -= speed;
                    posY -= speed;
                    break;
                case 14:
                    posX -= speed;
                    break;
            }

            if (posX > 600) posX = 640;
            if (posY > 500) posY = 512;
            if (posX < 0) posX = 0;
            if (posY < 0) posY = 0;
            //--------------------------------------------------------------

            //GetFacing();

            canvas.DrawImage(animChain[count], posX, posY);
            count++;
            if (count > frameCount) count = 0;
        }
    }
}
