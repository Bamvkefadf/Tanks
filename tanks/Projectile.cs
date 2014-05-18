using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Projectile
    {
        private Image img;
        public int distance;
        public int cooldown;
        int x, y;
        public Direction direction;

        public Image Img
        {
            get { return img; }
            set { img = value; }
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

        public Projectile()
        {
            ProjectileDefaultSettings();
        }

        public void ProjectileDefaultSettings()
        {
            x = y = -10;
            direction = Direction.STOP;
            distance = 0;
        }

        public void Run()
        {
            if (cooldown <= 110)
                cooldown++;
            if (direction == Direction.STOP)
                return;
            distance = distance + 3;
            GoDirection();
            if (distance > 250)
                ProjectileDefaultSettings();
        }

        private void GoDirection()
        {
            if (direction == Direction.DOWN)
            {
                x += 0;
                y += 3;
            }
            else if (direction == Direction.UP)
            {
                x += 0;
                y += -3;
            }
            else if (direction == Direction.RIGHT)
            {
                x += 3;
                y += 0;
            }
            else if (direction == Direction.LEFT)
            {
                x += -3;
                y += 0;
            }
            else
            {
                x += 0;
                y += 0;
            }
        }
    }
}
