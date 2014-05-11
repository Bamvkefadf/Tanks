using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace tanks
{
    class EnemyTank : IRun, ITurn, ITurnAround, IExternalWalls, IPicture
    {
        private TankIMG tankImg = new TankIMG();
        protected Image img;
        protected int x, y;
        protected int direct_x = 0, direct_y = 1;// 1 - вправо, вниз. 0 - без движения. -1 - влево, вверх
        protected static Random r;
        protected int mustTurn;

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

        public EnemyTank(int x, int y)
        {
            r = new Random();

            img = tankImg.Img0_1;

            Direct_x = r.Next(-1, 2);
            if (Direct_x != 0)
                Direct_y = 0;
            else
                Direct_y = r.Next(-1, 2);

            PutImg();

            this.x = x;
            this.y = y;
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

        public void Run()
        {
            x += direct_x;
            y += direct_y;
            mustTurn++;
            
            ExternalWalls();

            PutImg();

            if (mustTurn == 25)
            {
                Turn();
                mustTurn = 0;
            }
        }

        public void Turn()
        {
                if (r.Next(500) < 250)
                {
                    if (Direct_y == 0)
                    {
                        Direct_x = 0;
                        while (Direct_y == 0)
                            Direct_y = r.Next(-1, 2);
                    }
                }
                else
                {
                    if (Direct_x == 0)
                    {
                        Direct_y = 0;
                        while (Direct_x == 0)
                            Direct_x = r.Next(-1, 2);
                    }
                }

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

        private void PutImg()
        {
            if (direct_x == 1)
                img = tankImg.Img10;
            else if (direct_y == 1)
                img = tankImg.Img01;
            else if (direct_x == -1)
                img = tankImg.Img_10;
            else if (direct_y == -1)
                img = tankImg.Img0_1;
        }

        public void TurnAround()
        {
            Direct_x = -Direct_x;
            Direct_y = -Direct_y;
            PutImg();
        }
    }
}
