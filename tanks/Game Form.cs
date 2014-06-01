using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace tanks
{
    public partial class Controller_MainForm : Form
    {
        View view;
        Model model;
        Thread modelPlay;
        Game_Settings_Form GS_Form;

        public Controller_MainForm(int amountSimpleTanks, int speedGame, int amountWalls, int amountHunterTanks)
        {
            InitializeComponent();
            model = new Model(amountSimpleTanks, speedGame, amountWalls, amountHunterTanks);
            view = new View(model);
            view.Location = new Point(10, 20);
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
            else if (model.gameStatus == GameStatus.STOP)
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
                        if (model.Player.projectile.distance == 0 && model.Player.projectile.cooldown > 100)
                        {
                            model.Player.projectile.cooldown = 0;
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

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modelPlay != null)
            {
                model.gameStatus = GameStatus.STOP;
                modelPlay.Abort();
            }
            model.NewGame();
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

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playButton.Image = Properties.Resources.PlayButton;
            model.gameStatus = GameStatus.STOP;
            GS_Form = new Game_Settings_Form();
            GS_Form.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string pathToFile = @"highscore.dat";
            
            FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);

            //if (sw.)
            sw.Close();
        }
    }
}

