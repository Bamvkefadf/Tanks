using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    public class Player : Tank, IRun, ITurn, IExternalWalls
    {
        PlayerIMG playerImg = new PlayerIMG();
        Image[] img;
        Image currentImage;
        int k;  
        int health;
        int invulnerability = 200;
        int respawnX = 400;
        int respawnY = 550;
        public int cooldown = 100;
        public int distanceOfProjectile = 250;
        public bool canDamaged = false;
        public Direction moving_direction;

        int temp_x, temp_y;             //координаты для сохранения игрока внутри поля
        static Random r;

        public Image CurrentImage
        {
            get { return currentImage; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Invulnerability
        {
            get { return invulnerability; }
            set { invulnerability = value; }
        }

        public Image[] Img
        {
            get { return img; }
        }
               
        public Player()
        {
            r = new Random();

            img = playerImg.Up;
            health = 3;

            this.x = respawnX;
            this.y = respawnY;
            moving_direction = Direction.UP;
            PutCurrentImage();
            PutImg();
        }
        public void Run()
        {
            temp_x = x;
            temp_y = y;
            cooldown--;
            if (canDamaged == false)
                invulnerability--;

            if (invulnerability <= 0)
            {
                canDamaged = true;
            }

            GoDirection();
            ExternalWalls();
            PutCurrentImage();
            PutImg();

            Turn();
        }
        public override void Turn()
        {
            PutImg();
        }
        public override void ExternalWalls()
        {
            if (x <= leftBorder || x >= rightBorder || y <= topBorder || y >= bottomBorder)
            {
                moving_direction = Direction.STOP;
                x = temp_x;
                y = temp_y;
            }
        }
        public void ResetPlayer()
        {
            canDamaged = false;
            invulnerability = 200;
            X = respawnX;
            Y = respawnY;
        }

        public override void PutCurrentImage()
        {
            if (canDamaged == false)
            {
                currentImage = img[0];
                k = 0;
            }
            else
            {
                try
                {
                    currentImage = img[k];
                }
                catch (IndexOutOfRangeException)
                {
                    currentImage = img[0];
                }
                k++;
                if (k == 4)
                    k = 0;
            }
        }
        public override void PutImg()
        {
            if (canDamaged == false)
            {
                if (moving_direction == Direction.RIGHT)
                {
                    img_direction = Direction.RIGHT;
                    img = playerImg.Inv_right;
                }
                else if (moving_direction == Direction.DOWN)
                {
                    img_direction = Direction.DOWN;
                    img = playerImg.Inv_down;
                }
                else if (moving_direction == Direction.LEFT)
                {
                    img_direction = Direction.LEFT;
                    img = playerImg.Inv_left;
                }
                else if (moving_direction == Direction.UP)
                {
                    img_direction = Direction.UP;
                    img = playerImg.Inv_up;
                }
            }
            else
            {
                if (moving_direction == Direction.RIGHT)
                {
                    img_direction = Direction.RIGHT;
                    img = playerImg.Right;
                }
                else if (moving_direction == Direction.DOWN)
                {
                    img_direction = Direction.DOWN;
                    img = playerImg.Down;
                }
                else if (moving_direction == Direction.LEFT)
                {
                    img_direction = Direction.LEFT;
                    img = playerImg.Left;
                }
                else if (moving_direction == Direction.UP)
                {
                    img_direction = Direction.UP;
                    img = playerImg.Up;
                }
            }
        }

        private void GoDirection()
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

    }
}
