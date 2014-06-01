using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace tanks
{
    partial class View : UserControl
    {
        Model model;
        Random r;

        public View(Model model)
        {
            InitializeComponent();
            r = new Random();
            this.model = model;       
        }

        void Draw(PaintEventArgs e)
        {
            DrawWall(e);
            DrawTank(e);
            DrawPlayer(e);
            DrawScore(e);       

            if (model.gameStatus != GameStatus.PLAY)
                return;

            Thread.Sleep(model.speedGame);
            Invalidate();
        }

        private void DrawScore(PaintEventArgs e)
        {
            label2.Text = model.Score.CurrentScore.ToString();
            label4.Text = model.Player.Health.ToString();
            Update();
        }

        private void DrawPlayer(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.Player.Img, model.Player.X, model.Player.Y);
            e.Graphics.DrawImage(model.Player.projectile.Img, model.Player.projectile.X, model.Player.projectile.Y);
        }

        private void DrawTank(PaintEventArgs e)
        {
            for (int i = 0; i < model.SimpleTanks.Count; i++)
            {
                e.Graphics.DrawImage(model.SimpleTanks[i].Img, new Point(model.SimpleTanks[i].X, model.SimpleTanks[i].Y));
                e.Graphics.DrawImage(model.SimpleTanks[i].projectile.Img, new Point(model.SimpleTanks[i].projectile.X, model.SimpleTanks[i].projectile.Y));
            }

            for (int i = 0; i < model.HunterTanks.Count; i++)
            {
                e.Graphics.DrawImage(model.HunterTanks[i].projectile.Img, new Point(model.HunterTanks[i].projectile.X, model.HunterTanks[i].projectile.Y));
                e.Graphics.DrawImage(model.HunterTanks[i].Img, new Point(model.HunterTanks[i].X, model.HunterTanks[i].Y));
            }
        }

        private void DrawWall(PaintEventArgs e)
        {
            for (int i = 0; i < model.Walls.Count; i++)
            {
                e.Graphics.DrawImage(model.Walls[i].Img, new Point(model.Walls[i].X, model.Walls[i].Y));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e);
        }

        private void View_Load(object sender, EventArgs e)
        {

        }

        private void View_Click(object sender, EventArgs e)
        {
             
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                case 'ф':
                case 'A':
                case 'Ф':
                    {
                        model.Player.moving_direction = Direction.LEFT;
                    }
                    break;
                case 's':
                case 'ы':
                case 'S':
                case 'Ы':
                    {
                        model.Player.moving_direction = Direction.DOWN;
                    }
                    break;
                case 'd':
                case 'в':
                case 'D':
                case 'В':
                    {
                        model.Player.moving_direction = Direction.RIGHT;
                    }
                    break;
                case 'w':
                case 'ц':
                case 'W':
                case 'Ц':
                    {
                        model.Player.moving_direction = Direction.UP;
                    }
                    break;
                default:
                    {
                        if (model.Player.projectile.distance == 0)
                        {
                            model.Player.projectile.direction = model.Player.img_direction;
                            if (model.Player.projectile.direction == Direction.UP)
                            {
                                model.Player.projectile.X = model.Player.X + 20;
                                model.Player.projectile.Y = model.Player.Y;
                            }
                            else if (model.Player.projectile.direction == Direction.DOWN)
                            {
                                model.Player.projectile.X = model.Player.X + 20;
                                model.Player.projectile.Y = model.Player.Y + 40;
                            }
                            else if (model.Player.projectile.direction == Direction.LEFT)
                            {
                                model.Player.projectile.X = model.Player.X;
                                model.Player.projectile.Y = model.Player.Y + 20;
                            }
                            else if (model.Player.projectile.direction == Direction.RIGHT)
                            {
                                model.Player.projectile.X = model.Player.X + 40;
                                model.Player.projectile.Y = model.Player.Y + 20;
                            }
                        }

                    }
                    break;
            }

        }
    }
}
