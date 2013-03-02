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
    }
}
