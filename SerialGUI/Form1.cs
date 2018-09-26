using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Configuration;
using System.Media;

namespace SerialGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => Serial.SendOff("off"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void cornerButton_Click(object sender, EventArgs e)
        {
            var screenSize = Screen.FromControl(this).Bounds.Width;
            var point = new Point(screenSize - 1057 + 10, 0);
            Form1.ActiveForm.DesktopLocation = point;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TCP Listener (Server)
            MyTcpListener myTcpListener = new MyTcpListener(this);

            Task.Run(() => myTcpListener.RunServer());

            // Ping + Color Status
            PingVM pingVM = new PingVM(this);

            PingVM.enablePing = true;
            PingVM.ip = "192.168.2.55";

            Task.Run(() => pingVM.PingMe());

            //Uptime
            UpTime upTime = new UpTime(this);

            Task.Run(() => upTime.Time());

            // No Taskbar
            this.ShowInTaskbar = false;

            // Subscribe to Ping Fail Event
            var pingFailedIndicator = new PingFailedIndicator();

            pingVM.PingFailed += pingFailedIndicator.OnPingFailed;

        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }

            richTextBox1.SelectionColor = Color.LimeGreen;
            richTextBox1.AppendText(value + Environment.NewLine);
        }

        public void TriggerBeep(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(TriggerBeep), new object[] {value});
                return;
            }

            NewMessage(value);
        }

        public void NewMessage(string servdata)
        {
            Task.Run(() => Serial.SendOff("beep" + servdata));
            Console.WriteLine("beep" + servdata);

            SystemSounds.Asterisk.Play();
        }

        public void GoPing(bool value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(GoPing), new object[] { value });
                return;
            }

            if (value == true)
            {
                colorStatusBox.BackColor = Color.LimeGreen;
            }
            else if (value == false)
            {
                colorStatusBox.BackColor = Color.Red;

                Task.Run(() => Serial.SendOff("beep" + "Messaging Down!"));

                richTextBox1.SelectionColor = Color.LimeGreen;
                richTextBox1.AppendText("IP: " + PingVM.ip + " is down!" + Environment.NewLine);
            }

        }

        public void UpTime(string value)
        {

            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpTime), new object[] { value });
                return;
            }
            upTimeBox.Text = value;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LogOpener();
        }

        private void UpdateFormRead(string name)
        {
            richTextBox1.SelectionColor = Color.Aqua;
            richTextBox1.AppendText(name + Environment.NewLine);
            comboBox1.Text = "Mark Read";
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 settings = new Form2(this);
            settings.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KosoData v1.1");
        }

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogOpener();
        }

        private void LogOpener()
        {
            var path = ($"{Application.StartupPath}\\myapp.log");
            ShowLog showLog = new ShowLog(path);
            showLog.OpenLog();
        }

    }
}
