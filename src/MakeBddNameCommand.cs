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
    using Microsoft.VisualStudio.Shell;

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
            if (menuCommandService == null) { throw new ArgumentNullException(nameof(menuCommandService)); }
            if (getTextSelectionFunc == null) { throw new ArgumentNullException(nameof(getTextSelectionFunc)); }
            if (getOptionsFunc == null) { throw new ArgumentNullException(nameof(getOptionsFunc)); }

            RegisterCommand(menuCommandService);
            _getTextSelectionFunc = getTextSelectionFunc;
            _getOptionsFunc = getOptionsFunc;
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
        public static void Initialize(
            IMenuCommandService menuCommandService,
            Func<ITextSelection> getTextSelectionFunc,
            Func<IOptions> getOptionsFunc)
        {
            Instance = new MakeBddNameCommand(menuCommandService, getTextSelectionFunc, getOptionsFunc);
        }

        /// <summary>
        /// Replaces the selection with the user-specified BDD naming style. Assumes that there is a
        /// valid selection ( <see cref="TextSelectionExtensions.ExtendSelectionToFullString"/> has
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

            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            selection.Insert(
                bddName,
                vsInsertFlags.vsInsertFlagsContainNewText | vsInsertFlags.vsInsertFlagsCollapseToEnd);
            selection.Collapse();
        }

        private void RegisterCommand(IMenuCommandService menuCommandService)
        {
            var menuCommandId = new CommandID(PackageGuids.guidMakeBddNameCmdSet, PackageIds.cmdMakeBddName);
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
