using System.Collections.Generic;
using System.Drawing;

namespace AlienShooter
{
    class UnitFrames
    {
        Point loc;
        Rectangle rect;
        Size sz;
        Bitmap sprites;
        private int step;
        private int width;

        class Frame
        {
            public int width;
            public int height;
            public int number;
        }

        public UnitFrames(Bitmap spriteSrc)
        {
            sprites = new Bitmap(spriteSrc);
        }
        /*
        public List<Bitmap> getFrames()
        {
            for (int x = 0; x < width; x += step)
            {
                
            }
        }
         */
    }
}
