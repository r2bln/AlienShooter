using System.Collections.Generic;
using System.Drawing;

namespace AlienShooter
{
    public class UnitFrames
    {
        Point loc;
        Rectangle rect;
        Bitmap sprites;
        List<Bitmap> animChain = new List<Bitmap>();
        private Bitmap tmpFrame; 
        private int step;
        public int facing = 14;
        private int prev = 0;
        public int frameCount = 0;
        private bool flipped;

        public UnitFrames(Bitmap spriteSrc)
        {
            sprites = new Bitmap(spriteSrc);
            rect.Width = 64;
            rect.Height = 64;
            loc.X = 32;
            loc.Y = 32;
            rect.Location = loc;
            step = rect.Width * 2 * 9;
        }
        
        public List<Bitmap> DumpAllFrames()
        {
            loc.X = 32;
            while (loc.Y < sprites.Height) 
            {
                tmpFrame = sprites.Clone(rect, sprites.PixelFormat);
                animChain.Add(tmpFrame);
                loc.X += rect.Width * 2;
                if (loc.X > sprites.Width)
                {
                    loc.X = 32;
                    loc.Y += rect.Height * 2;
                }
                rect.Location = loc;
            }
            return animChain;
        }

        public List<Bitmap> GetFrames(int animId, int _frameCount)
        {
            if (animId > 10)
            {
                animId -= 10;
                flipped = true;
            }
            
            loc.X = 32 + 64 * 2 * animId;
            rect.Location = loc;
            animChain.Clear();
            while (loc.Y < sprites.Height - rect.Width * 2) // Достает все анимации while (frameCount <= _frameCount + 1) //
            {
                tmpFrame = sprites.Clone(rect, sprites.PixelFormat);
                animChain.Add(tmpFrame);
                prev = loc.X;
                loc.X += step;
                frameCount++;

                if (loc.X > sprites.Width)
                {
                    loc.X = loc.X - (step + rect.Width*21 + 32*2);
                    prev = 0;
                    loc.Y += rect.Height*2;
                }
                if (loc.X > 0)
                {
                    rect.Location = loc;
                    frameCount++;
                }
            }
            frameCount = 0;
            loc.Y = 32;

            if (flipped)
            {
                foreach (Bitmap bitmap in animChain)
                {
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                flipped = false;
            }
            return animChain;
        }

        /* Test purposes only! */

        public void draw(Graphics canvas)
        {
            Point loc1 = new Point(0,100);
            foreach (Bitmap frame in animChain)
            {
                canvas.DrawImage(frame, loc1);
                loc1.X += frame.Width;
                if (loc1.X > 500)
                {
                    loc1.X = 0;
                    loc1.Y += frame.Height;
                }
            }
        }
    }
}
