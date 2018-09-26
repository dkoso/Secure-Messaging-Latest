using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialGUI
{
    public class ShowLog
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string FileName { get; set; }

        public ShowLog(string filename)
        {
            this.FileName = filename;
        }
        public void OpenLog()
        {
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = FileName;
            myProcess.StartInfo.CreateNoWindow = true;

            try
            {
                myProcess.Start();
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }
    }
}
