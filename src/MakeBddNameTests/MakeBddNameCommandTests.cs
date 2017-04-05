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

                [Test]
                public void should_only_extend_to_the_whole_word_if_there_are_no_quotes()
                {
                    var selection = new MockTextSelection("public void Should<<Do|>>Something()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<ShouldDoSomething|>>()");
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

                [Test]
                public void should_only_extend_to_the_word_boundary_if_there_are_no_quotes()
                {
                    var selection = new MockTextSelection("<<|NoQuo>>tes but spaces");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("<<NoQuotes|>> but spaces");
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

                [Test]
                public void should_only_extend_to_the_word_boundary_if_there_are_no_quotes()
                {
                    var selection = new MockTextSelection("NoQuotes but spa<<ces|>>");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("NoQuotes but <<spaces|>>");
                }
            }

            public class Given_a_selection_of_a_PascalCase_word
            {
                [Test]
                public void should_not_select_more_words()
                {
                    var selection = new MockTextSelection("public void <<PascalCaseMethodName|>>()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<PascalCaseMethodName|>>()");
                }

                [Test]
                public void should_move_the_active_point_to_the_end_of_the_word()
                {
                    var selection = new MockTextSelection("public void <<|PascalCaseMethodName>>()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<PascalCaseMethodName|>>()");
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

                [Test]
                public void should_select_the_current_word_if_there_are_no_quotes()
                {
                    var selection = new MockTextSelection("public void MyMetho|dName()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<MyMethodName|>>()");
                }

                [Test]
                public void should_recognize_numbers_as_part_of_the_word()
                {
                    var selection = new MockTextSelection("public void My|Method2 ()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<MyMethod2|>> ()");
                }

                [Test]
                public void should_recognize_a_generic_signature()
                {
                    var selection = new MockTextSelection("public void GenericMethod|<T>");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<GenericMethod|>><T>");
                }

                [Test]
                public void should_select_the_current_word_if_the_selection_is_at_the_beginning_of_the_word()
                {
                    var selection = new MockTextSelection("public void |MyMethodName()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<MyMethodName|>>()");
                }

                [Test]
                public void should_select_the_current_word_if_the_selection_is_at_the_end_of_the_word()
                {
                    var selection = new MockTextSelection("public void MyMethodName|()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<MyMethodName|>>()");
                }

                [Test]
                public void should_select_the_entire_underscore_delimited_word()
                {
                    var selection = new MockTextSelection("public void My_metho|d_name()");
                    MakeBddNameCommand.ExtendSelectionToFullString(selection);
                    selection.LineSpec.Should().Be("public void <<My_method_name|>>()");
                }
            }
        }

        public class RenameSelection
        {
            public class Given_a_valid_selection
            {
                [Test]
                public void should_replace_the_selection_and_put_the_cursor_at_the_end_of_the_word()
                {
                    var selection = new MockTextSelection("public void <<MethodName|>>()");
                    var options = new MockOptions(BddNameStyle.UnderscorePreserveCase);
                    MakeBddNameCommand.RenameSelection(selection, options);
                    selection.LineSpec.Should().Be("public void Method_Name|()");

                    selection = new MockTextSelection("public void <<MethodName|>>()");
                    options = new MockOptions(BddNameStyle.UnderscoreSentenceCase);
                    MakeBddNameCommand.RenameSelection(selection, options);
                    selection.LineSpec.Should().Be("public void Method_name|()");

                    selection = new MockTextSelection("public void <<MethodName|>>()");
                    options = new MockOptions(BddNameStyle.PascalCase);
                    MakeBddNameCommand.RenameSelection(selection, options);
                    selection.LineSpec.Should().Be("public void MethodName|()");
                }
            }
        }
    }
}
