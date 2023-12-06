using System;
using System.Windows.Forms;
using System.IO;

namespace Fatkullin_AA_BIVT_21_14_HW_1_Calculator
{
    public partial class LogFileReader : Form
    {
        public LogFileReader()
        {
            InitializeComponent();
        }

        private void LogFileReader_Load(object sender, EventArgs e)
        {

        }
        private void LogTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogFileReader_Load_1(object sender, EventArgs e)
        {
            LogTextBox.Text = Logger.GetLogText();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LogTextBox.Text = "";
            Logger.Close();
            File.WriteAllText("log.txt", "");
        }
    }
}
