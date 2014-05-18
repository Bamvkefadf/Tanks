using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tanks
{
    public partial class Game_Settings_Form : Form
    {
        public Game_Settings_Form()
        {
            InitializeComponent();
            textBox2.Text = Properties.Settings.Default.amountHunterTanks.ToString();
            textBox1.Text = Properties.Settings.Default.amountSimpleTanks.ToString();
            textBox3.Text = Properties.Settings.Default.amountWalls.ToString();
            textBox4.Text = Properties.Settings.Default.speedGame.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.amountHunterTanks = Convert.ToInt32(textBox2.Text);
            Properties.Settings.Default.amountSimpleTanks = Convert.ToInt32(textBox1.Text);
            Properties.Settings.Default.amountWalls = Convert.ToInt32(textBox3.Text);
            Properties.Settings.Default.speedGame = Convert.ToInt32(textBox4.Text);
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || Convert.ToInt32(textBox1.Text) > 50
                || textBox2.Text == "" || Convert.ToInt32(textBox1.Text) > 50
                || textBox3.Text == "" || Convert.ToInt32(textBox1.Text) > 50
                || textBox4.Text == "" || Convert.ToInt32(textBox1.Text) > 50)
            {
                button1.Enabled = false;
                toolTip1.Show("Необходимо ввести число < 50", button1, 2000);
            }
            else
                button1.Enabled = true;
        }

        private void checkKeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }
    }
}
