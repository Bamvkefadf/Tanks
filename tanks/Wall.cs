using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Wall : IPicture
    {
        WallIMG wallImg = new WallIMG();
        int health = 3;
        int currentHealth;
        Image img;
        int x, y;

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public Image Img
        {
            get { return img; }
        }

        public Wall(int x, int y)
        {
            img = wallImg.Img;

            currentHealth = health;

            this.x = x;
            this.y = y;
        }
    }
}
