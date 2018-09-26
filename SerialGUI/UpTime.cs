using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SerialGUI
{
    public class UpTime
    {
        private DateTime startTime = DateTime.Now;
        private int hourvalue = 0;

        private Form1 form1;

        public UpTime(Form1 form1)
        {
            this.form1 = form1;
        }

        public void Time()
        {
            while (true)
            {
                var delta = DateTime.Now - startTime;
                var minvalue = Math.Floor(delta.TotalMinutes).ToString("n0");
                int intminvalue = int.Parse(minvalue);

                if (intminvalue < 10)
                {
                    minvalue = minvalue.PadLeft(2, '0');
                }

                if (minvalue == "60")
                {
                    startTime = DateTime.Now;
                    minvalue = "00";
                    hourvalue++;
                }

                var value = "Uptime  " + (hourvalue.ToString()) + ":" + minvalue;
                form1.UpTime(value);
                Thread.Sleep(1000);
            }
        }
    }
}
