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
            Update();
        }

        private void DrawPlayer(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.Player.Img, model.Player.X, model.Player.Y);
        }

        private void DrawTank(PaintEventArgs e)
        {
            foreach (Tank t in model.Tanks)
            {
                e.Graphics.DrawImage(t.Img, new Point(t.X, t.Y));
            }
        }

        private void DrawWall(PaintEventArgs e)
        {
            foreach (Wall w in model.Walls)
            {
                e.Graphics.DrawImage(w.Img, new Point(w.X, w.Y));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e);
        }
    }
}
