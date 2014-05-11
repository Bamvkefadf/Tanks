using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace tanks
{
    public partial class Controller_MainForm : Form
    {
        View view;
        Model model;
        Thread modelPlay;

        public Controller_MainForm() : this(12) { }
        public Controller_MainForm(int amountTanks) : this(amountTanks, 20) { }
        public Controller_MainForm(int amountTanks, int speedGame) : this(amountTanks, speedGame, 50) { }

        public Controller_MainForm(int amountTanks, int speedGame, int amountWalls)
        {
            InitializeComponent();
            model = new Model(amountTanks, speedGame, amountWalls, 2);
            view = new View(model);
            view.Location = new Point(10, 10);
            view.Visible = false;
            this.Controls.Add(view);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (model.gameStatus == GameStatus.PLAY)
            {
                modelPlay.Abort();
                model.gameStatus = GameStatus.STOP;
            }
            else
            {
                
                model.gameStatus = GameStatus.PLAY;
                modelPlay = new Thread(model.Play);
                modelPlay.Start();
                view.Invalidate();
                view.Visible = true;
                playButton.Location = new Point(-500, 0);
                playButton.Focus();
            }
        }

        private void Controller_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modelPlay != null)
            {
                model.gameStatus = GameStatus.STOP;
                modelPlay.Abort();
            }

            if (DialogResult.Cancel != MessageBox.Show("Вы точно хотите выйти?", "Танки", MessageBoxButtons.OKCancel))
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void playButton_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {

                case Keys.A:
                /*case "Ф":
                case "A":
                case "a":*/
                    {
                        model.Player.moving_direction = Direction.LEFT;
                    }
                    break;
                /*case "S":
                case "s":
                case "Ы":
                case "ы":*/
                case Keys.S:
                    {
                        model.Player.moving_direction = Direction.DOWN;
                    }
                    break;
                /*case "D":
                case "d":
                case "в":
                case "В":*/
                case Keys.D:
                    {
                        model.Player.moving_direction = Direction.RIGHT;
                    }
                    break;
                /*case "W":
                case "w":
                case "ц":
                case "Ц":*/
                case Keys.W:
                    {
                        model.Player.moving_direction = Direction.UP;
                    }
                    break;
                case Keys.Space:
                    {
                        if (model.Projectile.distance == 0)
                        {
                            model.Projectile.direction = model.Player.img_direction;
                            if (model.Projectile.direction == Direction.UP)
                            {
                                model.Projectile.X = model.Player.X + 20;
                                model.Projectile.Y = model.Player.Y;
                            }
                            else if (model.Projectile.direction == Direction.DOWN)
                            {
                                model.Projectile.X = model.Player.X + 20;
                                model.Projectile.Y = model.Player.Y + 40;
                            }
                            else if (model.Projectile.direction == Direction.LEFT)
                            {
                                model.Projectile.X = model.Player.X;
                                model.Projectile.Y = model.Player.Y + 20;
                            }
                            else if (model.Projectile.direction == Direction.RIGHT)
                            {
                                model.Projectile.X = model.Player.X + 40;
                                model.Projectile.Y = model.Player.Y + 20;
                            }
                        }

                    }
                    break;
                    
            }
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

