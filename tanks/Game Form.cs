using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Media;
using System.Resources;

namespace tanks
{
    public partial class Controller_MainForm : Form
    {
        View view;
        ModeGame model;
        Thread modelPlay;
        Game_Settings_Form GS_Form;
        SoundPlayer sp;
        String wantToExit, cancelButtonText, okButtonText;
        CultureInfo cultureInfo;
        private SaveResultForm srf;

        public Controller_MainForm(int amountSimpleTanks, int speedGame, int amountWalls, int amountHunterTanks)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.language);
            cultureInfo = new CultureInfo(Properties.Settings.Default.language);
            InitializeComponent();

            sp = new SoundPlayer(Properties.Resources.cannon_fire1);

            model = new ModeGame(Properties.Settings.Default.amountSimpleTanks, Properties.Settings.Default.speedGame, Properties.Settings.Default.amountWalls, Properties.Settings.Default.amountHunterTanks);
            view = new View(model);
            view.Location = new Point(10, 20);
            this.Controls.Add(view);
            SetLanguage(out wantToExit, out cancelButtonText, out okButtonText);
            SetLanguageButtonsToPlay();
            ChangeLanguage.Instance.localizeForm(this, cultureInfo);
        }

        private void StartPause(object sender, EventArgs e)
        {
            if (model.GameStatus == GameStatus.PLAY)
            {
                modelPlay.Abort();
                model.GameStatus = GameStatus.STOP;
                SetLanguageButtonsToPlay();
            }
            else if (model.GameStatus == GameStatus.STOP)
            {
                model.GameStatus = GameStatus.PLAY;
                modelPlay = new Thread(model.Play);
                modelPlay.Start();
                view.Invalidate();
                SetLanguageButtonsToPause();
                playButton.Focus();
            }
        }

        private void Controller_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            view.check = -1;
            view.Invalidate();
            SetLanguageButtonsToPlay();
            if (modelPlay != null)
            {
                model.GameStatus = GameStatus.STOP;
                modelPlay.Abort();
            }
            SetLanguage(out wantToExit, out cancelButtonText, out okButtonText);
            MessageBoxManager.Cancel = cancelButtonText;
            MessageBoxManager.OK = okButtonText;
            MessageBoxManager.Register();

            if (DialogResult.OK == MessageBox.Show(wantToExit, this.Text, MessageBoxButtons.OKCancel))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                view.check = 1;
            }
            SetLanguageButtonsToPlay();
            MessageBoxManager.Unregister();
            view.Invalidate();
        }

        private void SetLanguageButtonsToPause()
        {
            if (Properties.Settings.Default.language == "et")
                playButton.Image = Properties.Resources.enPauseButton;
            if (Properties.Settings.Default.language == "ru")
                playButton.Image = Properties.Resources.ruPauseButton;
            if (Properties.Settings.Default.language == "fr")
                playButton.Image = Properties.Resources.frPauseButton;
            if (Properties.Settings.Default.language == "de")
                playButton.Image = Properties.Resources.dePauseButton;
            if (Properties.Settings.Default.language == "es")
                playButton.Image = Properties.Resources.esPauseButton;
            if (Properties.Settings.Default.language == "zh-CN")
                playButton.Image = Properties.Resources.zhPauseButton;
            if (Properties.Settings.Default.language == "ar")
                playButton.Image = Properties.Resources.arPauseButton;
            if (Properties.Settings.Default.language == "pt")
                playButton.Image = Properties.Resources.ptPauseButton;
            if (Properties.Settings.Default.language == "hi")
                playButton.Image = Properties.Resources.hiPauseButton;
            if (Properties.Settings.Default.language == "el")
                playButton.Image = Properties.Resources.elPauseButton;
            if (Properties.Settings.Default.language == "ja")
                playButton.Image = Properties.Resources.jaPauseButton;
            if (Properties.Settings.Default.language == "ko")
                playButton.Image = Properties.Resources.koPauseButton;
            if (Properties.Settings.Default.language == "uk")
                playButton.Image = Properties.Resources.ukPauseButton;
            if (Properties.Settings.Default.language == "te")
                playButton.Image = Properties.Resources.tePauseButton;
            if (Properties.Settings.Default.language == "tr")
                playButton.Image = Properties.Resources.trPauseButton;
            if (Properties.Settings.Default.language == "it")
                playButton.Image = Properties.Resources.itPauseButton;
            if (Properties.Settings.Default.language == "hu")
                playButton.Image = Properties.Resources.huPauseButton;
            if (Properties.Settings.Default.language == "pl")
                playButton.Image = Properties.Resources.plPauseButton;
        }

        private void SetLanguageButtonsToPlay()
        {
            if (Properties.Settings.Default.language == "et")
                playButton.Image = Properties.Resources.enPlayButton;
            if (Properties.Settings.Default.language == "ru")
                playButton.Image = Properties.Resources.ruPlayButton;
            if (Properties.Settings.Default.language == "fr")
                playButton.Image = Properties.Resources.frPlayButton;
            if (Properties.Settings.Default.language == "de")
                playButton.Image = Properties.Resources.dePlayButton;
            if (Properties.Settings.Default.language == "es")
                playButton.Image = Properties.Resources.esPlayButton;
            if (Properties.Settings.Default.language == "zh-CN")
                playButton.Image = Properties.Resources.zhPlayButton;
            if (Properties.Settings.Default.language == "ar")
                playButton.Image = Properties.Resources.arPlayButton;
            if (Properties.Settings.Default.language == "pt")
                playButton.Image = Properties.Resources.ptPlayButton;
            if (Properties.Settings.Default.language == "hi")
                playButton.Image = Properties.Resources.hiPlayButton;
            if (Properties.Settings.Default.language == "el")
                playButton.Image = Properties.Resources.elPlayButton;
            if (Properties.Settings.Default.language == "ja")
                playButton.Image = Properties.Resources.jaPlayButton;
            if (Properties.Settings.Default.language == "ko")
                playButton.Image = Properties.Resources.koPlayButton;
            if (Properties.Settings.Default.language == "uk")
                playButton.Image = Properties.Resources.ukPlayButton;
            if (Properties.Settings.Default.language == "te")
                playButton.Image = Properties.Resources.tePlayButton;
            if (Properties.Settings.Default.language == "tr")
                playButton.Image = Properties.Resources.trPlayButton;
            if (Properties.Settings.Default.language == "it")
                playButton.Image = Properties.Resources.itPlayButton;
            if (Properties.Settings.Default.language == "hu")
                playButton.Image = Properties.Resources.huPlayButton;
            if (Properties.Settings.Default.language == "pl")
                playButton.Image = Properties.Resources.plPlayButton;
        }

        private void SetLanguage(out string wantToExit, out string cancelButtonText, out string okButtonText)
        {
            wantToExit = "";
            okButtonText = "";
            cancelButtonText = "";
            if (Properties.Settings.Default.language == "et")
            {
                wantToExit = Properties.Resources.enExit;
                cancelButtonText = Properties.Resources.enCancel;
                okButtonText = Properties.Resources.enOK;
                model.LoseMessage = Properties.Resources.enLose;
                model.WinMessage = Properties.Resources.enWin;
                model.TooMuchObjectsMessage = Properties.Resources.enTooMuch;
                playButton.Image = Properties.Resources.enPauseButton;
            }
            if (Properties.Settings.Default.language == "ru")
            {
                wantToExit = Properties.Resources.ruExit;
                cancelButtonText = Properties.Resources.ruCancel;
                okButtonText = Properties.Resources.ruOK;
                model.LoseMessage = Properties.Resources.ruLose;
                model.WinMessage = Properties.Resources.ruWin;
                model.TooMuchObjectsMessage = Properties.Resources.ruTooMuch;
                playButton.Image = Properties.Resources.ruPauseButton;
            }
            if (Properties.Settings.Default.language == "fr")
            {
                wantToExit = Properties.Resources.frExit;
                cancelButtonText = Properties.Resources.frCancel;
                okButtonText = Properties.Resources.frOK;
                model.LoseMessage = Properties.Resources.frLose;
                model.WinMessage = Properties.Resources.frWin;
                model.TooMuchObjectsMessage = Properties.Resources.frTooMuch;
                playButton.Image = Properties.Resources.frPauseButton;
            }
            if (Properties.Settings.Default.language == "de")
            {
                wantToExit = Properties.Resources.deExit;
                cancelButtonText = Properties.Resources.deCancel;
                okButtonText = Properties.Resources.deOK;
                model.LoseMessage = Properties.Resources.deLose;
                model.WinMessage = Properties.Resources.deWin;
                model.TooMuchObjectsMessage = Properties.Resources.deTooMuch;
                playButton.Image = Properties.Resources.dePauseButton;
            }
            if (Properties.Settings.Default.language == "es")
            {
                wantToExit = Properties.Resources.esExit;
                cancelButtonText = Properties.Resources.esCancel;
                okButtonText = Properties.Resources.esOK;
                model.LoseMessage = Properties.Resources.esLose;
                model.WinMessage = Properties.Resources.esWin;
                model.TooMuchObjectsMessage = Properties.Resources.esTooMuch;
                playButton.Image = Properties.Resources.esPauseButton;
            }
            if (Properties.Settings.Default.language == "zh-CN")
            {
                wantToExit = Properties.Resources.zhExit;
                cancelButtonText = Properties.Resources.zhCancel;
                okButtonText = Properties.Resources.zhOK;
                model.LoseMessage = Properties.Resources.zhLose;
                model.WinMessage = Properties.Resources.zhWin;
                model.TooMuchObjectsMessage = Properties.Resources.zhTooMuch;
                playButton.Image = Properties.Resources.zhPauseButton;
            }
            if (Properties.Settings.Default.language == "ar")
            {
                wantToExit = Properties.Resources.arExit;
                cancelButtonText = Properties.Resources.arCancel;
                okButtonText = Properties.Resources.arOK;
                model.LoseMessage = Properties.Resources.arLose;
                model.WinMessage = Properties.Resources.arWin;
                model.TooMuchObjectsMessage = Properties.Resources.arTooMuch;
                playButton.Image = Properties.Resources.arPauseButton;
            }
            if (Properties.Settings.Default.language == "pt")
            {
                wantToExit = Properties.Resources.ptExit;
                cancelButtonText = Properties.Resources.ptCancel;
                okButtonText = Properties.Resources.ptOK;
                model.LoseMessage = Properties.Resources.ptLose;
                model.WinMessage = Properties.Resources.ptWin;
                model.TooMuchObjectsMessage = Properties.Resources.ptTooMuch;
                playButton.Image = Properties.Resources.ptPauseButton;
            }
            if (Properties.Settings.Default.language == "hi")
            {
                wantToExit = Properties.Resources.hiExit;
                cancelButtonText = Properties.Resources.hiCancel;
                okButtonText = Properties.Resources.hiOK;
                model.LoseMessage = Properties.Resources.hiLose;
                model.WinMessage = Properties.Resources.hiWin;
                model.TooMuchObjectsMessage = Properties.Resources.hiTooMuch;
                playButton.Image = Properties.Resources.hiPauseButton;
            }
            if (Properties.Settings.Default.language == "el")
            {
                wantToExit = Properties.Resources.elExit;
                cancelButtonText = Properties.Resources.elCancel;
                okButtonText = Properties.Resources.elOK;
                model.LoseMessage = Properties.Resources.elLose;
                model.WinMessage = Properties.Resources.elWin;
                model.TooMuchObjectsMessage = Properties.Resources.elTooMuch;
                playButton.Image = Properties.Resources.elPauseButton;
            }
            if (Properties.Settings.Default.language == "ja")
            {
                wantToExit = Properties.Resources.jaExit;
                cancelButtonText = Properties.Resources.jaCancel;
                okButtonText = Properties.Resources.jaOK;
                model.LoseMessage = Properties.Resources.jaLose;
                model.WinMessage = Properties.Resources.jaWin;
                model.TooMuchObjectsMessage = Properties.Resources.jaTooMuch;
                playButton.Image = Properties.Resources.jaPauseButton;
            }
            if (Properties.Settings.Default.language == "ko")
            {
                wantToExit = Properties.Resources.koExit;
                cancelButtonText = Properties.Resources.koCancel;
                okButtonText = Properties.Resources.koOK;
                model.LoseMessage = Properties.Resources.koLose;
                model.WinMessage = Properties.Resources.koWin;
                model.TooMuchObjectsMessage = Properties.Resources.koTooMuch;
                playButton.Image = Properties.Resources.koPauseButton;
            }
            if (Properties.Settings.Default.language == "uk")
            {
                wantToExit = Properties.Resources.ukExit;
                cancelButtonText = Properties.Resources.ukCancel;
                okButtonText = Properties.Resources.ukOK;
                model.LoseMessage = Properties.Resources.ukLose;
                model.WinMessage = Properties.Resources.ukWin;
                model.TooMuchObjectsMessage = Properties.Resources.ukTooMuch;
                playButton.Image = Properties.Resources.ukPauseButton;
            }
            if (Properties.Settings.Default.language == "te")
            {
                wantToExit = Properties.Resources.teExit;
                cancelButtonText = Properties.Resources.teCancel;
                okButtonText = Properties.Resources.teOK;
                model.LoseMessage = Properties.Resources.teLose;
                model.WinMessage = Properties.Resources.teWin;
                model.TooMuchObjectsMessage = Properties.Resources.teTooMuch;
                playButton.Image = Properties.Resources.tePauseButton;
            }
            if (Properties.Settings.Default.language == "tr")
            {
                wantToExit = Properties.Resources.trExit;
                cancelButtonText = Properties.Resources.trCancel;
                okButtonText = Properties.Resources.trOK;
                model.LoseMessage = Properties.Resources.trLose;
                model.WinMessage = Properties.Resources.trWin;
                model.TooMuchObjectsMessage = Properties.Resources.trTooMuch;
                playButton.Image = Properties.Resources.trPauseButton;
            }
            if (Properties.Settings.Default.language == "it")
            {
                wantToExit = Properties.Resources.itExit;
                cancelButtonText = Properties.Resources.itCancel;
                okButtonText = Properties.Resources.itOK;
                model.LoseMessage = Properties.Resources.itLose;
                model.WinMessage = Properties.Resources.itWin;
                model.TooMuchObjectsMessage = Properties.Resources.itTooMuch;
                playButton.Image = Properties.Resources.itPauseButton;
            }
            if (Properties.Settings.Default.language == "hu")
            {
                wantToExit = Properties.Resources.huExit;
                cancelButtonText = Properties.Resources.huCancel;
                okButtonText = Properties.Resources.huOK;
                model.LoseMessage = Properties.Resources.huLose;
                model.WinMessage = Properties.Resources.huWin;
                model.TooMuchObjectsMessage = Properties.Resources.huTooMuch;
                playButton.Image = Properties.Resources.huPauseButton;
            }
            if (Properties.Settings.Default.language == "pl")
            {
                wantToExit = Properties.Resources.plExit;
                cancelButtonText = Properties.Resources.plCancel;
                okButtonText = Properties.Resources.plOK;
                model.LoseMessage = Properties.Resources.plLose;
                model.WinMessage = Properties.Resources.plWin;
                model.TooMuchObjectsMessage = Properties.Resources.plTooMuch;
                playButton.Image = Properties.Resources.plPauseButton;
            }
            model.OkMessage = okButtonText;
            SetLanguageButtonsToPause();
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
                        if (model.Player.cooldown <= 0)
                        {
                            model.Player.cooldown = 100;
                            model.Projectiles.Add(new Projectile(model.Player.distanceOfProjectile, model.Player.GetDefaultProjectileX(), model.Player.GetDefaultProjectileY(), model.Player.img_direction, TypeOfProjectile.PLAYER));
                            sp.Play();
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
                model.GameStatus = GameStatus.STOP;
                modelPlay.Abort();
            }
            if (Properties.Settings.Default.mode == 0)
                model.NewGame();
            SetLanguageButtonsToPlay();
            view.Refresh();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguageButtonsToPlay();
            model.GameStatus = GameStatus.STOP;
            HelpForm hp = new HelpForm();
            hp.ShowDialog();
        }

        private void играToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguageButtonsToPlay();
            model.GameStatus = GameStatus.STOP;
            GS_Form = new Game_Settings_Form();
            GS_Form.ShowDialog();
            cultureInfo = new CultureInfo(Properties.Settings.Default.language);
            ChangeLanguage.Instance.localizeForm(this, cultureInfo);
            view.SetLanguage();
            SetLanguageButtonsToPlay();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void Controller_MainForm_Load(object sender, EventArgs e)
        {

        }

        private void переводToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this.
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (modelPlay != null)
            {
                model.GameStatus = GameStatus.STOP;
                modelPlay.Abort();
                SetLanguageButtonsToPlay();
            }
            srf = new SaveResultForm(model.Score.CurrentScore, false);
            srf.ShowDialog();
        }
    }
}

