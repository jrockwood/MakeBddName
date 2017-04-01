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
    using Microsoft.VisualStudio.Shell.Interop;

    internal static class Logger
    {
        private static IVsOutputWindowPane s_pane;
        private static IServiceProvider s_provider;
        private static string s_name;

        public static void Initialize(IServiceProvider provider, string name)
        {
            s_provider = provider;
            s_name = name;
        }

        [Conditional("DEBUG")]
        public static void LogDebug(string message)
        {
            Log(message);
        }

        public static void Log(string message)
        {
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
            if (ex != null)
            {
                Log(ex.ToString());
            }
        }

        private static bool EnsurePane()
        {
            if (s_pane == null)
            {
                var guid = Guid.NewGuid();
                var output = (IVsOutputWindow)s_provider.GetService(typeof(SVsOutputWindow));
                output.CreatePane(ref guid, s_name, fInitVisible: 1, fClearWithSolution: 1);
                output.GetPane(ref guid, out s_pane);
            }

            return s_pane != null;
        }
    }
}
