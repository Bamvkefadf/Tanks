using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Player : IRun, ITurn, IExternalWalls, IPicture
    {
        PlayerIMG playerImg = new PlayerIMG();
        Image img;
        int x, y;
        int direct_x = 0, direct_y = 1;// 1 - вправо, вниз. 0 - без движения. -1 - влево, вверх
        static Random r;

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

        public Image Img
        {
            get { return img; }
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

        public Player()
        {
            r = new Random();

            img = playerImg.Img0_1;

            this.x = 300;
            this.y = 500;
            this.Direct_x = 0;
            this.Direct_y = -1;

            PutImg();
        }

        public void Run()
        {
            x += direct_x;
            y += direct_y;

            ExternalWalls();

            PutImg();

            Turn();
        }

        public void Turn()
        {

            PutImg();
        }

        public void ExternalWalls()
        {
            if (x <= 0)
                direct_x = 1;
            else if (x >= 761)
                direct_x = -1;
            else if (y <= 0)
                direct_y = 1;
            else if (y >= 561)
                direct_y = -1;
        }

        void PutImg()
        {
            if (direct_x == 1)
                img = playerImg.Img10;
            else if (direct_y == 1)
                img = playerImg.Img01;
            else if (direct_x == -1)
                img = playerImg.Img_10;
            else if (direct_y == -1)
                img = playerImg.Img0_1;
        }

    }
}
