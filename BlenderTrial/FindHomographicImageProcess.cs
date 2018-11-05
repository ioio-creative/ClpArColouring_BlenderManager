using System;
using System.Diagnostics;

namespace BlenderTrial
{
    public class FindHomographicImageProcess
    {
        private static string findHomographicImageExePath = AppGlobal.FindHomographicImageExePath;
        private const string findHomographicImageCmdArgumentsFormat =
            "\"{0}\" \"{1}\"";  // add quotes around the argument values in case the values contain space
        private static string findHomographicImageProcessWorkDir =
            AppGlobal.WorkDirOfFindHomographyProcess;


        public static int StartFindHomographicImageProcess(string refImgFilePath, 
            string inputImgFilePath)
        {
            int exitCode;
            string findHomographicImageCmdArguments = String.Format(findHomographicImageCmdArgumentsFormat,
                refImgFilePath, inputImgFilePath);

            ProcessStartInfo startInfo = ProcessUtil.CreateProcessStartInfo(findHomographicImageExePath,
                findHomographicImageCmdArguments, findHomographicImageProcessWorkDir);

            using (Process proc = Process.Start(startInfo))
            {
                ProcessUtil.OutputFromProcess(proc);

                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }
                       
            return exitCode;
        }
    }
}
