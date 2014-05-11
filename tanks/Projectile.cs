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
        int x, y;
        int direct_x, direct_y;

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

        public int Direct_y
        {
            get { return direct_y; }
            set
            {
                if (value == 0 || value == 1 || value == -1)
                    direct_y = value;
                else direct_y = 0;
            }
        }

        public int Direct_x
        {
            get { return direct_x; }
            set
            {
                if (value == 0 || value == 1 || value == -1)
                    direct_x = value;
                else direct_x = 0;
            }
        }

        public Projectile()
        {
            img = projectileImg.Img_10;
            x = y = -10;
            direct_x = direct_y = 0;
        }

        private void PutImg()
        {
            if (direct_x == 1)
                img = projectileImg.Img10;
            else if (direct_y == 1)
                img = projectileImg.Img01;
            else if (direct_x == -1)
                img = projectileImg.Img_10;
            else if (direct_y == -1)
                img = projectileImg.Img0_1;
        }

        public void Run()
        {
            PutImg();
            x += direct_x * 3;
            y += direct_y * 3;
        }
    }
}
