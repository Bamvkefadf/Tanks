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

        public Controller_MainForm() : this(8) { }
        public Controller_MainForm(int amountTanks) : this(amountTanks, 15) { }
        public Controller_MainForm(int amountTanks, int speedGame) : this(amountTanks, speedGame, 30) { }

        public Controller_MainForm(int amountTanks, int speedGame, int amountWalls)
        {
            InitializeComponent();
            model = new Model(amountTanks, speedGame, amountWalls, 2);
            view = new View(model);
            view.Location = new Point(50, 50);
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

        private void PlayerController(object sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
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
    }
}

