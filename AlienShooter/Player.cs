using System.Windows.Forms;

namespace AlienShooter
{
    internal class Player : Unit
    {
        public Player(int _posX, int _posY, int _frameCount)
        {
            posX = _posX;
            posY = _posY;
            frameCount = _frameCount;
            frameSource = new UnitFrames(Resource1.tank_full);
            frameSource.DumpAllFrames();
            animChain = frameSource.GetAnim(facing);
        }

        public void ProcessKeys(int eventType, Keys key) // 0 - keyDown 1 - keyUp
        {
            if (eventType == 0)
            {
                speed = 3;
                switch (key)
                {
                    case Keys.W:
                        KeyUp = true;
                        break;
                    case Keys.A:
                        KeyLeft = true;
                        break;
                    case Keys.S:
                        KeyDown = true;
                        break;
                    case Keys.D:
                        KeyRight = true;
                        break;
                }

                if (key == Keys.Space)
                {
                    speed = 5;
                }

            }
            if (eventType == 1)
            {
                switch (key)
                {
                    case Keys.W:
                        KeyUp = false;
                        break;
                    case Keys.A:
                        KeyLeft = false;
                        break;
                    case Keys.S:
                        KeyDown = false;
                        break;
                    case Keys.D:
                        KeyRight = false;
                        break;
                }
                if (key == Keys.Space)
                {
                    speed = 3;
                }
                if (!KeyUp && !KeyDown && !KeyLeft && !KeyRight)
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
    }
}
