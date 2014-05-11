using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Wall
    {
        WallIMG wallImg = new WallIMG();
        Image img;

        public Image Img
        {
            get { return img; }
        }

        public Wall()
        {
            img = wallImg.Img;
        }
    }
}
