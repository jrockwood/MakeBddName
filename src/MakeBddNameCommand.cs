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

        private readonly Func<ITextSelection> _getTextSelectionFunc;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private MakeBddNameCommand(IMenuCommandService menuCommandService, Func<ITextSelection> getTextSelectionFunc)
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
        public static void Initialize(IMenuCommandService menuCommandService, Func<ITextSelection> getTextSelectionFunc)
        {
            if (menuCommandService == null) { throw new ArgumentNullException(nameof(menuCommandService)); }
            if (getTextSelectionFunc == null) { throw new ArgumentNullException(nameof(getTextSelectionFunc)); }

            Instance = new MakeBddNameCommand(menuCommandService, getTextSelectionFunc);
        }

        internal static void ExtendSelectionToFullString(ITextSelection selection)
        {
            bool lookingForQuotes = LineHasQuotes(selection);
            Func<char, bool> isSelectionEndChar = c => lookingForQuotes ? c == '"' : !char.IsLetterOrDigit(c);

            Action adjustSelection = () =>
            {
                // If we selected text that was enclosed with quotes, leave the quote characters in the
                // selection so they're overwritten.
                if (lookingForQuotes)
                {
                    return;
                }

                // Decrease the selection by one on both sides if we selected too much.
                if (selection.IsActiveEndGreater)
                {
                    selection.SwapAnchor();
                }

                if (!selection.IsEmpty && isSelectionEndChar(selection.Text[0]))
                {
                    selection.CharRight(extend: true, count: 1);
                }

                selection.SwapAnchor();
                if (!selection.IsEmpty && isSelectionEndChar(selection.Text[selection.Text.Length - 1]))
                {
                    selection.CharLeft(extend: true, count: 1);
                }
            };

            // If the selection is empty, check for the common case where the user just finished
            // typing a string and the caret is at the end of the string. Like this: "my test"|
            // We'll detect this case by seeing if we have a quote just to the left of the selection
            // and no quote until the end of the line.
            if (selection.IsEmpty)
            {
                selection.CharLeft(extend: true, count: 1);
                if (isSelectionEndChar(selection.Text[0]))
                {
                    selection.SwapAnchor();
                    if (selection.ActivePointAtEndOfLine)
                    {
                        // We're seeing this pattern: ..."|<eol> which means that we want to let the
                        // normal processing of a selection within a string take place further below.
                        // But first reset the anchor to just after the quote and clear the selection.
                        selection.CharLeft(extend: false, count: 1);
                    }
                    else
                    {
                        do
                        {
                            selection.CharRight(extend: true, count: 1);
                        }

                        while (!isSelectionEndChar(selection.Text[selection.Text.Length - 1])
                            && !selection.ActivePointAtEndOfLine);

                        if (isSelectionEndChar(selection.Text[selection.Text.Length - 1]))
                        {
                            // We saw this pattern: "...", which is now the answer we want
                            adjustSelection();
                            return;
                        }

                        // We now know that we're looking at this pattern ..."|, so reset the anchor
                        // and selection and let the normal processing below happen.
                        selection.SwapAnchor();
                        selection.Collapse();
                    }
                }
            }

            // Select left until we see an ending character.
            if (selection.IsActiveEndGreater)
            {
                selection.SwapAnchor();
            }

            while ((selection.IsEmpty || !isSelectionEndChar(selection.Text[0]) && !selection.ActivePointAtStartOfLine))
            {
                selection.CharLeft(extend: true, count: 1);
            }

            // Select right until we see an ending character.
            selection.SwapAnchor();
            while ((selection.IsEmpty || !isSelectionEndChar(selection.Text[selection.Text.Length - 1])
                && !selection.ActivePointAtEndOfLine))
            {
                selection.CharRight(extend: true, count: 1);
            }

            adjustSelection();
        }

        private static bool LineHasQuotes(ITextSelection selection)
        {
            string line = string.Empty;
            selection.PerformActionAndRestoreSelection(() =>
            {
                selection.SelectLine();
                line = selection.Text;
            });

            return line.Contains("\"");
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

            ITextSelection selection = _getTextSelectionFunc();
            ExtendSelectionToFullString(selection);
            string bddName = BddNamer.ToUnderscoreName(selection.Text);
            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            selection.Insert(
                bddName,
                vsInsertFlags.vsInsertFlagsContainNewText | vsInsertFlags.vsInsertFlagsCollapseToEnd);
            selection.Collapse();
        }
    }
}
