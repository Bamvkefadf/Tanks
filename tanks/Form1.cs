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
            this.Controls.Add(view);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (model.gameStatus == GameStatus.PLAY)
            {
                modelPlay.Abort();
                model.gameStatus = GameStatus.STOP;
                playButton.Image = Properties.Resources.PlayButton;
            }
            else
            {
                model.gameStatus = GameStatus.PLAY;
                modelPlay = new Thread(model.Play);
                modelPlay.Start();
                view.Invalidate();
                playButton.Image = Properties.Resources.PauseButton;
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
                    {
                        model.Player.moving_direction = Direction.LEFT;
                    }
                    break;
                case Keys.S:
                    {
                        model.Player.moving_direction = Direction.DOWN;
                    }
                    break;
                case Keys.D:
                    {
                        model.Player.moving_direction = Direction.RIGHT;
                    }
                    break;
                case Keys.W:
                    {
                        model.Player.moving_direction = Direction.UP;
                    }
                    break;
                case Keys.Space:
                    {
                        if (model.Projectile.distance == 0 && model.Projectile.cooldown > 100)
                        {
                            model.Projectile.cooldown = 0;
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

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.NewGame();
            //view.Visible = false;
            playButton.Image = Properties.Resources.PlayButton;
            view.Refresh();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playButton.Image = Properties.Resources.PlayButton;
            model.gameStatus = GameStatus.STOP;
            MessageBox.Show(@"Разработчики: 
                            Вальт Игорь 
                            Гофман Александр
                            Ефимов Александр", "Танки");
        }

        private void играToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

