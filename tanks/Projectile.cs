using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tanks
{
    class Projectile
    {
        private Image img;
        public int distanceToDestruct;
        int x, y;
        public Direction direction;
        public int safeProjectile = 0;
        TypeOfProjectile type;
        public bool ended = false;

        public bool throughWalls = false;
        public int damage = 1;
        public int speed = 6;

        public Image Img
        {
            get { return img; }
            set { img = value; }
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

        public Projectile(int distance, int x, int y, Direction direction, TypeOfProjectile type)
        {
            this.distanceToDestruct = distance;
            this.type = type;

            if (type == TypeOfProjectile.RED)
                SetRedType();
              
            if (type == TypeOfProjectile.BLUE)
                SetBlueType();

            if (type == TypeOfProjectile.PLAYER)
                SetPlayerType();

            this.x = x;
            this.y = y;
            this.direction = direction;
            Run();
        }

        private void SetPlayerType()
        {
            Img = Properties.Resources.PlayerProjectile;
        }

        private void SetRedType()
        {
            Img = Properties.Resources.ProjectileRed;
            speed = 3;
        }

        private void SetBlueType()
        {
            Img = Properties.Resources.ProjectileBlue;
            throughWalls = true;
        }

        public void Run()
        {
            distanceToDestruct = distanceToDestruct - speed;
            GoDirection();
            safeProjectile++;
            if (distanceToDestruct < 0)
                ended = true;
        }

        private void GoDirection()
        {
            if (direction == Direction.DOWN)
            {
                x += 0;
                y += speed;
            }
            else if (direction == Direction.UP)
            {
                x += 0;
                y += -speed;
            }
            else if (direction == Direction.RIGHT)
            {
                x += speed;
                y += 0;
            }
            else if (direction == Direction.LEFT)
            {
                x += -speed;
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
