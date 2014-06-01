using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace tanks
{
    public class EnemyTank : Tank, ITurn, ITurnAround, IExternalWalls
    {
        private EnemyTankIMG tankImg = new EnemyTankIMG();
        private Image[] img;
        private Direction moving_direction;
        protected Image currentImage;
        protected int k;
        protected int prev_x, prev_y;
        protected int respawn_x, respawn_y;
        protected static Random r;
        protected int countToTurn = 0;
        protected int cooldown = 200;
        protected int distanceOfProjectile = 250;
        protected bool shot = false;

        public bool Shot
        {
            get { return shot; }
            set { shot = value; }
        }
        public Direction Moving_direction
        {
            get { return moving_direction; }
            set { moving_direction = value; }
        }
        public int DistanceOfProjectile
        {
            get { return distanceOfProjectile; }
            set { distanceOfProjectile = value; }
        }
        public int Respawn_y
        {
            get { return respawn_y; }
        }
        public int Respawn_x
        {
            get { return respawn_x; }
        }
        public Image CurrentImage
        {
            get { return currentImage; }
        }
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

            img = tankImg.Down;

            moving_direction = (Direction)r.Next(1, 5);
            PutCurrentImage();
            PutImg();

            this.x = x;
            this.y = y;
        }
        public Image[] Img
        {
            get { return img; }
        }

        public override void PutImg()
        {
            if (moving_direction == Direction.RIGHT)
            {
                moving_direction = Direction.RIGHT;
                img = tankImg.Right;
            }
            else if (moving_direction == Direction.DOWN)
            {
                moving_direction = Direction.DOWN;
                img = tankImg.Down;
            }
            else if (moving_direction == Direction.LEFT)
            {
                moving_direction = Direction.LEFT;
                img = tankImg.Left;
            }
            else if (moving_direction == Direction.UP)
            {
                moving_direction = Direction.UP;
                img = tankImg.Up;
            }
            img_direction = moving_direction;
        }
        //Анимация танков
        public override void PutCurrentImage()
        {
            currentImage = img[k];
            k++;
            if (k == 4)
                k = 0;
        }
        protected void Shoot(int value)
        {
            shot = true;
            cooldown = value;
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

        public void Run()
        {
            prev_x = x;
            prev_y = y;
            GoDirection();

            if ((r.Next(0, 500) > 450) && (cooldown <= 0))
                Shoot(200);

            countToTurn++;
            cooldown--;

            ExternalWalls();
            PutCurrentImage();
            PutImg();

            if (countToTurn > r.Next(40, 80))
            {
                Turn();
                countToTurn = 0;
            }
        }
        public override void Turn()
        {
            if (r.Next(0, 2) == 0)
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
            img_direction = moving_direction;
            PutImg();
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
            img_direction = moving_direction;
            PutImg();
        }
        public override void ExternalWalls()
        {
            if (x <= leftBorder)
                moving_direction = Direction.RIGHT;
            else if (x >= rightBorder)
                moving_direction = Direction.LEFT;
            else if (y <= topBorder)
                moving_direction = Direction.DOWN;
            else if (y >= bottomBorder)
                moving_direction = Direction.UP;
            
            img_direction = moving_direction;
        }
    }
}
