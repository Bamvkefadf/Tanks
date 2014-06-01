using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tanks
{
    class HunterTank : EnemyTank
    {
        HunterIMG hunterImg = new HunterIMG();

        void PutImg()
        {
            if (moving_direction == Direction.RIGHT)
            {
                moving_direction = Direction.RIGHT;
                img = hunterImg.Img10;
            }
            else if (moving_direction == Direction.DOWN)
            {
                moving_direction = Direction.DOWN;
                img = hunterImg.Img01;
            }
            else if (moving_direction == Direction.LEFT)
            {
                moving_direction = Direction.LEFT;
                img = hunterImg.Img_10;
            }
            else if (moving_direction == Direction.UP)
            {
                moving_direction = Direction.UP;
                img = hunterImg.Img0_1;
            }
        }

        public void Turn(int target_x, int target_y)
        {
            moving_direction = Direction.STOP;

            if (r.Next(500) < 250)
            {
                if (X > target_x)
                    moving_direction = Direction.LEFT;
                else if (X < target_x)
                    moving_direction = Direction.RIGHT;
            }
            else
            {
                if (Y > target_y)
                    moving_direction = Direction.UP;
                else if (Y < target_y)
                    moving_direction = Direction.DOWN;
            }
            
           PutImg();
        }

        new public void TurnAround()
        {
            if (moving_direction == Direction.LEFT)
                moving_direction = Direction.RIGHT;
            else if (moving_direction == Direction.RIGHT)
                moving_direction = Direction.LEFT;
            else if (moving_direction == Direction.UP)
                moving_direction = Direction.DOWN;
            else if (moving_direction == Direction.DOWN)
                moving_direction = Direction.UP;
            PutImg();
        }

        public void Run(int target_x, int target_y)
        {
            prev_x = x;
            prev_y = y;
            projectile.Run();
            GoDirection();

            if (
                ((X > target_x - 30 && X < target_x + 30)
                ||
                (Y > target_y - 30 && Y < target_y + 30)) 
                && 
                (projectile.distance == 0 && projectile.cooldown > 150)
                )
                Shoot();

            mustTurn++;

            ExternalWalls();

            PutImg();

            if (mustTurn == 40)
            {
                Turn(target_x, target_y);
                mustTurn = 0;
            }
        }

        public HunterTank(int x, int y) : base (x, y)
        {
            projectile.Img = Properties.Resources.ProjectileBlue;
            PutImg();
        }

    }
}
