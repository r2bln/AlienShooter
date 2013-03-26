using System.Windows.Forms;

namespace AlienShooter
{
    class Player : Unit
    {
        public Player(int _posX, int _posY, int _frameCount)
        {
            posX = _posX;
            posY = _posY;
            frameCount = _frameCount;
            frameSource = new UnitFrames(Resource1.tank_full);
            animChain = frameSource.GetFrames(facing, frameCount);
        }

        public void processKeys(int eventType, Keys key) // 0 - keyDown 1 - keyUp
        {
            if (eventType == 0)
            {
                speed = 3;
                switch (key)
                {
                    case Keys.W: KeyUp = true;
                        break;
                    case Keys.A: KeyLeft = true;
                        break;
                    case Keys.S: KeyDown = true;
                        break;
                    case Keys.D: KeyRight = true;
                        break;
                }

                if (key == Keys.Space)
                {
                    speed = 5;
                }

            }
            if (eventType == 1)
            {
                speed = 0;
                switch (key)
                {
                    case Keys.W: KeyUp = false;
                        break;
                    case Keys.A: KeyLeft = false;
                        break;
                    case Keys.S: KeyDown = false;
                        break;
                    case Keys.D: KeyRight = false;
                        break;
                }
                if (key == Keys.Space)
                {
                    speed = 0;
                }
            }

            if (KeyUp && KeyRight) facing = 2;
            else if (KeyRight && KeyDown) facing = 6;
            else if (KeyLeft && KeyDown) facing = 16;
            else if (KeyUp && KeyLeft) facing = 12;
            else if (KeyUp) facing = 0;
            else if (KeyRight) facing = 4;
            else if (KeyLeft) facing = 14;
            else if (KeyDown) facing = 8;

            animChain = frameSource.GetFrames(facing, frameCount);
        }
    }
}
