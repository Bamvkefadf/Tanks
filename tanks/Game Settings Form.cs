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
    }
}
