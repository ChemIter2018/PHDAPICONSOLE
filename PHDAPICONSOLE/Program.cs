using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniformance.PHD;
using System.Runtime.InteropServices;

namespace PHDAPICONSOLE
{
    class Program
    {
        static void Main()
        {
            PHDHistorian PHD = new PHDHistorian();
            PHDServer DefaultServer = new PHDServer("192.168.0.4");
            DefaultServer.APIVersion = SERVERVERSION.RAPI200;
            
            //DefaultServer.UserName = "Experionadmin";
            //DefaultServer.Password = "!password123";

            //PutData
            DefaultServer.WindowsUsername = "Experionadmin";
            DefaultServer.WindowsPassword = "!password123";

            PHD.DefaultServer = DefaultServer;
            PHD.DefaultServer.Port = 3150;

            string flag = "Read";

            if (flag == "Read")
            {
                Double[] timestamps = null;
                Double[] values = null;
                short[] confidences = null;

                Tag MyTag = new Tag("RTOR.TI1237.DACA.PV");

                //PHD.StartTime = "NOW-1H";
                //PHD.EndTime = "NOW";

                PHD.StartTime = "6/12/2020 9:00";
                PHD.EndTime = "6/12/2020 10:00";

                //PHD.Sampletype = SAMPLETYPE.Raw;
                PHD.Sampletype = SAMPLETYPE.Average;
                //PHD.SampleFrequency = 3600;
                //PHD.ReductionType = REDUCTIONTYPE.None;
                PHD.ReductionType = REDUCTIONTYPE.Average;
                PHD.ReductionFrequency = 300;               

                PHD.FetchData(MyTag, ref timestamps, ref values, ref confidences);

                int count = timestamps.GetUpperBound(0);
                Console.WriteLine("Retrieved {0} data points", count);
                Console.WriteLine();

                for (int i = 0; i < count; ++i)
                {
                    Console.WriteLine("{0},{1},{2},{3}",
                        i + 1,
                        DateTime.FromOADate(timestamps[i]),
                        values[i],
                        confidences[i]);
                }

                //Console.WriteLine("{0},{1},{2},{3}",
                //        count + 1,
                //        DateTime.FromOADate(timestamps[count]),
                //        values[count],
                //        confidences[count]);
            }
            else if(flag == "Write")
            {
                Tag MyTag = new Tag("RTOS.TEST.PV");
                float floatValue = 3;

                PHD.PutData(MyTag, floatValue);
            }
            
            PHD.Dispose();
        }
    }
}
