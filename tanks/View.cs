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
            if (model.gameStatus != GameStatus.PLAY)
                return;

            Thread.Sleep(model.speedGame);
            Invalidate();
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
            for (int i = 50; i < 600; i = i + 200)
                for (int n = 50; n < 600; n = n + 200)
                    e.Graphics.DrawImage(model.wall.Img, new Point(i, n));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e);
        }
    }
}
