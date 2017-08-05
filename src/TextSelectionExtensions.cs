// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TextSelectionExtensions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-08-05</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System;

    /// <summary>
    /// Extension methods for working with <see cref="ITextSelection"/> objects.
    /// </summary>
    internal static class TextSelectionExtensions
    {
        /// <summary>
        /// Selects the entire word or sentence that should be replaced with a converted BDD name.
        /// </summary>
        /// <param name="selection">The selection to examine and modify.</param>
        public static void ExtendSelectionToFullString(this ITextSelection selection)
        {
            bool lookingForQuotes = selection.LineHasQuotes();
            // ReSharper disable ImplicitlyCapturedClosure
            Func<char, bool> isSelectionEndChar = c => lookingForQuotes ? c == '"' : !char.IsLetterOrDigit(c) && c != '_';
            // ReSharper restore ImplicitlyCapturedClosure

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

        /// <summary>
        /// Returns a value indicating whether the line has quote characters.
        /// </summary>
        /// <param name="selection">The selection to test.</param>
        /// <returns>
        /// true if there are double quote characters (") somewhere on the line; otherwise, false.
        /// </returns>
        public static bool LineHasQuotes(this ITextSelection selection)
        {
            string line = string.Empty;
            selection.PerformActionAndRestoreSelection(() =>
            {
                selection.SelectLine();
                line = selection.Text;
            });

            return line.Contains("\"");
        }
    }
}
