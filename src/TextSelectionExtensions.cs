// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TextSelectionExtensions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-08-05</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    /// <summary>
    /// Extension methods for working with <see cref="ITextSelection"/> objects.
    /// </summary>
    internal static class TextSelectionExtensions
    {
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
