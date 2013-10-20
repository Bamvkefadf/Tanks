using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Tank : IRun, ITurn
    {
        TankIMG tankImg = new TankIMG();
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

        public Tank()
        {
            r = new Random();
            img = tankImg.Img;
            x = 80;
            y = 80;
        }

        public int Y
        {
            get { return y; }
        }

        public int X
        {
            get { return x; }
        }

        public Image Img
        {
            get { return img; }
        }


        public void Run()
        {
            x += direct_x;
            y += direct_y;
        }

        public void Turn()
        {
            if ((Math.IEEERemainder(x, 40) == 0) && (Math.IEEERemainder(y, 40) == 0))
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
            }
        }

    }
}
