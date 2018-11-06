using System;
using System.Diagnostics;

namespace BlenderTrial
{
    public class BlenderRenderProcess
    {
        private static string blenderExePath = AppGlobal.BlenderExePath;
        // add quotes around the argument values in case the values contain space
        private const string blenderCmdArgumentsFormat =
            "--background {0} --python {1} -- " +
            "--texture=\"{2}\" " +
            "--render=\"{3}\" " +
            "--startframe={4} " +
            "--endframe={5} " +
            "--allowsinglecore={6} " +  // 0 (no) or 1 (yes)
            "--reservedcores={7} " +
            "--xresolution={8} " +
            "--yresolution={9} " + 
            "--framerate={10}";
        private static string blenderProcessWorkDir = AppGlobal.WorkDirOfBlenderProcess;


        public static int StartBlenderProcess(string blendFilePath, string pythonScriptPath,
            string textureFilePath, string renderOutputPathWithoutExtension,
            int startFrame, int endFrame, int isAllowSingleCore, int numOfReservedCores,
            int xResolution, int yResolution, int fps)
        {
            int exitCode;
            string blenderCmdArguments = String.Format(blenderCmdArgumentsFormat,
                blendFilePath, pythonScriptPath, textureFilePath,
                renderOutputPathWithoutExtension, startFrame, endFrame,
                isAllowSingleCore, numOfReservedCores,
                xResolution, yResolution, fps);

            ProcessStartInfo startInfo = ProcessUtil.CreateProcessStartInfo(blenderExePath,
                blenderCmdArguments, blenderProcessWorkDir);

            //ProcessStartInfo startInfo = ProcessUtil.CreateProcessStartInfoUseShellExecute(
            //    blenderExePath, blenderCmdArguments, blenderProcessWorkDir);

            using (Process proc = Process.Start(startInfo))
            {
                // Can't print output from process if UseShellExecute == true
                ProcessUtil.OutputFromProcess(proc);

                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }          

            return exitCode;
        }
    }
}
