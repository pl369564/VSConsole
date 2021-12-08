using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static string PwFile
        {
            get { return Directory.GetCurrentDirectory() + "/Pw"; }
        }

        static string tempstr;
        static bool isNotEnd = true;
        static void Main(string[] args)
        {
            WLine(PwFile);
            if (!File.Exists(PwFile))
                File.CreateText(PwFile);
            while (isNotEnd) {
                RLine();
                if (File.Exists(tempstr))
                {
                    DealFile();
                }
                else {
                    DealCmd();
                }
            }
            WLine("按回车退出");
            RLine();
        }

      

        private static void DealFile()
        {
            if (UnRarOrZip(tempstr, false))
                return;
            string[]passwords =  File.ReadAllLines(PwFile);
            foreach (var password in passwords)
            {
                if (UnRarOrZip(tempstr, false, password))
                    break;
            }
            isNotEnd = false;
        }

        private static void DealCmd()
        {
            string[] cmd = tempstr.Split(" ");

            switch (cmd[0].ToLower())
            {
                case "add":
                    AddPw(cmd[1]);
                    break;
                case "remove":
                    RemovePw(cmd[1]);
                    break;
                case "end":
                    isNotEnd = false;
                    break;
                default:
                    WLine("格式错误");
                    break;
                /*
                case "":
                    break;
                */
            }
        }

        private static void AddPw(string pw)
        {
            string[] passwords = File.ReadAllLines(PwFile);
            var list = new List<string>(passwords);
            if (!list.Contains(pw))
            {
                list.Add(pw);
                File.WriteAllLines(PwFile,list.ToArray());
                WLine("Success");
            }
            else 
            {
                WLine("Already Add");
            }
        }

        private static void RemovePw(string pw)
        {
            string[] passwords = File.ReadAllLines(PwFile);
            var list = new List<string>(passwords);
            if (list.Contains(pw))
            {
                list.Remove(pw);
                File.WriteAllLines(PwFile, list.ToArray());
                WLine("Success");
            }
            else
            {
                WLine("Already Remove");
            }
        }

        private static void RLine() {
            tempstr = Console.ReadLine();
        }

        private static void WLine(string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// 解压RAR和ZIP文件(需存在Winrar.exe(只要自己电脑上可以解压或压缩文件就存在Winrar.exe))
        /// </summary>
        /// <param name="UnPath">解压后文件保存目录</param>
        /// <param name="rarPathName">待解压文件存放绝对路径（包括文件名称）</param>
        /// <param name="IsCover">所解压的文件是否会覆盖已存在的文件(如果不覆盖,所解压出的文件和已存在的相同名称文件不会共同存在,只保留原已存在文件)</param>
        /// <param name="PassWord">解压密码(如果不需要密码则为空)</param>
        /// <returns>true(解压成功);false(解压失败)</returns>
        public static bool UnRarOrZip( string rarPathName, bool IsCover, string PassWord = "")
        {
            string UnPath = Path.GetFileNameWithoutExtension(rarPathName);
            UnPath = Path.GetDirectoryName(rarPathName) + "/" + UnPath ;
            if (!Directory.Exists(UnPath))
                Directory.CreateDirectory(UnPath);

            Process Process1 = new Process();
            Process1.StartInfo.FileName = "C:/Program Files/WinRAR/Winrar.exe";
            string cmd = "";
            if (!string.IsNullOrEmpty(PassWord) && IsCover)
                //解压加密文件且覆盖已存在文件( -p密码 )
                cmd = string.Format(" x -p{0} -o+ {1} {2} -y", PassWord, rarPathName, UnPath);
            else if (!string.IsNullOrEmpty(PassWord) && !IsCover)
                //解压加密文件且不覆盖已存在文件( -p密码 )
                cmd = string.Format(" x -p{0} -o- {1} {2} -y", PassWord, rarPathName, UnPath);
            else if (IsCover)
                //覆盖命令( x -o+ 代表覆盖已存在的文件)
                cmd = string.Format(" x -o+ {0} {1} -y  -p-", rarPathName, UnPath);
            else
                //不覆盖命令( x -o- 代表不覆盖已存在的文件)
                cmd = string.Format(" x -o- {0} {1} -y  -p-", rarPathName, UnPath);
            //命令
            Process1.StartInfo.Arguments = cmd;
            Process1.StartInfo.CreateNoWindow = true;
            Process1.Start();
            Process1.WaitForExit();//无限期等待进程 winrar.exe 退出
                                   //Process1.ExitCode==0指正常执行，Process1.ExitCode==1则指不正常执行
            if (Process1.ExitCode == 0)
            {
                Process1.Close();
                return true;
            }
            else
            {
                Process1.Close();
                return false;
            }

        }
    }
}
