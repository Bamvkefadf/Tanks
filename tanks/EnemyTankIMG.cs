using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class EnemyTankIMG
    {
        Image[] up = new Image[] { Properties.Resources.simpleTankUP1, Properties.Resources.simpleTankUP2, Properties.Resources.simpleTankUP3, Properties.Resources.simpleTankUP4 };
        Image[] down = new Image[] { Properties.Resources.simpleTankDOWN1, Properties.Resources.simpleTankDOWN2, Properties.Resources.simpleTankDOWN3, Properties.Resources.simpleTankDOWN4 };
        Image[] left = new Image[] { Properties.Resources.simpleTankLEFT1, Properties.Resources.simpleTankLEFT2, Properties.Resources.simpleTankLEFT3, Properties.Resources.simpleTankLEFT4 };
        Image[] right = new Image[] { Properties.Resources.simpleTankRIGHT1, Properties.Resources.simpleTankRIGHT2, Properties.Resources.simpleTankRIGHT3, Properties.Resources.simpleTankRIGHT4 };

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
