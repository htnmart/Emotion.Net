﻿using System;
using System.Diagnostics;
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
            Bridge.StartInfo = new ProcessStartInfo()
            {
                FileName = "python",
                Arguments = @"F:\encodeous\htn\Backend\htn\ML\ML.py",
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };
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
            if (string.IsNullOrEmpty(input)) return 0;
            StringBuilder sb = new StringBuilder();
            foreach (var c in input)
            {
                if (char.IsLetter(c) || char.IsDigit(c) || char.IsPunctuation(c) || char.IsSeparator(c))
                {
                    sb.Append(c);
                }
            }
            try
            {
                BridgeSemaphore.Wait();
                Bridge.StandardInput.WriteLine(sb);
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