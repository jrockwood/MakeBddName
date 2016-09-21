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

        private TextSelection _selection;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public VsTextSelectionWrapper(TextSelection selection)
        {
            if (selection == null) { throw new ArgumentNullException(nameof(selection)); }
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

        public void SwapAnchor() => _selection.SwapAnchor();
    }
}
