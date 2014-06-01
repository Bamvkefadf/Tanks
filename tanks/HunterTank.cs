using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class HunterTank : EnemyTank
    {
        HunterIMG hunterImg = new HunterIMG();
        Image[] img;

        public HunterTank(int x, int y): base(x, y)
        {
            DistanceOfProjectile = 200;
            PutImg();
            PutCurrentImage();
            cooldown = 100;
        }
        public void Turn(int target_x, int target_y)
        {
            Moving_direction = Direction.STOP;

            if (r.Next(500) < 250)
            {
                if (X > target_x)
                    Moving_direction = Direction.LEFT;
                else if (X < target_x)
                    Moving_direction = Direction.RIGHT;
            }
            else
            {
                if (Y > target_y)
                    Moving_direction = Direction.UP;
                else if (Y < target_y)
                    Moving_direction = Direction.DOWN;
            }

            PutImg();
        }
        new public void TurnAround()
        {
            if (Moving_direction == Direction.LEFT)
                Moving_direction = Direction.RIGHT;
            else if (Moving_direction == Direction.RIGHT)
                Moving_direction = Direction.LEFT;
            else if (Moving_direction == Direction.UP)
                Moving_direction = Direction.DOWN;
            else if (Moving_direction == Direction.DOWN)
                Moving_direction = Direction.UP;
            PutImg();
        }
        new public void Run(int target_x, int target_y)
        {
            prev_x = x;
            prev_y = y;

            GoDirection();

            if (
                ((X > target_x - 30 && X < target_x + 30)
                ||
                (Y > target_y - 30 && Y < target_y + 30))
                &&
                (cooldown <= 0)
                )
                Shoot(100);

            countToTurn++;
            cooldown--;

            ExternalWalls();
            PutCurrentImage();
            PutImg();

            if (countToTurn == 40)
            {
                Turn(target_x, target_y);
                countToTurn = 0;
            }
        }
        new private void PutImg()
        {
            if (Moving_direction == Direction.RIGHT)
            {
                Moving_direction = Direction.RIGHT;
                img = hunterImg.Right;
            }
            else if (Moving_direction == Direction.DOWN)
            {
                Moving_direction = Direction.DOWN;
                img = hunterImg.Down;
            }
            else if (Moving_direction == Direction.LEFT)
            {
                Moving_direction = Direction.LEFT;
                img = hunterImg.Left;
            }
            else if (Moving_direction == Direction.UP)
            {
                Moving_direction = Direction.UP;
                img = hunterImg.Up;
            }
        }
        new private void PutCurrentImage()
        {
            currentImage = img[k];
            k++;
            if (k == 4)
                k = 0;
        }


    }
}
