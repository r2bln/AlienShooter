using System.Drawing;

namespace AlienShooter
{
    class Level
    {
        int width;
        int height;

        public Level(int _width, int _height)
        {
            width = _width;
            height = _height;
        }

        public void Draw(Graphics canvas)
        {
            for (int i = 0; i < width; i += 128)
            {
                for (int j = 0; j < height; j += 128)
                {
                    canvas.DrawImage(Resource1.grass, i, j);
                }
            }
        }
        public void DrawHex(Graphics canvas)
        {
            for (int i = 0; i < height - 128; i += 55)
            {
                for (int j = 0; j < width - 128 - 64; j += 128+64)
                {
                    if (i%2 == 0)
                    {
                        canvas.DrawImage(Resource1.hex, j + 95, i);
                        canvas.DrawRectangle(new Pen(Color.Black), j + 95, i, 128, 128);
                    }
                    else
                    {
                        canvas.DrawImage(Resource1.hex, j, i);
                        canvas.DrawRectangle(new Pen(Color.Black), j, i, 128, 128);
                    }
                }
            } 
        }
    }
}
