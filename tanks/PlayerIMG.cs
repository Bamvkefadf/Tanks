﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class PlayerIMG
    {
        Image img01 = Properties.Resources.playerTank01;
        Image img0_1 = Properties.Resources.playerTank0_1;
        Image img_10 = Properties.Resources.playerTank_10;
        Image img10 = Properties.Resources.playerTank10;

        public Image Img0_1
        {
            get { return img0_1; }
            set { img0_1 = value; }
        }

        public Image Img10
        {
            get { return img10; }
            set { img10 = value; }
        }

        public Image Img_10
        {
            get { return img_10; }
            set { img_10 = value; }
        }

        public Image Img01
        {
            get { return img01; }
            set { img01 = value; }
        }
    }
}
