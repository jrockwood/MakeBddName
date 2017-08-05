// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="VsTextSelectionWrapper.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-20</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System;
    using EnvDTE;

    /// <summary>
    /// Implements <see cref="ITextSelection"/> by wrapping a Visual Studio <see cref="TextSelection"/>.
    /// </summary>
    internal class VsTextSelectionWrapper : ITextSelection
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private readonly TextSelection _selection;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public VsTextSelectionWrapper(TextSelection selection)
        {
            if (selection == null)
            {
                throw new ArgumentNullException(nameof(selection));
            }

            _selection = selection;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool ActivePointAtEndOfLine => _selection.ActivePoint.AtEndOfLine;
        public bool ActivePointAtStartOfLine => _selection.ActivePoint.AtStartOfLine;
        public bool IsActiveEndGreater => _selection.IsActiveEndGreater;
        public bool IsEmpty => _selection.IsEmpty;
        public string Text => _selection.Text;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void CharLeft(bool extend = false, int count = 1) => _selection.CharLeft(extend, count);

        public void CharRight(bool extend = false, int count = 1) => _selection.CharRight(extend, count);

        public void Collapse() => _selection.Collapse();

        public void Insert(string text, vsInsertFlags insertFlags) => _selection.Insert(text, (int)insertFlags);

        public void PerformActionAndRestoreSelection(Action action)
        {
            // We can't just cache _selection.ActivePoint because it's not immutable and changes
            // according to the current active point. Instead, we'll cache the individual point's
            // properties in immutable variables so that we can restore them later.
            int oldAnchorPoint = _selection.AnchorPoint.AbsoluteCharOffset;
            int oldActivePoint = _selection.ActivePoint.AbsoluteCharOffset;

            try
            {
                action();
            }
            finally
            {
                // First select the anchor point, then extend the selection to the active point.
                _selection.MoveToAbsoluteOffset(oldAnchorPoint, Extend: false);
                _selection.MoveToAbsoluteOffset(oldActivePoint, Extend: true);
            }
        }

        public void SelectLine() => _selection.SelectLine();

        public void SwapAnchor() => _selection.SwapAnchor();
    }
}
