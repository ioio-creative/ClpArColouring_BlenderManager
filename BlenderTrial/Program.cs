using FileHelperDLL;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BlenderTrial
{
    public class Program
    {
        private static void Main(string[] args)
        {
            int minIntervalBetweenRendersInMillis = AppGlobal.MinIntervalBetweenRenders * 1000;
            int numOfTrialsLeft = AppGlobal.MaxRetryTime;
            string nameOfLastProcessedFile = "";

            while (true)
            {               
                try
                {
                    FileInfo inputImgFile = FileHelper.GetFileWithEarliestLastWriteTimeInDirectoryByExtensions(
                        AppGlobal.InputFileDir, ".jpg", ".jpeg", ".png");

                    if (inputImgFile != null)
                    {
                        if (inputImgFile.Name == nameOfLastProcessedFile)
                        {
                            numOfTrialsLeft--;
                        }
                        else  // normal case
                        {
                            numOfTrialsLeft = AppGlobal.MaxRetryTime;
                        }

                        if (numOfTrialsLeft <= 0)  // no more trials left
                        {
                            // output empty file to output folder
                            string outVideoFileFullPath = OutputVideoFileFullPath(inputImgFile, true);
                            FileHelper.CreateEmptyFile(outVideoFileFullPath);

                            // delete input file
                            FileHelper.DeleteFileSafe(inputImgFile.FullName);
                            Log("Max retry time of " + AppGlobal.MaxRetryTime.ToString() + " reached." + Environment.NewLine +
                                "Input file deleted." + Environment.NewLine +
                                "No more retries will be done." + Environment.NewLine);

                            // reset numOfTrialsLeft
                            numOfTrialsLeft = AppGlobal.MaxRetryTime;
                        }
                        else  // normal case
                        {
                            nameOfLastProcessedFile = inputImgFile.Name;

                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();

                            ProcessImageToVideo(inputImgFile);

                            // delete input file if no exception raised in ProcessImageToVideo()
                            FileHelper.DeleteFileSafe(inputImgFile.FullName);

                            stopWatch.Stop();

                            // Get the elapsed time as a TimeSpan value.
                            TimeSpan ts = stopWatch.Elapsed;

                            // Format and display the TimeSpan value.
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts.Hours, ts.Minutes, ts.Seconds,
                                ts.Milliseconds / 10);
                            Log("Video render time: " + elapsedTime);
                        }
                    }
                    else
                    {
                        Log("No files in input file folder.");
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }

                Thread.Sleep(minIntervalBetweenRendersInMillis);
            }

            //Console.Read();
        }


        /* image-to-video process */

        private static void ProcessImageToVideo(FileInfo inputImgFile)
        {
            int exitCode;


            /* FindHomographicImageProcess */

            //exitCode = FindHomographicImageProcess.StartFindHomographicImageProcess(
            //    "duck1.jpg",
            //    "duck_photo1.jpg");

            //// exitCode == 3 means pattern in ref. image cannot be found in input image
            //if (exitCode == 3)
            //{
            //    Log("Pattern cannot be found in input image.");
            //}
            //else if (exitCode != 0)
            //{
            //    throw new Exception("Error when finding homographic image." + Environment.NewLine +
            //        "Error code: " + exitCode);
            //}

            /* end of FindHomographicImageProcess */


            /* WarpImageByMarkersProcess */

            //// Path.GetFullPath() will make all the file separators changed to Windows default "\\"
            //string outputWarpedImageFileFullPath = Path.Combine(
            //    Path.GetFullPath(AppGlobal.WarpImageByMarkersOutputFileDir),
            //    inputFileNameWithoutExtNorPath + "_warped.jpg");

            //exitCode = WarpImageByMarkersProcess.StartWarpImageByMarkersProcess(
            //    inputImgFile.FullName, outputWarpedImageFileFullPath);

            //// Create an empty file to signify error in warping image process to web server program
            //if (exitCode != 0)
            //{
            //    string emptyFileFullPath = Path.Combine(
            //        Path.GetFullPath(AppGlobal.WarpImageByMarkersOutputFileDir),
            //        inputFileNameWithoutExtNorPath + "_" + exitCode.ToString() + ".txt");
            //    FileHelper.CreateEmptyFile(emptyFileFullPath);
            //}

            /* end of WarpImageByMarkersProcess */


            //if (exitCode != 0)
            //{
            //    return;
            //}


            /* BlenderRenderProcess */

            string outVideoFileFullPathWithoutExt = OutputVideoFileFullPath(inputImgFile, false);

            exitCode = BlenderRenderProcess.StartBlenderProcess(
                AppGlobal.BlendFileName,
                AppGlobal.BlenderPythonScriptName,
                inputImgFile.FullName,
                outVideoFileFullPathWithoutExt, //"//Output\\orbit",
                AppGlobal.BlenderSceneStartFrame, AppGlobal.BlenderSceneEndFrame,
                AppGlobal.BlenderProcessIsAllowSingleCPU,
                AppGlobal.BlenderProcessNumOfCPUsToReserve,
                AppGlobal.BlenderOutVideoResolutionX, AppGlobal.BlenderOutVideoResolutionY, 
                AppGlobal.BlenderOutVideoFps);

            if (exitCode != 0)
            {
                throw new Exception("Error when running Blender.");
            }

            string outVideoFileFullPathWithExt = OutputVideoFileFullPath(inputImgFile, true);
            if (!File.Exists(outVideoFileFullPathWithExt) || FileHelper.IsFileSizeZero(outVideoFileFullPathWithExt))
            {
                throw new Exception("Error when running Blender.");
            }

            /* end of BlenderRenderProcess */
        }

        public static string OutputVideoFileFullPath(FileInfo inputImgFile, bool isIncludeExt)
        {
            string inputFileNameWithoutExtNorPath = Path.GetFileNameWithoutExtension(inputImgFile.Name);

            // Path.GetFullPath() will make all the file separators changed to Windows default "\\"
            // https://stackoverflow.com/questions/4804990/c-sharp-getting-file-names-without-extensions
            string outputVideoFileFullPath = Path.Combine(
                Path.GetFullPath(AppGlobal.OutputFileDir),
                inputFileNameWithoutExtNorPath.Substring(0, inputFileNameWithoutExtNorPath.Length - AppGlobal.WarpedImageFileNameSuffix.Length));  // cut WarpedImageFileNameSuffix from file name

            return isIncludeExt ? outputVideoFileFullPath + AppGlobal.BlenderOutVideoExtension : outputVideoFileFullPath;
        }


        /* end of image-to-video process */


        /* log & error handling */

        public static void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        private static void HandleException(Exception ex)
        {
            Log("Exception: " + ex);
        }

        /* end of log & error handling */
    }
}