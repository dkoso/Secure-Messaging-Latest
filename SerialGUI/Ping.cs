using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SerialGUI
{
    public class PingVM
    {
        public static string ip { get; set; }

        private int downcount = 0;

        public static bool enablePing { get; set; } = true;

        public delegate void PingEventHandler(object source, PingEventArgs e);

        public event PingEventHandler PingFailed;

        private Form1 form1;

        public PingVM(Form1 form1)
        {
            this.form1 = form1;
        }

        public void PingMe()
        {
            
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            while (true)
            {
                while (enablePing)
                {
                    // Use the default Ttl value which is 128,
                    // but change the fragmentation behavior.
                    options.DontFragment = true;

                    // Create a buffer of 32 bytes of data to be transmitted.
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;
                    PingReply reply = pingSender.Send(ip, timeout, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine(reply.Status);
                        downcount = 0;
                        form1.GoPing(true);
                        OnPingFailed(true);
                    }
                    else
                    {
                        Console.Write(reply.Status);
                        Console.WriteLine(" - " + downcount);
                        downcount++;
                        if (downcount == 4)
                        {
                            downcount = 0;
                            form1.GoPing(false);
                            OnPingFailed(false);
                        }
                    }
                    Thread.Sleep(10000);
                }
                Thread.Sleep(100);
            }
        }

        protected virtual void OnPingFailed(bool value)
        {
            if (PingFailed != null)
            {
                PingFailed(this, new PingEventArgs() { pingValue = value } );
            }
        }
    }
}
    

