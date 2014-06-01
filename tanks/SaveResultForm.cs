using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Xml;

namespace tanks
{
    public partial class SaveResultForm : Form
    {
        int score = 0;
        List<int> scores;
        List<string> names;
        string pathToXml = @"highscore.xml";

        public SaveResultForm(int score, bool accessToSaveResult)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.language);
            InitializeComponent();
            scores = new List<int>();
            names = new List<string>();
            this.score = score;
            OpenHighscore();
            ShowResults();
            if (accessToSaveResult)
                SaveScores.Enabled = true;
        }

        private void ShowResults()
        {
            for (int i = 0; i < scores.Count; i++)
            {
                for (int j = 0; j < scores.Count - 1; j++)
                {
                    if (scores[j] < scores[j + 1])
                    {
                        int z = scores[j];
                        string x = names[j];
                        scores[j] = scores[j + 1];
                        names[j] = names[j + 1];
                        scores[j + 1] = z;
                        names[j + 1] = x;
                    }
                }
            }

            for (int i = 0; i < scores.Count; i++)
            {
                if (i == scores.Count - 1)
                    resultsTB.Text += scores[i] + " - " + names[i];
                else
                    resultsTB.Text += scores[i] + " - " + names[i] + Environment.NewLine;
            }
        }

        private void OpenHighscore()
        {
            names.Clear();
            scores.Clear();
            XmlDocument document = new XmlDocument();
            document.Load(pathToXml);
            XmlNodeList list = document.GetElementsByTagName("name");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement tname = (XmlElement)document.GetElementsByTagName("name")[i];         // Забиваем id в переменную  
                XmlElement tscore = (XmlElement)document.GetElementsByTagName("score")[i];      // Забиваем login в переменную  

                names.Add(tname.InnerText);
                scores.Add(Int32.Parse(tscore.InnerText));
            }
        }

        public void SaveResult_Click(object sender, EventArgs e)
        {
            string FormatMessage = "Unknown";

            if (NameTB.Text == "")
            {
                resultsTB.Text = FormatMessage;
            }

            if (scores.Count < 10)
            {
                scores.Add(score);
                names.Add(NameTB.Text);
            }
            else if (score > scores[scores.Count - 1] && scores.Count >= 10)
            {
                scores[scores.Count - 1] = score;
                names[scores.Count - 1] = NameTB.Text;
            }

            CreateXMLDocument();
            XmlDocument document = new XmlDocument();
            document.Load(pathToXml);

            for (int i = 0; i < scores.Count; i++)
            {
                XmlElement MyName = document.CreateElement("name");
                XmlElement MyScore = document.CreateElement("score"); // даём имя
                XmlText tName = document.CreateTextNode(names[i].ToString());
                XmlText tScore = document.CreateTextNode(scores[i].ToString());
                MyName.AppendChild(tName);
                MyScore.AppendChild(tScore);
                document.DocumentElement.AppendChild(MyName);
                document.DocumentElement.AppendChild(MyScore);
            }

            document.Save(pathToXml);
            SaveScores.Enabled = false;
            resultsTB.Text = "";
            OpenHighscore();
            ShowResults();
        }

        void CreateXMLDocument()
        {
            XmlTextWriter scoreWriter = new XmlTextWriter(pathToXml, Encoding.UTF8);
            scoreWriter.WriteStartDocument();
            scoreWriter.WriteStartElement("head");
            scoreWriter.WriteEndElement();
            scoreWriter.Close();
        }

        private void SaveResultForm_Load(object sender, EventArgs e)
        {

        }
    }
}
