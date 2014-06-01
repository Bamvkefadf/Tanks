using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class PlayerIMG
    {
        Image[] up = new Image[] { Properties.Resources.playerUP1, Properties.Resources.playerUP2, Properties.Resources.playerUP3, Properties.Resources.playerUP4 };
        Image[] down = new Image[] { Properties.Resources.playerDOWN1, Properties.Resources.playerDOWN2, Properties.Resources.playerDOWN3, Properties.Resources.playerDOWN4 };
        Image[] left = new Image[] { Properties.Resources.playerLEFT1, Properties.Resources.playerLEFT2, Properties.Resources.playerLEFT3, Properties.Resources.playerLEFT4 };
        Image[] right = new Image[] { Properties.Resources.playerRIGHT1, Properties.Resources.playerRIGHT2, Properties.Resources.playerRIGHT3, Properties.Resources.playerRIGHT4 };
        Image[] inv_up = new Image[] { Properties.Resources.playerInvurability};
        Image[] inv_down = new Image[] { Properties.Resources.playerInvurability};
        Image[] inv_right = new Image[] { Properties.Resources.playerInvurability};
        Image[] inv_left = new Image[] { Properties.Resources.playerInvurability};

        public Image[] Inv_down
        {
            get { return inv_down; }
            set { inv_down = value; }
        }
        public Image[] Inv_up
        {
            get { return inv_up; }
            set { inv_up = value; }
        }
        public Image[] Inv_left
        {
            get { return inv_left; }
            set { inv_left = value; }
        }
        public Image[] Inv_right
        {
            get { return inv_right; }
            set { inv_right = value; }
        }
        
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

        public PlayerIMG()
        {
            foreach (Image i in inv_down)
            {
                i.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            foreach (Image i in inv_left)
            {
                i.RotateFlip(RotateFlipType.Rotate270FlipY);
            }
            foreach (Image i in inv_right)
            {
                i.RotateFlip(RotateFlipType.Rotate90FlipY);
            }
        }
    }
}
