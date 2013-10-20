using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class TankIMG
    {
        Image img = Properties.Resources.tank;

        public Image Img
        {
            get { return img; }
            set { img = value; }
        }
    }
}
