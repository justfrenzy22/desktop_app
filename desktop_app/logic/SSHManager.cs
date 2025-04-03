using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_app.logic
{
    public class SSHManager
    {
        public void connectMain()
        {
            string username = "justfrenzy";
            string server = "yans";
            string password = "password";

            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = info };
            process.Start();

            using (StreamWriter sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine($"plink -ssh {username}@{server} -pw {password}");
                }   
            }                                                                               
                                                                                                   
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            System.Console.WriteLine("Output: " + output);
            System.Console.WriteLine("Error: " + error);            

        }
    }
}
