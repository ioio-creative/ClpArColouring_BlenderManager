using System;
using System.Configuration;

namespace BlenderTrial
{
    public class AppGlobal
    {
        public static int MinIntervalBetweenRenders { get; }
        public static int MaxRetryTime { get; }

        public static string InputFileDir { get; }
        public static string OutputFileDir { get; }

        public static string FindHomographicImageExePath { get; }
        public static string WorkDirOfFindHomographyProcess { get; }

        public static string WarpImageByMarkersExePath { get; }
        public static string WorkDirOfWarpImageByMarkersProcess { get; }
        public static string WarpImageByMarkersOutputFileDir { get; }

        public static string WarpedImageFileNameSuffix { get; }
        public static string BlenderExePath { get; }
        public static string WorkDirOfBlenderProcess { get; }
        public static string BlendFileName { get; }
        public static string BlenderPythonScriptName { get; }
        public static int BlenderSceneStartFrame { get; }
        public static int BlenderSceneEndFrame { get; }
        public static string BlenderOutVideoExtension { get; }
        public static int BlenderProcessIsAllowSingleCPU { get; }
        public static int BlenderProcessNumOfCPUsToReserve { get; }
        public static int BlenderOutVideoResolutionX { get; }
        public static int BlenderOutVideoResolutionY { get; }
        public static int BlenderOutVideoFps { get; }

        static AppGlobal()
        {
            MinIntervalBetweenRenders = Int32.Parse(
                ConfigurationManager.AppSettings["MinIntervalBetweenRenders"]);
            MaxRetryTime = Int32.Parse(
                ConfigurationManager.AppSettings["MaxRetryTime"]);

            InputFileDir =
                ConfigurationManager.AppSettings["InputFileDir"];
            OutputFileDir =
                ConfigurationManager.AppSettings["OutputFileDir"];

            FindHomographicImageExePath =
                ConfigurationManager.AppSettings["FindHomographicImageExePath"];
            WorkDirOfFindHomographyProcess =
                ConfigurationManager.AppSettings["WorkDirOfFindHomographyProcess"];

            WarpImageByMarkersExePath =
                ConfigurationManager.AppSettings["WarpImageByMarkersExePath"];
            WorkDirOfWarpImageByMarkersProcess =
                ConfigurationManager.AppSettings["WorkDirOfWarpImageByMarkersProcess"];
            WarpImageByMarkersOutputFileDir =
                ConfigurationManager.AppSettings["WarpImageByMarkersOutputFileDir"];

            WarpedImageFileNameSuffix =
                ConfigurationManager.AppSettings["WarpedImageFileNameSuffix"];
            BlenderExePath =
                ConfigurationManager.AppSettings["BlenderExePath"];
            WorkDirOfBlenderProcess =
                ConfigurationManager.AppSettings["WorkDirOfBlenderProcess"];
            BlendFileName =
                ConfigurationManager.AppSettings["BlendFileName"];
            BlenderPythonScriptName =
                ConfigurationManager.AppSettings["BlenderPythonScriptName"];
            BlenderSceneStartFrame =
                Int32.Parse(ConfigurationManager.AppSettings["BlenderSceneStartFrame"]);
            BlenderSceneEndFrame =
                Int32.Parse(ConfigurationManager.AppSettings["BlenderSceneEndFrame"]);
            BlenderOutVideoExtension =
                ConfigurationManager.AppSettings["BlenderOutVideoExtension"];
            BlenderProcessIsAllowSingleCPU =
                Int32.Parse(ConfigurationManager.AppSettings["BlenderProcessIsAllowSingleCPU"]);
            BlenderProcessNumOfCPUsToReserve =
                Int32.Parse(ConfigurationManager.AppSettings["BlenderProcessNumOfCPUsToReserve"]);
            BlenderOutVideoResolutionX =
                Int32.Parse(ConfigurationManager.AppSettings["BlenderOutVideoResolutionX"]);
            BlenderOutVideoResolutionY =
                Int32.Parse(ConfigurationManager.AppSettings["BlenderOutVideoResolutionY"]);
            BlenderOutVideoFps =
                Int32.Parse(ConfigurationManager.AppSettings["BlenderOutVideoFps"]);
        }
    }
}
