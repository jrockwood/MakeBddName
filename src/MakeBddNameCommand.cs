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
    using System.Diagnostics;
    using EnvDTE;

    /// <summary>
    /// Represents the "Make BDD Name" menu command.
    /// </summary>
    internal sealed class MakeBddNameCommand
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly Func<TextSelection> _getTextSelectionFunc;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private MakeBddNameCommand(IMenuCommandService menuCommandService, Func<TextSelection> getTextSelectionFunc)
        {
            Debug.Assert(menuCommandService != null, "commandService != null");
            Debug.Assert(getTextSelectionFunc != null, "getTextSelectionFunc != null");
            RegisterCommand(menuCommandService);
            _getTextSelectionFunc = getTextSelectionFunc;
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
        public static void Initialize(IMenuCommandService menuCommandService, Func<TextSelection> getTextSelectionFunc)
        {
            if (menuCommandService == null) { throw new ArgumentNullException(nameof(menuCommandService)); }
            if (getTextSelectionFunc == null) { throw new ArgumentNullException(nameof(getTextSelectionFunc)); }

            Instance = new MakeBddNameCommand(menuCommandService, getTextSelectionFunc);
        }

        private static void ExtendSelectionToFullString(TextSelection selection)
        {
            // If the selection is empty, then move left until we see the first quote character.
            if (selection.IsEmpty)
            {
                do
                {
                    selection.CharLeft(Extend: true, Count: 1);
                }
                while (selection.Text[0] != '"' && !selection.ActivePoint.AtStartOfLine);

                if (!selection.ActivePoint.AtStartOfLine)
                {
                    // Make sure the selection only includes the quote char.
                    selection.SwapAnchor();
                    selection.CharRight(Extend: true, Count: -(selection.Text.Length - 1));
                    selection.SwapAnchor();

                    // Now get one more char to the left.
                    selection.CharLeft(Extend: true, Count: 1);
                }
            }

            // Select left until we see a quote character.
            if (selection.IsActiveEndGreater)
            {
                selection.SwapAnchor();
            }

            while ((selection.IsEmpty || selection.Text[0] != '"') && !selection.ActivePoint.AtStartOfLine)
            {
                selection.CharLeft(Extend: true, Count: 1);
            }

            // Select right until we see a quote character.
            selection.SwapAnchor();
            while ((selection.IsEmpty || selection.Text[selection.Text.Length - 1] != '"') && !selection.ActivePoint.AtEndOfLine)
            {
                selection.CharRight(Extend: true, Count: 1);
            }
        }

        private void RegisterCommand(IMenuCommandService menuCommandService)
        {
            var menuCommandId = new CommandID(PackageGuids.guidMakeBddNameCmdSet, PackageIds.cmdMakeBddName);
            var menuItem = new MenuCommand(OnMakeBddNameCommandClick, menuCommandId);
            menuCommandService.AddCommand(menuItem);
        }

        private void OnMakeBddNameCommandClick(object sender, EventArgs e)
        {
            Logger.Log($"Inside {GetType().FullName}.{nameof(OnMakeBddNameCommandClick)}");

            TextSelection selection = _getTextSelectionFunc();
            ExtendSelectionToFullString(selection);
            string bddName = BddNamer.ToBddName(selection.Text);
            selection.Insert(
                bddName,
                (int)(vsInsertFlags.vsInsertFlagsContainNewText | vsInsertFlags.vsInsertFlagsCollapseToEnd));
            selection.Collapse();
        }
    }
}
