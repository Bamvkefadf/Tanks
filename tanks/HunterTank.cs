using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tanks
{
    class HunterTank : EnemyTank
    {
        HunterIMG hunterImg = new HunterIMG();

        private void PutImg()
        {
            if (direct_x == 1)
                img = hunterImg.Img10;
            else if (direct_y == 1)
                img = hunterImg.Img01;
            else if (direct_x == -1)
                img = hunterImg.Img_10;
            else if (direct_y == -1)
                img = hunterImg.Img0_1;
        }

        public void Turn(int target_x, int target_y)
        {
            Direct_x = Direct_y = 0;

            if (X > target_x)
                Direct_x = -1;
            if (X < target_x)
                direct_x = 1;
            if (Y > target_y)
                direct_y = -1;
            if (Y < target_y)
                direct_y = 1;

            if (Direct_x != 0 || Direct_y != 0)
                if (r.Next(500) < 250)
                    Direct_x = 0;
                else
                    Direct_y = 0;

           PutImg();
        }

        new public void TurnAround()
        {
            Direct_x = -Direct_x;
            Direct_y = -Direct_y;
            PutImg();
        }

        public void Run(int target_x, int target_y)
        {
            x += direct_x;
            y += direct_y;
            mustTurn++;

            ExternalWalls();

            PutImg();

            if (mustTurn == 25)
            {
                Turn(target_x, target_y);
                mustTurn = 0;
            }
        }

        public HunterTank(int x, int y) : base (x, y)
        {
            PutImg();
        }

    }
}
