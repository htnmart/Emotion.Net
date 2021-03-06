﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace htn.ML
{
    public class PyBridge
    {
        private Process Bridge;
        private SemaphoreSlim BridgeSemaphore = new SemaphoreSlim(1, 1);
        private SemaphoreSlim StateSemaphore = new SemaphoreSlim(0, 1);
        private string StateData = "";
        public PyBridge()
        {
            Bridge = new Process();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Bridge.StartInfo = new ProcessStartInfo()
                {
                    FileName = "python",
                    Arguments = Path.Join(Environment.CurrentDirectory, @"ML/ML.py"),
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WorkingDirectory = Environment.CurrentDirectory
                };
            }
            else
            {
                Bridge.StartInfo = new ProcessStartInfo()
                {
                    FileName = "python3",
                    Arguments = Path.Join(Environment.CurrentDirectory, @"ML/ML.py"),
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WorkingDirectory = Environment.CurrentDirectory
                };
            }
            Bridge.Start();
            Bridge.StandardInput.AutoFlush = true;
            Task.Run(async () =>
            {
                while (!Bridge.HasExited)
                {
                    string data = await Bridge.StandardOutput.ReadLineAsync();
                    if (!data.StartsWith("[nltk_data]"))
                    {
                        StateData = data;
                        StateSemaphore.Release();
                    }
                }
            });
        }

        ~PyBridge()
        {
            Bridge?.Kill();
        }

        public float GetScore(string input)
        {
            try
            {
                BridgeSemaphore.Wait();
                Bridge.StandardInput.WriteLine(input);
                StateSemaphore.Wait();
                return float.Parse(StateData);
            }
            finally
            {
                BridgeSemaphore.Release();
            }
        }
    }
}