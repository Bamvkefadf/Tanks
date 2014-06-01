using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tanks
{
    public abstract class Tank
    {
        protected int x, y;
        public Direction img_direction;
        protected int leftBorder = 0;
        protected int rightBorder = 761;
        protected int topBorder = 0;
        protected int bottomBorder = 561;

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

        public int GetDefaultProjectileX()
        {
            if (img_direction == Direction.UP)
            {
                return X + 11;
            }
            else if (img_direction == Direction.DOWN)
            {
                return X + 11;
            }
            else if (img_direction == Direction.LEFT)
            {
                return X;
            }
            else if (img_direction == Direction.RIGHT)
            {
                return X + 30;
            }
            return X;
        }
        public int GetDefaultProjectileY()
        {
            if (img_direction == Direction.UP)
            {
                return Y;
            }
            else if (img_direction == Direction.DOWN)
            {
                return Y + 30;
            }
            else if (img_direction == Direction.LEFT)
            {
                return Y + 11;
            }
            else if (img_direction == Direction.RIGHT)
            {
                return Y + 11;
            }
            return Y;
        }
        public abstract void Turn();
        public abstract void ExternalWalls();
        public abstract void PutCurrentImage();
        public abstract void PutImg();
    }
}
