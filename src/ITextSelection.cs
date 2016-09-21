// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ITextSelection.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-20</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using EnvDTE;

    /// <summary>
    /// Contains an abstraction for the Visual Studio <see cref="TextSelection"/> functionality.
    /// Made into an interface to support unit testing.
    /// </summary>
    internal interface ITextSelection
    {
        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        bool ActivePointAtEndOfLine { get; }
        bool ActivePointAtStartOfLine { get; }

        /// <summary>
        /// Gets whether the active point is equal to the bottom point.
        /// </summary>
        /// <returns>
        /// A value indicating if the text selection's active end is at a greater absolute character
        /// offset than the anchor in the text document.
        /// </returns>
        bool IsActiveEndGreater { get; }

        /// <summary>
        /// Gets whether the anchor point is equal to the active point.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets the text selection.
        /// </summary>
        string Text { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Moves the object the specified number of characters to the left.
        /// </summary>
        /// <param name="extend">
        /// Optional. Determines whether the moved text is collapsed or not. The default is False.
        /// </param>
        /// <param name="count">
        /// Optional. Represents the number of characters to move to the left. The default is 1.
        /// </param>
        void CharLeft(bool extend = false, int count = 1);

        /// <summary>
        /// Moves the object the specified number of characters to the right.
        /// </summary>
        /// <param name="extend">
        /// Optional. Determines whether the moved text is collapsed or not. The default is False.
        /// </param>
        /// <param name="count">
        /// Optional. Represents the number of characters to move to the right. The default is 1.
        /// </param>
        void CharRight(bool extend = false, int count = 1);

        /// <summary>
        /// Collapses the text selection to the active point.
        /// </summary>
        void Collapse();

        /// <summary>
        /// Inserts the given string at the current insertion point.
        /// </summary>
        /// <param name="text">The text to insert.</param>
        /// <param name="insertFlags">
        /// One of the <see cref="vsInsertFlags"/> values indicating how to insert the text.
        /// </param>
        void Insert(string text, vsInsertFlags insertFlags);

        /// <summary>
        /// Exchanges the position of the active and the anchor points.
        /// </summary>
        void SwapAnchor();
    }
}
