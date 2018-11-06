using System;
using System.Diagnostics;

namespace BlenderTrial
{
    public class ProcessUtil
    {
        public static void OutputFromProcess(Process proc)
        {
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (!String.IsNullOrEmpty(line))
                {
                    //Program.Log(line.Substring(0, line.Length - 1));  // remove trailing "\n"
                    Program.Log(line);
                }
            }
        }

        public static ProcessStartInfo CreateProcessStartInfo(string exeFileName,
            string cmdArguments, string workingDirectory)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = exeFileName,
                Arguments = cmdArguments,
                WorkingDirectory = workingDirectory,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = false
            };
            return startInfo;
        }

        // https://stackoverflow.com/questions/5255086/when-do-we-need-to-set-useshellexecute-to-true
        public static ProcessStartInfo CreateProcessStartInfoUseShellExecute(string exeFileName,
            string cmdArguments, string workingDirectory)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = exeFileName,
                Arguments = cmdArguments,
                WorkingDirectory = workingDirectory,
                CreateNoWindow = false,
                UseShellExecute = true,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false
            };

            //startInfo.UserName = "IOIO";
            //string pw = "askmeioio";
            //foreach (char c in pw)
            //{
            //    startInfo.Password.AppendChar(c);
            //}

            return startInfo;
        }
    }
}
