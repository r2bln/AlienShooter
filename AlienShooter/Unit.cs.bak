using System.Drawing;
using System.Windows.Forms;

namespace AlienShooter
{
    class Unit
    {
        public int posX;
        public int posY;
        public int frameCount = 0;
        public int facing = 14; // 0 - N, 1 - NE, 3 - E, and so on
        public int facing1;
        public int speed = 0;
        public int mouseX;
        public int mouseY;

        private int xFrame = 0;
        private int yFrame = 0;
        private int step = 0;
        private int prev = 0;

        int offset = 128;
        Point loc;
        Point loc1; 
        Size sz;
        Rectangle rect;
        Bitmap sprites;
        private bool flipped;

        bool up;
        bool left;
        bool down;
        bool right;

        public Unit(int _posX, int _posY)
        {
            posX = _posX;
            posY = _posY;
            loc = new Point(32 + 64 * 4,32);
            loc1 = new Point(800, 416);
            sz  = new Size(64,64);
            rect = new Rectangle(loc, sz);
            sprites = new Bitmap(Resource1.zergl_full);
            step = rect.Width*2*9;
        }

        public void processKeys(int eventType, Keys key) // 0 - keyDown 1 - keyUp
        {
            if (eventType == 0)
            {
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
                    speed = 1;
                }
            }
            
            if (up && right) facing = 2;
            else if (right && down) facing = 6;
            else if (left && down) facing = 12;
            else if (up && left) facing = 16;
            else if (up) facing = 0;
            else if (right) facing = 4;
            else if (left) facing = 14;
            else if (down) facing = 8;
        }

        public void draw(Graphics canvas, Label lb1, Label lb2, int frc)
        {
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

                // Ебаный костыль при отражении полотна со спрайтами: 

                case 12:
                    posX -= speed;
                    posY += speed;
                    flipped = true;
                    //sprites.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    facing = facing - 10;
                    break;
                case 16:
                    posX -= speed;
                    posY -= speed;
                    flipped = true;
                    //sprites.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    facing = facing - 10;
                    break;
                case 14:
                    posX -= speed;
                    flipped = true;
                    //sprites.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    facing = facing - 10;
                    break;
            }

            
            canvas.DrawImage(sprites, posX, posY, rect, GraphicsUnit.Pixel);
            
            prev = loc.X;
            loc.X += step;
            xFrame++;
            frameCount++;
            
            if (loc.X > sprites.Width)
            {
                loc.X = loc.X - (step + rect.Width * 21 + 32*2);
                prev = 0;
                loc.Y += rect.Height*2;
                yFrame++;
                xFrame = 0;
            }
            if (frameCount > frc)//loc.Y >sprites.Height - rect.Width * 2)
            {
                xFrame = 0;
                yFrame = 0;
                loc.X = 32 + 64 * 2 * facing;
                loc.Y = 32;
                frameCount = 0;
            }
            if (loc.X > 0)
            {
                rect.Location = loc;
            }
            
            lb1.Text = xFrame.ToString();
            lb2.Text = yFrame.ToString();

            /*
            loc.X = 32 + offset * facing;
            loc1.X = 32 + offset * facing1;
            if (loc.Y >= 288) loc.Y = 32;
            loc.Y += 128;
            canvas.DrawImage(sprites, posX, posY, rect, GraphicsUnit.Pixel);
            //canvas.DrawRectangle(new Pen(Color.Red, 1), posX, posY, rect.Width, rect.Height);
            canvas.DrawLine(new Pen(Color.Red, 1),posX + rect.Width/2,posY+rect.Height/2,mouseX,mouseY);
            rect.Location = loc1;
            canvas.DrawImage(sprites, posX, posY, rect, GraphicsUnit.Pixel);
            rect.Location = loc;
            
            // Ебаный костыль:

            if (flipped)
            {
                sprites.RotateFlip(RotateFlipType.RotateNoneFlipX);
                facing = facing + 10;
                flipped = false;
            }
            */
        }
    }
}
