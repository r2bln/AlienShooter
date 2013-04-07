using System.Windows.Forms;

namespace AlienShooter
{
    internal class Player : Unit
    {
        public Player(int _posX, int _posY, int _frameCount)
            : base(_posX, _posY)
        {
            posX = _posX;
            posY = _posY;
            frameCount = _frameCount;
            frameSource = new UnitFrames(Resource1.tank_full);
            frameSource.DumpAllFrames();
            animChain = frameSource.GetAnim(facing);
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
    }
}
