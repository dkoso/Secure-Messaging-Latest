using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialGUI
{
    class Serial
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool EnableLED { get; set; } = true;

        public static void SendOff(object sertext)
        {
            if (EnableLED)
            {
                var serialPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
                serialPort.ReadTimeout = 6000;
                serialPort.DtrEnable = true;

                try
                {
                    serialPort.Open();
                    Console.WriteLine("Serial port is " + serialPort.IsOpen);
                    var line1 = serialPort.ReadLine();
                    Console.WriteLine(line1);
                    serialPort.Write((string)sertext);
                    var line2 = serialPort.ReadLine();
                    Console.WriteLine(line2);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(e);
                }
                finally
                {
                    serialPort.Close();
                    Console.WriteLine("Reached Finally");
                }
                Console.WriteLine("Serial port is " + serialPort.IsOpen);
                serialPort.Dispose();
            }
        }
    }
}
