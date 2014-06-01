using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace tanks
{
    class EnemyTank : ITurn, ITurnAround, IExternalWalls, IPicture
    {
        private TankIMG tankImg = new TankIMG();
        protected Image img;
        protected int x, y;
        protected int prev_x, prev_y;
        protected static Random r;
        protected int mustTurn;
        public Direction moving_direction;
        public Projectile projectile;

        public int Prev_y
        {
            get { return prev_y; }
        }

        public int Prev_x
        {
            get { return prev_x; }
        }

        public EnemyTank(int x, int y)
        {
            r = new Random();

            img = tankImg.Img0_1;
            projectile = new Projectile();
            projectile.Img = Properties.Resources.ProjectileRed;
            moving_direction = (Direction)r.Next(1, 5);

            PutImg();

            this.x = x;
            this.y = y;
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public Image Img
        {
            get { return img; }
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
                Turn();
                mustTurn = 0;
            }
        }

        protected void Shoot()
        {
            projectile.direction = moving_direction;
            if (projectile.direction == Direction.UP)
            {
                projectile.X = X + 20;
                projectile.Y = Y;
            }
            else if (projectile.direction == Direction.DOWN)
            {
                projectile.X = X + 20;
                projectile.Y = Y + 40;
            }
            else if (projectile.direction == Direction.LEFT)
            {
                projectile.X = X;
                projectile.Y = Y + 20;
            }
            else if (projectile.direction == Direction.RIGHT)
            {
                projectile.X = X + 40;
                projectile.Y = Y + 20;
            }
        }

        protected void GoDirection()
        {
            if (moving_direction == Direction.DOWN)
            {
                x += 0;
                y += 1;
            }
            else if (moving_direction == Direction.UP)
            {
                x += 0;
                y += -1;
            }
            else if (moving_direction == Direction.RIGHT)
            {
                x += 1;
                y += 0;
            }
            else if (moving_direction == Direction.LEFT)
            {
                x += -1;
                y += 0;
            }
            else
            {
                x += 0;
                y += 0;
            }
        }

        public void Turn()
        {
                if (r.Next(500) < 250)
                {
                    if (moving_direction == Direction.LEFT || moving_direction == Direction.RIGHT)
                    {
                        moving_direction = (Direction)r.Next(3, 5);
                    }
                }
                else
                {
                    if (moving_direction == Direction.UP || moving_direction == Direction.DOWN)
                    {
                        moving_direction = (Direction)r.Next(1, 3);
                    }
                }

                PutImg();
        }

        public void ExternalWalls()
        {
            if (x <= 0)
                moving_direction = Direction.RIGHT;
            else if (x >= 761)
                moving_direction = Direction.LEFT;
            else if (y <= 0)
                moving_direction = Direction.DOWN;
            else if (y >= 561)
                moving_direction = Direction.UP;
        }

        void PutImg()
        {
            if (moving_direction == Direction.RIGHT)
            {
                moving_direction = Direction.RIGHT;
                img = tankImg.Img10;
            }
            else if (moving_direction == Direction.DOWN)
            {
                moving_direction = Direction.DOWN;
                img = tankImg.Img01;
            }
            else if (moving_direction == Direction.LEFT)
            {
                moving_direction = Direction.LEFT;
                img = tankImg.Img_10;
            }
            else if (moving_direction == Direction.UP)
            {
                moving_direction = Direction.UP;
                img = tankImg.Img0_1;
            }
        }

        public void TurnAround()
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
    }
}
