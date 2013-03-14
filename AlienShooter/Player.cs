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
                    case Keys.W: up = true;
                        break;
                    case Keys.A: left = true;
                        break;
                    case Keys.S: down = true;
                        break;
                    case Keys.D: right = true;
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
                    case Keys.W: up = false;
                        break;
                    case Keys.A: left = false;
                        break;
                    case Keys.S: down = false;
                        break;
                    case Keys.D: right = false;
                        break;
                }
                if (key == Keys.Space)
                {
                    speed = 0;
                }
            }

            if (up && right) facing = 2;
            else if (right && down) facing = 6;
            else if (left && down) facing = 16;
            else if (up && left) facing = 12;
            else if (up) facing = 0;
            else if (right) facing = 4;
            else if (left) facing = 14;
            else if (down) facing = 8;

            animChain = frameSource.GetFrames(facing, frameCount);
        }
    }
}
