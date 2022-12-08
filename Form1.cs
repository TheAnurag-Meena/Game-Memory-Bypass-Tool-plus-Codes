using Memory;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("KERNEL32.DLL")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32First(IntPtr handle, ref ProcessEntry32 pe);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32Next(IntPtr handle, ref ProcessEntry32 pe);
        private string HostsPath = @"C:\Windows\System32\drivers\etc\hosts";
        public string userName = Environment.UserName;
        public string GagaPath = null;
        private bool HostPaste = false;
        private async Task PutTaskDelay(int Time)
        {
            await Task.Delay(Time);
        }
        private async Task<IntPtr> method_0()
        {
            var intPtr = IntPtr.Zero;
            uint num = 0U;
            var intPtr2 = CreateToolhelp32Snapshot(2U, 0U);
            if ((int)intPtr2 > 0)
            {
                ProcessEntry32 processEntry = default;
                processEntry.dwSize = (uint)Marshal.SizeOf(processEntry);
                int num2 = Process32First(intPtr2, ref processEntry);
                while (num2 == 1)
                {
                    var intPtr3 = Marshal.AllocHGlobal((int)processEntry.dwSize);
                    Marshal.StructureToPtr(processEntry, intPtr3, true);
                    ProcessEntry32 processEntry2 = (ProcessEntry32)Marshal.PtrToStructure(intPtr3, typeof(ProcessEntry32));
                    Marshal.FreeHGlobal(intPtr3);
                    // AndroidProcess
                    if (processEntry2.szExeFile.Contains("AndroidProcess") && processEntry2.cntThreads > num)
                    {
                        num = processEntry2.cntThreads;
                        intPtr = (IntPtr)(long)(ulong)processEntry2.th32ProcessID;
                    }

                    num2 = Process32Next(intPtr2, ref processEntry);
                }
                label4.Text = Convert.ToString(intPtr);
                await PutTaskDelay(1000);
                Bypass();
            }

            return intPtr;
        }

        private string sr;

        public async void XtremeService()
        {
            string TempPath = @"C:\Users\" + userName + @"\AppData\Local\Temp\";
            string validchars = "abcdefghijklmnopqrstuvwxyz";
            var sb = new StringBuilder();
            var rand = new Random();
            for (int i = 1; i <= 10; i++)
            {
                int idx = rand.Next(0, validchars.Length);
                char randomChar = validchars[idx];
                sb.Append(randomChar);
            }

            sr = sb.ToString();
            if (File.Exists(TempPath + sr + ".sys"))
            {
            }
            else
            {
                File.WriteAllBytes(TempPath + sr + ".sys", Properties.Resources.xtreme);
                var p1 = new Process();
                p1.StartInfo.FileName = "cmd.exe"; // jioUN0XmYd
                p1.StartInfo.Arguments = "/c sc create " + sr + @" binpath=" + TempPath + sr + ".sys start=demand type=filesys & net start " + sr; // & r
                p1.StartInfo.UseShellExecute = false;
                p1.StartInfo.CreateNoWindow = true;
                p1.Start();
                p1.WaitForExit();
            }

            x = 0;
            await method_0();
        }
        public long enumerable = new long();

        public async void Bypass()
        {
            bool k = false;
            int counter = 1;
            if (Convert.ToInt32(label4.Text) == 0)
            {
                label4.ForeColor = Color.Red;
                label4.Text = "SmartGaGa Not Running";
            }
            else
            {
                label4.ForeColor = Color.Green;
                MemLib.OpenProcess(Convert.ToInt32(label4.Text));

                var enumerable = await MemLib.AoBScan(1879048192L, 2415919104L, "F0 4F 2D E9 1C B0 8D E2 14 D0 4D E2 00 A0 A0 E1 0C 06 9F E5 01 80 A0 E1", false, false, "");
                string_0 = "0x" + enumerable.FirstOrDefault().ToString("X");
                Mem.MemoryProtection memoryProtection;
                MemLib.ChangeProtection(string_0, Mem.MemoryProtection.ReadWrite, out memoryProtection, "");
                foreach (long num in enumerable)
                {
                    MemLib.WriteMemory(num.ToString("X"), "bytes", "00 00 A0 E3 1E FF 2F E1 14 D0 4D E2 00 A0 A0 E1 0C 06 9F E5 01 80 A0 E1", "", null);
                    k = true;
                }

                if (k == true)
                {
                    label5.Text = "Bypassed";
                    label5.ForeColor = Color.Green;
                    await PutTaskDelay(500);
                    // Thread.Sleep(500)
                    if (k == true)
                    {
                        Interaction.Shell("cmd /c" + "sc stop " + sr, AppWinStyle.Hide, true, -1);
                        Interaction.Shell("cmd /c" + "sc delete " + sr, AppWinStyle.Hide, true, -1);
                        Interaction.Shell("cmd /c" + @"cd /d C:\Users\" + Environment.UserName + @"\AppData\Local\Temp", AppWinStyle.Hide, true, -1);
                        Interaction.Shell("cmd /c" + "del /f " + sr, AppWinStyle.Hide, true, -1);
                    }
                }
                else if (counter < 4)
                {
                    label5.ForeColor = Color.Aqua;
                    label5.Text = "Bypassing";
                    counter += 1;
                    await method_0();
                }
                else
                {
                    label5.Text = "Failed, Try Again";
                    label5.ForeColor = Color.Red;
                }

                Mem.MemoryProtection memoryProtection2;
                MemLib.ChangeProtection(string_0, Mem.MemoryProtection.ReadOnly, out memoryProtection2, "");
            }
        }


        private int x;
        public Mem MemLib = new Mem();
        private static string string_0;
        private IContainer icontainer_0;

        public struct ProcessEntry32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (HostPaste == false)
            {
                //File.WriteAllBytes(HostsPath, Properties.Resources.hosts);
            }
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                RedirectStandardInput = true,
                UseShellExecute = false
            };
            process.Start();

            using (StreamWriter standardInput = process.StandardInput)
            {
                if (standardInput.BaseStream.CanWrite)
                standardInput.WriteLine("adb kill-server");
                standardInput.WriteLine("adb start-server");
                standardInput.WriteLine("adb devices");
                standardInput.WriteLine("adb shell mkdir /data/data/com.tencent.tinput/");
                standardInput.WriteLine("adb shell mkdir /data/data/com.tencent.tinput/cache/");
                standardInput.WriteLine("adb shell am start -n com.tencent.ig/com.epicgames.ue4.SplashActivity");
                standardInput.WriteLine("adb shell sleep 7");
                standardInput.Flush();
                standardInput.Close();
                process.WaitForExit();
            }
            await PutTaskDelay(1000);
            XtremeService();
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            if (IsInstalled())
            {
                Process.Start(GagaPath);
            }
            if (!IsInstalled())
            {
                MessageBox.Show("Please install smartgaga correctly");
            }
        }
        private bool IsInstalled()
        {
            if (File.Exists(@"C:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"C:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"D:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"D:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"E:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"E:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"F:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"F:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"G:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"G:\Program Files (x86)\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"C:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"C:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"D:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"D:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"E:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"E:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"F:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"F:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }

            if (File.Exists(@"G:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe"))
            {
                GagaPath = @"G:\Program Files\SmartGaGa\ProjectTitan\Engine\ProjectTitan.exe";
                return true;
            }
            else
                return false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}