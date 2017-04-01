// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MakeBddNameCommandTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-20</created>
// ---------------------------------------------------------------------------------------------------------------------

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable InconsistentNaming

namespace MakeBddNameTests
{
    using FluentAssertions;
    using MakeBddName;
    using NUnit.Framework;

    public static class MakeBddNameCommandTests
    {
        public class ExtendSelectionToFullString
        {
            public class Given_a_selection_in_the_middle_of_the_line
            {
                [Test]
                public void should_extend_to_the_quotes_if_quotes_are_present()
                {
                    var selection = new MockTextSelection("public void \"should <<do|>> something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<\"should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_start_of_the_line_if_no_beginning_quote_but_there_is_an_end_quote()
                {
                    var selection = new MockTextSelection("public void should <<do|>> something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<public void should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_end_of_the_line_if_no_end_quote_but_there_is_a_beginning_quote()
                {
                    var selection = new MockTextSelection("public void \"should <<do|>> something()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<\"should do something()|>>");
                }
            }

            public class Given_a_selection_at_the_beginning_of_the_line
            {
                [Test]
                public void should_extend_to_the_quotes_if_quotes_are_present()
                {
                    var selection = new MockTextSelection("<<|\"should do>> something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<\"should do something\"|>>()");
                }
            }

            public class Given_a_selection_at_the_end_of_the_line
            {
                [Test]
                public void should_extend_to_the_quotes_if_quotes_are_present()
                {
                    var selection = new MockTextSelection("\"should <<do something\"|>>");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<\"should do something\"|>>");
                }
            }

            public class Given_no_selection
            {
                [Test]
                public void should_extend_to_the_quotes_if_present()
                {
                    var selection = new MockTextSelection("public void \"should do| something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<\"should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_start_of_the_line_if_no_beginning_quote_but_an_ending_quote()
                {
                    var selection = new MockTextSelection("public void should do| something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<public void should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_end_of_the_line_if_no_end_quote_but_beginning_quote()
                {
                    var selection = new MockTextSelection("public void \"should do| something()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<\"should do something()|>>");
                }

                [Test]
                public void should_detect_the_beginning_quote_with_the_anchor_at_the_end_of_the_line()
                {
                    var selection = new MockTextSelection("public void \"should do something\"|");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<\"should do something\"|>>");
                }

                [Test]
                public void should_detect_the_beginning_quote_with_the_anchor_after_the_closing_quote()
                {
                    var selection = new MockTextSelection("public void \"should do something\"|()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<\"should do something\"|>>()");
                }
            }
        }
    }
}
