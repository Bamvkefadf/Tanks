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
            Controller_MainForm cm;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            switch (args.Length)
            { 
                case 0:
                default:
                    cm = new Controller_MainForm(); break;
                case 1:
                    cm = new Controller_MainForm(Convert.ToInt32(args[0])); break;
                case 2:
                    cm = new Controller_MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1])); break;
                case 3:
                    cm = new Controller_MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]), Convert.ToInt32(args[2])); break;
                

            }
            Application.Run(cm);
        }
    }
}
