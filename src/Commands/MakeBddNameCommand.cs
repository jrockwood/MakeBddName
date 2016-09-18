// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MakeBddNameCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-16</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName.Commands
{
    using System;
    using System.ComponentModel.Design;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// Represents the "Make BDD Name" menu command.
    /// </summary>
    internal sealed class MakeBddNameCommand
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly Package _package;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private MakeBddNameCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            _package = package;

            RegisterCommand();
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static MakeBddNameCommand Instance { get; private set; }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider => _package;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new MakeBddNameCommand(package);
        }

        private void RegisterCommand()
        {
            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandId = new CommandID(PackageGuids.guidMakeBddNameCmdSet, PackageIds.cmdMakeBddName);
                var menuItem = new MenuCommand(OnMakeBddNameCommandClick, menuCommandId);
                commandService.AddCommand(menuItem);
            }
        }

        private void OnMakeBddNameCommandClick(object sender, EventArgs e)
        {
            string message = $"Inside {GetType().FullName}.{nameof(OnMakeBddNameCommandClick)}";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                ServiceProvider,
                message,
                "MakeBddNameCommand",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
