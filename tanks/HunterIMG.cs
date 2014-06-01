using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class HunterIMG
    {
        Image[] up = new Image[] { Properties.Resources.hunterTankUP1, Properties.Resources.hunterTankUP2, Properties.Resources.hunterTankUP3, Properties.Resources.hunterTankUP4 };
        Image[] down = new Image[] { Properties.Resources.hunterTankDOWN1, Properties.Resources.hunterTankDOWN2, Properties.Resources.hunterTankDOWN3, Properties.Resources.hunterTankDOWN4 };
        Image[] left = new Image[] { Properties.Resources.hunterTankLEFT1, Properties.Resources.hunterTankLEFT2, Properties.Resources.hunterTankLEFT3, Properties.Resources.hunterTankLEFT4 };
        Image[] right = new Image[] { Properties.Resources.hunterTankRIGHT1, Properties.Resources.hunterTankRIGHT2, Properties.Resources.hunterTankRIGHT3, Properties.Resources.hunterTankRIGHT4 };

        public Image[] Up
        {
            get { return up; }
            set { up = value; }
        }
        public Image[] Down
        {
            get { return down; }
            set { down = value; }
        }
        public Image[] Left
        {
            get { return left; }
            set { left = value; }
        }
        public Image[] Right
        {
            get { return right; }
            set { right = value; }
        }
    }
}
