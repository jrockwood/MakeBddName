// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-16</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System;
    using System.Diagnostics;
    using Microsoft;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    internal static class Logger
    {
        private static IVsOutputWindowPane s_pane;
        private static IVsOutputWindow s_outputWindowService;
        private static string s_name;

        public static void Initialize(IVsOutputWindow outputWindowService, string name)
        {
            Assumes.Present(outputWindowService);
            s_outputWindowService = outputWindowService;
            s_name = name;
        }

        [Conditional("DEBUG")]
        public static void LogDebug(string message)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Log(message);
        }

        public static void Log(string message)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            try
            {
                if (EnsurePane())
                {
                    s_pane.OutputString(DateTime.Now + ": " + message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void Log(Exception ex)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (ex != null)
            {
                Log(ex.ToString());
            }
        }

        private static bool EnsurePane()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (s_pane == null)
            {
                var guid = Guid.NewGuid();
                IVsOutputWindow output = s_outputWindowService;
                output.CreatePane(ref guid, s_name, fInitVisible: 1, fClearWithSolution: 1);
                output.GetPane(ref guid, out s_pane);
            }

            return s_pane != null;
        }
    }
}
