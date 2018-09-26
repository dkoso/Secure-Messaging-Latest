using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialGUI
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CurrentIp();
            CurrentColor();
            CurrentPingCheckBox();
            CurrentLEDCheckBox();

            // No Taskbar
            this.ShowInTaskbar = false;
        }

        private void CurrentIp()
        {
            textBoxIP.Text = PingVM.ip;
        }

        private void CurrentColor()
        {
            Color original = form1.richTextBox1.BackColor;
            comboBoxColor.Text = original.Name;
        }

        private void CurrentPingCheckBox()
        {
            checkBox1.Checked = PingVM.enablePing;
        }

        private void CurrentLEDCheckBox()
        {
            checkBox2.Checked = Serial.EnableLED;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            PingVM.ip = textBoxIP.Text;
            Console.WriteLine(PingVM.ip);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelButton = cancelButton;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxColor.SelectedIndex)
            {
                case 0:
                    form1.richTextBox1.BackColor = Color.SlateGray;
                    break;
                case 1:
                    form1.richTextBox1.BackColor = Color.LightSlateGray;
                    break;
                case 2:
                    form1.richTextBox1.BackColor = Color.LightSteelBlue;
                    break;
                case 3:
                    form1.richTextBox1.BackColor = Color.LightCyan;
                    break;
                case 4:
                    form1.richTextBox1.BackColor = Color.Black;
                    break;
                case 5:
                    form1.richTextBox1.BackColor = Color.PaleTurquoise;
                    break;
                case 6:
                    form1.richTextBox1.BackColor = Color.Aquamarine;
                    break;
                case 7:
                    form1.richTextBox1.BackColor = Color.Azure;
                    break;
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                PingVM.enablePing = true;
                Console.WriteLine($"Ping is {PingVM.enablePing}");

            }
            else
            {
                PingVM.enablePing = false;
                Console.WriteLine($"Ping is {PingVM.enablePing}");
                form1.colorStatusBox.BackColor = Color.Gray;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Serial.EnableLED = true;
            }
            else
            {
                Serial.EnableLED = false;
            }

        }
    }
}
