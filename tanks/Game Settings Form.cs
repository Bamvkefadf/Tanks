using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace tanks
{
    public partial class Game_Settings_Form : Form
    {
        public CultureInfo cultureInfo;

        public Game_Settings_Form()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.language);
            InitializeComponent();
            textBox2.Text = Properties.Settings.Default.amountHunterTanks.ToString();
            textBox1.Text = Properties.Settings.Default.amountSimpleTanks.ToString();
            textBox3.Text = Properties.Settings.Default.amountWalls.ToString();
            textBox4.Text = (101 - Properties.Settings.Default.speedGame).ToString();

            if (Properties.Settings.Default.music == true)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
            if (Properties.Settings.Default.sound == true)
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.amountHunterTanks = Convert.ToInt32(textBox2.Text);
            Properties.Settings.Default.amountSimpleTanks = Convert.ToInt32(textBox1.Text);
            Properties.Settings.Default.amountWalls = Convert.ToInt32(textBox3.Text);

            Properties.Settings.Default.speedGame = 101 - Convert.ToInt32(textBox4.Text);
            Properties.Settings.Default.music = checkBox1.Checked;
            Properties.Settings.Default.sound = checkBox2.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || Convert.ToInt32(textBox4.Text) > 100
                || textBox1.Text == "0" || textBox2.Text == "0" || textBox3.Text == "0" || textBox4.Text == "0" ||
                (Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text)) > 111)
            {
                button1.Enabled = false;
                label6.Visible = true;
            }
            else
            {
                button1.Enabled = true;
                label6.Visible = false;
            }
        }

        private void checkKeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndexChanged -= new System.EventHandler(this.comboBox1_SelectedIndexChanged);

            //cultureInfo = new CultureInfo(Properties.Settings.Default.language);
            if (comboBox1.SelectedIndex == 0) cultureInfo = new CultureInfo("et");
            if (comboBox1.SelectedIndex == 1) cultureInfo = new CultureInfo("ru");
            if (comboBox1.SelectedIndex == 2) cultureInfo = new CultureInfo("fr");
            if (comboBox1.SelectedIndex == 3) cultureInfo = new CultureInfo("de");
            if (comboBox1.SelectedIndex == 4) cultureInfo = new CultureInfo("es");
            if (comboBox1.SelectedIndex == 5) cultureInfo = new CultureInfo("zh-CN");
            if (comboBox1.SelectedIndex == 6) cultureInfo = new CultureInfo("ar");
            if (comboBox1.SelectedIndex == 7) cultureInfo = new CultureInfo("pt");
            if (comboBox1.SelectedIndex == 8) cultureInfo = new CultureInfo("hi");
            if (comboBox1.SelectedIndex == 9) cultureInfo = new CultureInfo("el");
            if (comboBox1.SelectedIndex == 10) cultureInfo = new CultureInfo("ja");
            if (comboBox1.SelectedIndex == 11) cultureInfo = new CultureInfo("ko");
            if (comboBox1.SelectedIndex == 12) cultureInfo = new CultureInfo("uk");
            if (comboBox1.SelectedIndex == 13) cultureInfo = new CultureInfo("te");
            if (comboBox1.SelectedIndex == 14) cultureInfo = new CultureInfo("tr");
            if (comboBox1.SelectedIndex == 15) cultureInfo = new CultureInfo("it");
            if (comboBox1.SelectedIndex == 16) cultureInfo = new CultureInfo("hu");
            if (comboBox1.SelectedIndex == 17) cultureInfo = new CultureInfo("pl");

            ChangeLanguage.Instance.localizeForm(this, cultureInfo);
            Properties.Settings.Default.language = cultureInfo.Name;
            Properties.Settings.Default.languageNum = comboBox1.SelectedIndex;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
        }

        private void Game_Settings_Form_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = Properties.Settings.Default.languageNum;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
