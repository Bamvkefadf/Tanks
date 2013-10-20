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

        public View(Model model)
        {
            InitializeComponent();

            this.model = model;       
        }

        void Draw(PaintEventArgs e)
        {
            DrawWall(e);
            DrawTank(e);
            if (model.gameStatus != GameStatus.PLAY)
                return;

            Thread.Sleep(model.speedGame);
            Invalidate();
        }

        private void DrawTank(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.tank.Img, new Point(model.tank.X, model.tank.Y));
        }

        private void DrawWall(PaintEventArgs e)
        {
            for (int i = 50; i < 600; i = i + 150)
                for (int n = 50; n < 600; n = n + 100)
                    e.Graphics.DrawImage(model.wall.Img, new Point(i, n));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e);
        }
    }
}
