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
            DrawProjectile(e);         

            if (model.gameStatus != GameStatus.PLAY)
                return;

            Thread.Sleep(model.speedGame);
            Invalidate();
        }

        private void DrawProjectile(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.Projectile.Img, new Point(model.Projectile.X, model.Projectile.Y));
        }

        private void DrawScore(PaintEventArgs e)
        {
            label2.Text = model.Score.CurrentScore.ToString();
            Update();
        }

        private void DrawPlayer(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.Player.Img, model.Player.X, model.Player.Y);
        }

        private void DrawTank(PaintEventArgs e)
        {
            for (int i = 0; i < model.SimpleTanks.Count; i++)
            {
                e.Graphics.DrawImage(model.SimpleTanks[i].Img, new Point(model.SimpleTanks[i].X, model.SimpleTanks[i].Y));
            }

            for (int i = 0; i < model.HunterTanks.Count; i++)
            {
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
    }
}
