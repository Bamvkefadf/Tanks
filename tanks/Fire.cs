using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Fire
    {
        FireIMG fireImg = new FireIMG();
        private Image img;
        int x, y;

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

        public Fire(int x, int y)
        {
            this.x = x;
            this.y = y;

            img = fireImg.Img_10;

            PutImg();
        }

        private void PutImg()
        {
                img = fireImg.Img10;
        }

    }
}
