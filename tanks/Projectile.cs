using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Projectile
    {
        private ProjectileIMG projectileImg = new ProjectileIMG();
        private Image img;
        public int distance;
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
            img = projectileImg.Img_10;
            ProjectileDefaultSettings();
        }

        public void ProjectileDefaultSettings()
        {
            x = y = -10;
            direction = Direction.STOP;
            distance = 0;
        }

        private void PutImg()
        {
            if (direction == Direction.RIGHT)
                img = projectileImg.Img10;
            else if (direction == Direction.DOWN)
                img = projectileImg.Img01;
            else if (direction == Direction.LEFT)
                img = projectileImg.Img_10;
            else if (direction == Direction.UP)
                img = projectileImg.Img0_1;
        }

        public void Run()
        {
            if (direction == Direction.STOP)
                return;
            distance = distance + 3;
            PutImg();
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
