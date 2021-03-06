// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MakeBddNameCommand.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-16</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft;
    using Microsoft.VisualStudio.Shell;
    using Task = System.Threading.Tasks.Task;

    /// <summary>
    /// Represents the "Make BDD Name" menu command.
    /// </summary>
    internal sealed class MakeBddNameCommand
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly Func<ITextSelection> _getTextSelectionFunc;
        private readonly Func<IOptions> _getOptionsFunc;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private MakeBddNameCommand(
            IMenuCommandService menuCommandService,
            Func<ITextSelection> getTextSelectionFunc,
            Func<IOptions> getOptionsFunc)
        {
            RegisterCommand(menuCommandService ?? throw new ArgumentNullException(nameof(menuCommandService)));
            _getTextSelectionFunc = getTextSelectionFunc ?? throw new ArgumentNullException(nameof(getTextSelectionFunc));
            _getOptionsFunc = getOptionsFunc ?? throw new ArgumentNullException(nameof(getOptionsFunc));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static MakeBddNameCommand Instance { get; private set; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Initializes the singleton instance of the command by registering the command with the
        /// Visual Studio environment.
        /// </summary>
        public static async Task InitializeAsync(
            AsyncPackage package,
            Func<ITextSelection> getTextSelectionFunc,
            Func<IOptions> getOptionsFunc)
        {
            // Switch to the main thread - the call to AddCommand in MakeBddNameCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var menuCommandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Assumes.Present(menuCommandService);
            Instance = new MakeBddNameCommand(menuCommandService, getTextSelectionFunc, getOptionsFunc);
        }

        /// <summary>
        /// Replaces the selection with the user-specified BDD naming style. Assumes that there is a
        /// valid selection (<see cref="TextSelectionExtensions.ExtendSelectionToFullString"/> has
        /// been called already).
        /// </summary>
        internal static void RenameSelection(ITextSelection selection, IOptions options)
        {
            BddNameStyle namingStyle = options.NamingStyle;

            // Rename the selection
            string bddName;
            switch (namingStyle)
            {
                case BddNameStyle.UnderscorePreserveCase:
                    bddName = BddNamer.ToUnderscoreName(selection.Text, makeSentence: false);
                    break;

                case BddNameStyle.UnderscoreSentenceCase:
                    bddName = BddNamer.ToUnderscoreName(selection.Text, makeSentence: true);
                    break;

                case BddNameStyle.PascalCase:
                    bddName = BddNamer.ToPascalCase(selection.Text);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            selection.Insert(
                bddName,
                vsInsertFlags.vsInsertFlagsContainNewText | vsInsertFlags.vsInsertFlagsCollapseToEnd);
            selection.Collapse();
        }

        private void RegisterCommand(IMenuCommandService menuCommandService)
        {
            var menuCommandId = new CommandID(PackageGuids.MakeBddNameCmdSetGuid, PackageIds.MakeBddNameCommandId);
            var menuItem = new MenuCommand(OnMakeBddNameCommandClick, menuCommandId);
            menuCommandService.AddCommand(menuItem);
        }

        private void OnMakeBddNameCommandClick(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Logger.LogDebug($"Inside {GetType().FullName}.{nameof(OnMakeBddNameCommandClick)}");

            // Select the appropriate word/sentence.
            ITextSelection selection = _getTextSelectionFunc();
            selection.ExtendSelectionToFullString();

            // Rename the selection.
            IOptions options = _getOptionsFunc();
            RenameSelection(selection, options);
        }
    }
}
