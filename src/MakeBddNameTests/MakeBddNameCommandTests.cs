// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MakeBddNameCommandTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-20</created>
// ---------------------------------------------------------------------------------------------------------------------

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
                public void should_extend_to_the_quotes()
                {
                    var selection = new MockTextSelection("public class \"should <<do|>> something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public class <<\"should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_start_of_the_line_if_no_beginning_quote()
                {
                    var selection = new MockTextSelection("public class should <<do|>> something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<public class should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_end_of_the_line_if_no_end_quote()
                {
                    var selection = new MockTextSelection("public class \"should <<do|>> something()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public class <<\"should do something()|>>");
                }
            }

            public class Given_a_selection_at_the_beginning_of_the_line
            {
                [Test]
                public void should_extend_to_the_quotes()
                {
                    var selection = new MockTextSelection("<<|\"should do>> something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<\"should do something\"|>>()");
                }
            }

            public class Given_a_selection_at_the_end_of_the_line
            {
                [Test]
                public void should_extend_to_the_quotes()
                {
                    var selection = new MockTextSelection("\"should <<do something\"|>>");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<\"should do something\"|>>");
                }
            }

            public class Given_no_selection
            {
                [Test]
                public void should_extend_to_the_quotes()
                {
                    var selection = new MockTextSelection("public class \"should do| something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public class <<\"should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_start_of_the_line_if_no_beginning_quote()
                {
                    var selection = new MockTextSelection("public class should do| something\"()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<public class should do something\"|>>()");
                }

                [Test]
                public void should_extend_to_the_end_of_the_line_if_no_end_quote()
                {
                    var selection = new MockTextSelection("public class \"should do| something()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public class <<\"should do something()|>>");
                }

                [Test]
                public void should_detect_the_beginning_quote_with_the_anchor_at_the_end_of_the_line()
                {
                    var selection = new MockTextSelection("public class \"should do something\"|");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public class <<\"should do something\"|>>");
                }

                [Test]
                public void should_detect_the_beginning_quote_with_the_anchor_not_at_the_end_of_the_line()
                {
                    var selection = new MockTextSelection("public class \"should do something\"|()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public class <<\"should do something\"|>>()");
                }
            }
        }
    }
}
