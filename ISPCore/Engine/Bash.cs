﻿using System;
using System.Diagnostics;

namespace ISPCore.Engine
{
    public class Bash
    {
        /// <summary>
        /// Выполнить Bash команду
        /// </summary>
        /// <param name="comand">Bash команда</param>
        public string Run(string comand)
        {
            try
            {
                var processInfo = new ProcessStartInfo();
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.FileName = "/bin/bash";
                processInfo.Arguments = $" -c \"{comand}\"";

                var process = Process.Start(processInfo);
                var outPut = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return outPut;
            }
            catch
            {
                return "";
            }
        }
    }
}
