using System.Collections.Generic;
using System.Drawing;

namespace AlienShooter
{
    public class UnitFrames
    {
        Point loc;
        Rectangle rect;
        Bitmap SpriteSheet;
        private Bitmap tmp;
        List<Bitmap> animChain = new List<Bitmap>();
        List<Bitmap> retAnimChain = new List<Bitmap>();
        private Bitmap tmpFrame; 
        public int facing = 14;
        public int frameCount = 0;

        public UnitFrames(Bitmap spriteSheetSrc)
        {
            SpriteSheet = spriteSheetSrc;
            rect.Width = 64;
            rect.Height = 64;
            loc.X = 32;
            loc.Y = 32;
            rect.Location = loc;
        }
        

        /// <summary>
        /// Dumps all frames of a spritesheet into a collection of Bitmaps
        /// </summary>
        /// <returns>none</returns>

        public void DumpAllFrames()
        {
            SpriteSheet = new Bitmap(SpriteSheet);
            loc.X = 32;
            while (loc.Y < SpriteSheet.Height) 
            {
                tmpFrame = SpriteSheet.Clone(rect, SpriteSheet.PixelFormat);
                animChain.Add(tmpFrame);
                loc.X += rect.Width * 2;
                if (loc.X > SpriteSheet.Width)
                {
                    loc.X = 32;
                    loc.Y += rect.Height * 2;
                }
                rect.Location = loc;
            }
        }

        /// <summary>
        /// Returns the sequence of frames of -th animation on the spritesheet
        /// </summary>
        /// <param name="animId">Animation number</param>
        /// <returns>List<Bitmap></returns>

        public List<Bitmap> GetAnim(int facing)
        {
            int animId = facing;
            if (animId > 10)
            {
               animId -= 10; 
            }
                
            retAnimChain.Clear();

            for (int i = animId; i < animChain.Count; i+=9)
            {
                tmp = animChain[i];

                if (facing > 10)
                {
                    tmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                retAnimChain.Add(tmp);
            }
            return retAnimChain;
        }

        /* Test purposes only! */
        /// <summary>
        /// Draws all previously dumped frames on the level.
        /// </summary>
        /// <param name="canvas">GDI+ Graphics Object</param>
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
