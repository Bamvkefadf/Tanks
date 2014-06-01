using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace tanks
{
    partial class View : UserControl
    {
        ModeGame model;
        Random r;
        public int check = 1;

        public View(ModeGame model)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.language);
            InitializeComponent();
            r = new Random();
            this.model = model;       
        }

        public void SetLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.language);
            InitializeComponent();
        }

        public void Draw(PaintEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.language);
            DrawWall(e);
            DrawTank(e);
            DrawPlayer(e);
            DrawScore(e);
            DrawProjectiles(e);

            if (check == -1)
            {
                return;
            }

            Thread.Sleep(model.SpeedGame);
            Invalidate();
        }

        private void DrawProjectiles(PaintEventArgs e)
        {
            for (int i = 0; i < model.Projectiles.Count; i++)
            {
                e.Graphics.DrawImage(model.Projectiles[i].Img, new Point(model.Projectiles[i].X, model.Projectiles[i].Y));
            }
        }

        private void DrawScore(PaintEventArgs e)
        {
            label2.Text = model.Score.CurrentScore.ToString();
            label4.Text = model.Player.Health.ToString();
            Update();
        }

        private void DrawPlayer(PaintEventArgs e)
        {
                e.Graphics.DrawImage(model.Player.CurrentImage, model.Player.X, model.Player.Y);
        }

        private void DrawTank(PaintEventArgs e)
        {
            for (int i = 0; i < model.SimpleTanks.Count; i++)
            {
                e.Graphics.DrawImage(model.SimpleTanks[i].CurrentImage, new Point(model.SimpleTanks[i].X, model.SimpleTanks[i].Y));
            }

            for (int i = 0; i < model.HunterTanks.Count; i++)
            {
                e.Graphics.DrawImage(model.HunterTanks[i].CurrentImage, new Point(model.HunterTanks[i].X, model.HunterTanks[i].Y));
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
            if (check == -1)
                return;
            else
                Draw(e);
                
        }

        private void View_Load(object sender, EventArgs e)
        {

        }

        private void View_Click(object sender, EventArgs e)
        {
             
        }

        private void controlPlayer(object sender, KeyPressEventArgs e)
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
                        if (model.Player.cooldown <= 0)
                        {
                            model.Player.cooldown = 100;
                            model.Projectiles.Add(new Projectile(model.Player.distanceOfProjectile, model.Player.GetDefaultProjectileX(), model.Player.GetDefaultProjectileY(), model.Player.img_direction, TypeOfProjectile.PLAYER));
                        }
                    }
                    break;
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
