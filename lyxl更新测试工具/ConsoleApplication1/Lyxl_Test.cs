using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Lyxl_Test
    {
       string oldpath = "D:/PYQP/安装包打包/Lyxl";
       string newpath = "D:/PYQP/客户端";
       string clientpath = "D:/PYQP/Client";

        public void StartTest() {

            Console.WriteLine("打包目录:"+oldpath);
            Console.WriteLine("测试文件地址:"+newpath);
            Console.WriteLine("SVN Client文件地址:"+clientpath);

            if (!SayYes("开始测试?"))
                return;

            if (Directory.Exists(newpath))
                Directory.Delete(newpath,true);
            CopyDirectory(oldpath, newpath);

            Console.WriteLine("测试文件复制完成");
            Console.ReadLine();

            if (!SayYes("测试成功?"))
                return;

            if (Directory.Exists(clientpath))
                Directory.Delete(clientpath, true);
            CopyDirectory(oldpath, clientpath);

            Console.WriteLine("测试文件复制至Client");
            Console.ReadLine();


        }

        private bool SayYes(string question) {
            Console.WriteLine(question);
            Console.WriteLine(BuildOptions.Development | BuildOptions.AllowDebugging);
            string str = Console.ReadLine();
            return str == "Y";
        }

        /// <summary>
        /// 复制文件用 
        /// ps:file无法直接操作文件夹,需要手动创建,再File.Copy复制文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        void CopyDirectory(string sourcePath, string destPath)
        {
            if (destPath[destPath.Length - 1] != Path.DirectorySeparatorChar)
                destPath += Path.DirectorySeparatorChar;

            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                if (!file.EndsWith(".meta"))
                    File.Copy(file, destPath + Path.GetFileName(file));
            }

            string[] dirs = Directory.GetDirectories(sourcePath);
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, destPath + Path.GetFileName(dir));
            }
        }
    }
    //
    // 摘要:
    //     ///
    //     Building options. Multiple options can be combined together.
    //     ///
    [Flags]
    public enum BuildOptions
    {
        //
        // 摘要:
        //     ///
        //     Perform the specified build without any special settings or extra tasks.
        //     ///
        None = 0,
        StripDebugSymbols = 0,
        CompressTextures = 0,
        //
        // 摘要:
        //     ///
        //     Force full optimizations for script complilation in Development builds.
        //     ///
        ForceOptimizeScriptCompilation = 0,
        //
        // 摘要:
        //     ///
        //     Build a development version of the player.
        //     ///
        Development = 1,
        //
        // 摘要:
        //     ///
        //     Run the built player.
        //     ///
        AutoRunPlayer = 4,
        //
        // 摘要:
        //     ///
        //     Show the built player.
        //     ///
        ShowBuiltPlayer = 8,
        //
        // 摘要:
        //     ///
        //     Build a compressed asset bundle that contains streamed scenes loadable with the
        //     WWW class.
        //     ///
        BuildAdditionalStreamedScenes = 16,
        //
        // 摘要:
        //     ///
        //     Used when building Xcode (iOS) or Eclipse (Android) projects.
        //     ///
        AcceptExternalModificationsToPlayer = 32,
        InstallInBuildFolder = 64,
        //
        // 摘要:
        //     ///
        //     Copy UnityObject.js alongside Web Player so it wouldn't have to be downloaded
        //     from internet.
        //     ///
        WebPlayerOfflineDeployment = 128,
        //
        // 摘要:
        //     ///
        //     Start the player with a connection to the profiler in the editor.
        //     ///
        ConnectWithProfiler = 256,
        //
        // 摘要:
        //     ///
        //     Allow script debuggers to attach to the player remotely.
        //     ///
        AllowDebugging = 512,
        //
        // 摘要:
        //     ///
        //     Symlink runtime libraries when generating iOS Xcode project. (Faster iteration
        //     time).
        //     ///
        SymlinkLibraries = 1024,
        //
        // 摘要:
        //     ///
        //     Don't compress the data when creating the asset bundle.
        //     ///
        UncompressedAssetBundle = 2048,
        //
        // 摘要:
        //     ///
        //     Sets the Player to connect to the Editor.
        //     ///
        ConnectToHost = 4096,
        //
        // 摘要:
        //     ///
        //     Build headless Linux standalone.
        //     ///
        EnableHeadlessMode = 16384,
        //
        // 摘要:
        //     ///
        //     Build only the scripts of a project.
        //     ///
        BuildScriptsOnly = 32768,
        Il2CPP = 65536,
        //
        // 摘要:
        //     ///
        //     Include assertions in the build. By default, the assertions are only included
        //     in development builds.
        //     ///
        ForceEnableAssertions = 131072,
        //
        // 摘要:
        //     ///
        //     Use chunk-based LZ4 compression when building the Player.
        //     ///
        CompressWithLz4 = 262144,
        //
        // 摘要:
        //     ///
        //     Use chunk-based LZ4 high-compression when building the Player.
        //     ///
        CompressWithLz4HC = 524288,
        ComputeCRC = 1048576,
        //
        // 摘要:
        //     ///
        //     Do not allow the build to succeed if any errors are reporting during it.
        //     ///
        StrictMode = 2097152
    }
}
