using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace tanks
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Controller_MainForm cm = new Controller_MainForm(Properties.Settings.Default.amountSimpleTanks, Properties.Settings.Default.speedGame, Properties.Settings.Default.amountWalls, Properties.Settings.Default.amountHunterTanks);
            Application.Run(cm);
        }
    }
}
