using System;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading;

namespace SerialGUI
{
    class IMAP
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string server;
        private int port;

        private static TcpClient tcpc = null;
        private static SslStream ssl = null;

        public string username { private get; set; }
        public string password { private get; set; }

        public IMAP(string server, int port)
        {
            this.server = server;
            this.port = port;
        }

        public void imapSeen()
        {
            try
            {
                tcpc = new TcpClient(server, port);
                ssl = new SslStream(tcpc.GetStream());
                ssl.AuthenticateAsClient(server);
                receiveResponse("");

                receiveResponse("$ LOGIN " + username + " " + password + "\r\n");

                receiveResponse("$ SELECT INBOX\r\n");

                receiveResponse("$ STORE 1:* +FLAGS \\Seen\r\n");

                receiveResponse("$ SELECT News\r\n");

                receiveResponse("$ STORE 1:* +FLAGS \\Seen\r\n");

                receiveResponse("$ LOGOUT\r\n");
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            finally
            {
                if (ssl != null)
                {
                    ssl.Close();
                    ssl.Dispose();
                }
                if (tcpc != null)
                {
                    tcpc.Close();
                }
            }

            
        }
        private void receiveResponse(string command)
        {
            try
            {
                if (command != "")
                {
                    if (tcpc.Connected)
                    {
                        byte[] dummy = Encoding.ASCII.GetBytes(command);
                        ssl.Write(dummy, 0, dummy.Length);
                    }
                    else
                    {
                        throw new ApplicationException("TCP CONNECTION DISCONNECTED");
                    }
                }
                ssl.Flush();

                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[2048];
                int bytes = ssl.Read(buffer, 0, 2048);
                sb.Append(Encoding.ASCII.GetString(buffer));


                Console.WriteLine(sb.ToString());

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

    }
}