using System;
using System.Diagnostics;

namespace BlenderTrial
{
    public class WarpImageByMarkersProcess
    {
        private static string warpImageByMarkersExePath = AppGlobal.WarpImageByMarkersExePath;
        private const string warpImageByMarkersCmdArgumentsFormat =
            "\"{0}\" \"{1}\"";  // add quotes around the argument values in case the values contain space
        private static string warpImageByMarkersWorkDir = AppGlobal.WorkDirOfWarpImageByMarkersProcess;


        public static int StartWarpImageByMarkersProcess(string inImgFilePath,
            string outImgFilePath)            
        {
            int exitCode = 0;

            string warpImageByMarkersCmdArguments = String.Format(warpImageByMarkersCmdArgumentsFormat,
                inImgFilePath, outImgFilePath);
                        
            ProcessStartInfo startInfo = ProcessUtil.CreateProcessStartInfo(warpImageByMarkersExePath,
                warpImageByMarkersCmdArguments, warpImageByMarkersWorkDir);

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
