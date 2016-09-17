// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-16</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName.Helpers
{
    using System;
    using System.Diagnostics;
    using Microsoft.VisualStudio.Shell.Interop;

    internal static class Logger
    {
        private static IVsOutputWindowPane _pane;
        private static IServiceProvider _provider;
        private static string _name;

        public static void Initialize(IServiceProvider provider, string name)
        {
            _provider = provider;
            _name = name;
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
                    _pane.OutputString(DateTime.Now + ": " + message + Environment.NewLine);
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
            if (_pane == null)
            {
                Guid guid = Guid.NewGuid();
                var output = (IVsOutputWindow)_provider.GetService(typeof(SVsOutputWindow));
                output.CreatePane(ref guid, _name, fInitVisible: 1, fClearWithSolution: 1);
                output.GetPane(ref guid, out _pane);
            }

            return _pane != null;
        }
    }
}
