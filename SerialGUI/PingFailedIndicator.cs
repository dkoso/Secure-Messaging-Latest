using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialGUI
{
    public class PingFailedIndicator
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void OnPingFailed(object source, PingEventArgs e)
        {
            if (!e.pingValue)
            {
                Console.WriteLine("Ping Failed! " + e.pingValue);

                AlertEmail();

                log.Info($"Email Message {PingVM.ip} down!");
            }
        }
}
