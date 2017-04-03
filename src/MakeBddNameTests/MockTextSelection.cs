// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MockTextSelection.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-20</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddNameTests
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using EnvDTE;
    using MakeBddName;

    [DebuggerDisplay("{LineSpec}")]
    internal class MockTextSelection : ITextSelection
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        public const string AnchorMarker = "|";
        public const string SelectionStartMarker = "<<";
        public const string SelectionEndMarker = ">>";

        private string _line;
        private int _selectionStart;
        private int _selectionEnd;
        private int _anchorPosition;

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public MockTextSelection(string lineSpec)
        {
            _line = lineSpec ?? string.Empty;

            // Get the selection start and then strip out the marker.
            _selectionStart = _line.IndexOf(SelectionStartMarker, StringComparison.Ordinal);
            if (_selectionStart >= 0)
            {
                _line = _line.Substring(0, _selectionStart) +
                    _line.Substring(_selectionStart + SelectionStartMarker.Length);
            }

            // Get the anchor position and then strip out the marker.
            _anchorPosition = _line.IndexOf(AnchorMarker, StringComparison.Ordinal);
            if (_anchorPosition < 0)
            {
                throw new ArgumentException("No anchor (caret position) specified.", nameof(lineSpec));
            }
            _line = _line.Substring(0, _anchorPosition) + _line.Substring(_anchorPosition + AnchorMarker.Length);

            // Get the selection end and then strip out the marker.
            _selectionEnd = _line.LastIndexOf(SelectionEndMarker, StringComparison.Ordinal);
            if (_selectionEnd >= 0)
            {
                _line = _line.Substring(0, _selectionEnd) + _line.Substring(_selectionEnd + SelectionEndMarker.Length);
            }

            if (_selectionStart < 0 && _selectionEnd < 0)
            {
                _selectionStart = _selectionEnd = _anchorPosition;
            }

            if ((_selectionStart >= 0 && _selectionEnd < 0) || (_selectionStart < 0 && _selectionEnd >= 0))
            {
                throw new ArgumentException("The selection was not bounded.", nameof(lineSpec));
            }

            if (_anchorPosition != _selectionStart && _anchorPosition != _selectionEnd)
            {
                throw new ArgumentException("The anchor is not at one end of the selection.", nameof(lineSpec));
            }
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool ActivePointAtEndOfLine => _anchorPosition == _line.Length;
        public bool ActivePointAtStartOfLine => _anchorPosition == 0;
        public bool IsActiveEndGreater => _anchorPosition > _selectionStart;
        public bool IsEmpty => _selectionStart == _selectionEnd;
        public string Text => _line.Substring(_selectionStart, _selectionEnd - _selectionStart);

        public string LineSpec
        {
            get
            {
                if (IsEmpty)
                {
                    return _line.Substring(0, _anchorPosition) + AnchorMarker + _line.Substring(_anchorPosition);
                }

                string start = _line.Substring(0, _selectionStart);
                string selection = Text;
                string end = _line.Substring(_selectionEnd);

                StringBuilder builder = new StringBuilder(start).Append(SelectionStartMarker);
                if (_anchorPosition == _selectionStart)
                {
                    builder.Append(AnchorMarker);
                }

                builder.Append(selection);
                if (_anchorPosition == _selectionEnd)
                {
                    builder.Append(AnchorMarker);
                }

                builder.Append(SelectionEndMarker).Append(end);
                return builder.ToString();
            }
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void CharLeft(bool extend = false, int count = 1)
        {
            int newAnchor = CalculateNewAnchor(-count);

            // moving the start to the left
            if (_anchorPosition == _selectionStart)
            {
                _selectionStart = newAnchor;
                if (!extend)
                {
                    _selectionEnd = _selectionStart;
                }
            }
            // moving the end to the left
            else
            {
                // selection moves past the original start (anchors swap)
                if (extend && newAnchor < _selectionStart)
                {
                    _selectionEnd = _selectionStart;
                    _selectionStart = newAnchor;
                }
                // selection collapses
                else if (!extend)
                {
                    _selectionStart = _selectionEnd = newAnchor;
                }
                // selection end moves to the left
                else
                {
                    _selectionEnd = newAnchor;
                }
            }

            _anchorPosition = newAnchor;
        }

        public void CharRight(bool extend = false, int count = 1)
        {
            int newAnchor = CalculateNewAnchor(count);

            // moving the start to the right
            if (_anchorPosition == _selectionStart)
            {
                // selection moves past the original end (anchors swap)
                if (extend && newAnchor > _selectionEnd)
                {
                    _selectionStart = _selectionEnd;
                    _selectionEnd = newAnchor;
                }
                // selection collapses
                else if (!extend)
                {
                    _selectionStart = _selectionEnd = newAnchor;
                }
                // selection start is moved to the right
                else
                {
                    _selectionStart = newAnchor;
                }
            }
            // moving the end to the right
            else
            {
                _selectionEnd = newAnchor;
                if (!extend)
                {
                    _selectionStart = newAnchor;
                }
            }

            _anchorPosition = newAnchor;
        }

        public void Collapse()
        {
            _selectionStart = _selectionEnd = _anchorPosition;
        }

        public void SelectLine()
        {
            _selectionStart = 0;
            _selectionEnd = _line.Length;
            _anchorPosition = _selectionEnd;
        }

        public void SwapAnchor()
        {
            _anchorPosition = _anchorPosition == _selectionStart ? _selectionEnd : _selectionStart;
        }

        [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
        public void Insert(string text, vsInsertFlags insertFlags)
        {
            if ((insertFlags & vsInsertFlags.vsInsertFlagsContainNewText) == vsInsertFlags.vsInsertFlagsContainNewText)
            {
                _line = _line.Substring(0, _selectionStart) + text + _line.Substring(_selectionEnd);
                int newSelectionEnd = _selectionStart + text.Length;
                if (_anchorPosition == _selectionEnd)
                {
                    _anchorPosition = newSelectionEnd;
                }
                _selectionEnd = newSelectionEnd;
            }

            if ((insertFlags & vsInsertFlags.vsInsertFlagsInsertAtEnd) == vsInsertFlags.vsInsertFlagsInsertAtStart)
            {
                throw new NotSupportedException("vsInsertFlagsInsertAtEnd is not supported");
            }

            if ((insertFlags & vsInsertFlags.vsInsertFlagsInsertAtStart) == vsInsertFlags.vsInsertFlagsInsertAtStart)
            {
                throw new NotSupportedException("vsInsertFlagsInsertAtStart is not supported");
            }

            if ((insertFlags & vsInsertFlags.vsInsertFlagsCollapseToEnd) == vsInsertFlags.vsInsertFlagsCollapseToEnd)
            {
                _anchorPosition = _selectionEnd;
                Collapse();
            }

            if ((insertFlags & vsInsertFlags.vsInsertFlagsCollapseToStart) == vsInsertFlags.vsInsertFlagsCollapseToStart)
            {
                _anchorPosition = _selectionStart;
                Collapse();
            }
        }

        public void PerformActionAndRestoreSelection(Action action)
        {
            int oldStart = _selectionStart;
            int oldEnd = _selectionEnd;
            int oldAnchor = _anchorPosition;

            try
            {
                action();
            }
            finally
            {
                _selectionStart = oldStart;
                _selectionEnd = oldEnd;
                _anchorPosition = oldAnchor;
            }
        }

        private int CalculateNewAnchor(int count)
        {
            if (_anchorPosition != _selectionStart && _anchorPosition != _selectionEnd)
            {
                throw new InvalidOperationException("Anchor is in an invalid position");
            }

            int newAnchor = _anchorPosition + count;
            if (newAnchor < 0)
            {
                throw new InvalidOperationException("Trying to select the previous line.");
            }

            if (newAnchor > _line.Length + 1)
            {
                throw new InvalidOperationException("Trying to extend past the line.");
            }

            return newAnchor;
        }
    }
}
