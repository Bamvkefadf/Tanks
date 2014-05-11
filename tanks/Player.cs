using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Player : IRun, ITurn, IExternalWalls, IPicture
    {
        PlayerIMG playerImg = new PlayerIMG();
        Image img;
        int x, y;
        public Direction moving_direction;
        public Direction img_direction;
        int temp_x, temp_y;             //координаты для сохранения игрока внутри поля
        static Random r;

        public Image Img
        {
            get { return img; }
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

        public Player()
        {
            r = new Random();

            img = playerImg.Img0_1;

            this.x = 400;
            this.y = 550;
            moving_direction = Direction.UP;

            PutImg();
        }

        public void Run()
        {
            temp_x = x;
            temp_y = y;

            GoDirection();

            ExternalWalls();

            PutImg();

            Turn();
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

        public void Turn()
        {

            PutImg();
        }

        public void ExternalWalls()
        {
            if (x <= 0 || x >= 761 || y <= 0 || y >= 561)
            {
                moving_direction = Direction.STOP;
                x = temp_x;
                y = temp_y;
            }
        }

        void PutImg()
        {
            if (moving_direction == Direction.RIGHT)
            {
                img_direction = Direction.RIGHT;
                img = playerImg.Img10;
            }
            else if (moving_direction == Direction.DOWN)
            {
                img_direction = Direction.DOWN;
                img = playerImg.Img01;
            }
            else if (moving_direction == Direction.LEFT)
            {
                img_direction = Direction.LEFT;
                img = playerImg.Img_10;
            }
            else if (moving_direction == Direction.UP)
            {
                img_direction = Direction.UP;
                img = playerImg.Img0_1;
            }
        }

    }
}
