// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BddNamerTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-17</created>
// ---------------------------------------------------------------------------------------------------------------------

#pragma warning disable IDE1006 // Naming Styles

namespace MakeBddNameTests
{
    using System;
    using FluentAssertions;
    using MakeBddName;
    using NUnit.Framework;

    public static class BddNamerTests
    {
        public class ToUnderscoreName
        {
            [Test]
            public void should_throw_on_invalid_args()
            {
                Action action = () => BddNamer.ToUnderscoreName(null);
                action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("sentence");
            }

            [Test]
            public void should_rename_a_quoted_string()
            {
                BddNamer.ToUnderscoreName("\"Make bdd name\"").Should().Be("Make_bdd_name");
            }

            [Test]
            public void should_rename_an_unquoted_string()
            {
                BddNamer.ToUnderscoreName("make a name").Should().Be("make_a_name");
            }

            [Test]
            public void should_replace_non_alpha_numeric_characters()
            {
                BddNamer.ToUnderscoreName("quote\"@here").Should().Be("quote__here");
            }

            [Test]
            public void should_trim_non_alpha_numeric_characters_from_the_ends()
            {
                BddNamer.ToUnderscoreName("@#$hello_there$&*").Should().Be("hello_there");
            }

            [Test]
            public void should_preserve_underscores()
            {
                BddNamer.ToUnderscoreName("hello_there").Should().Be("hello_there");
            }

            [Test]
            public void should_return_an_empty_string_if_all_whitespace_or_non_alpha_numeric()
            {
                BddNamer.ToUnderscoreName("  @%^*  ").Should().BeEmpty();
            }

            [Test]
            public void should_convert_from_a_pascal_string()
            {
                BddNamer.ToUnderscoreName("APascalString").Should().Be("A_Pascal_String");
            }

            [Test]
            public void should_change_the_casing_of_the_sentence()
            {
                BddNamer.ToUnderscoreName("should Be a SenteNCe", makeSentence: true)
                    .Should().Be("Should_be_a_sentence");
            }

            [Test]
            public void should_convert_from_a_pascal_string_to_sentence_style()
            {
                BddNamer.ToUnderscoreName("ThisIsASentence", makeSentence: true).Should().Be("This_is_a_sentence");
            }

            [Test]
            public void should_leave_a_single_word_untouched()
            {
                BddNamer.ToUnderscoreName("Word").Should().Be("Word");
            }

            [Test]
            public void should_return_the_same_string_if_already_underscore_case()
            {
                BddNamer.ToUnderscoreName("already_underscore").Should().Be("already_underscore");
            }

            [Test]
            public void should_treat_the_sentence_as_an_underscore_sentence_if_there_is_at_least_one()
            {
                BddNamer.ToUnderscoreName("PascalCase_WithAnUnderscore", makeSentence: false)
                    .Should().Be("PascalCase_WithAnUnderscore");
                BddNamer.ToUnderscoreName("PascalCase_WithAnUnderscore", makeSentence: true)
                    .Should().Be("PascalCase_withanunderscore");
            }
        }

        public class ToPascalCase
        {
            [Test]
            public void should_throw_on_invalid_args()
            {
                Action action = () => BddNamer.ToPascalCase(null);
                action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("sentence");
            }

            [Test]
            public void should_rename_a_quoted_string()
            {
                BddNamer.ToPascalCase("\"Make bdd name\"").Should().Be("MakeBddName");
            }

            [Test]
            public void should_rename_an_unquoted_string()
            {
                BddNamer.ToPascalCase("make a name").Should().Be("MakeAName");
            }

            [Test]
            public void should_remove_non_alpha_numeric_characters()
            {
                BddNamer.ToPascalCase("Quote\"@Here").Should().Be("QuoteHere");
            }

            [Test]
            public void should_capitalize_all_words()
            {
                BddNamer.ToPascalCase("this is lower").Should().Be("ThisIsLower");
            }

            [Test]
            public void should_start_a_new_word_after_a_number()
            {
                BddNamer.ToPascalCase("go 2 market").Should().Be("Go2Market");
            }

            [Test]
            public void should_remove_non_alpha_numeric_characters_from_the_ends()
            {
                BddNamer.ToPascalCase("@#$hello there$&*").Should().Be("HelloThere");
            }

            [Test]
            public void should_remove_underscores()
            {
                BddNamer.ToPascalCase("hello_there").Should().Be("HelloThere");
            }

            [Test]
            public void should_return_an_empty_string_if_all_whitespace_or_non_alpha_numeric()
            {
                BddNamer.ToPascalCase("  @%^*  ").Should().BeEmpty();
            }

            [Test]
            public void should_convert_from_an_underscore_string()
            {
                BddNamer.ToPascalCase("this_is_an_underscore_string").Should().Be("ThisIsAnUnderscoreString");
            }

            [Test]
            public void should_leave_a_single_capitalized_word_untouched()
            {
                BddNamer.ToPascalCase("Word").Should().Be("Word");
            }

            [Test]
            public void should_convert_a_single_lowercase_word_to_uppercase()
            {
                BddNamer.ToPascalCase("word").Should().Be("Word");
            }

            [Test]
            public void should_return_the_same_string_if_already_PascalCase()
            {
                BddNamer.ToPascalCase("PascalCase").Should().Be("PascalCase");
            }

            [Test]
            public void should_strip_underscores_if_there_is_a_space_in_the_sentence()
            {
                BddNamer.ToPascalCase("here_ is an underscore").Should().Be("HereIsAnUnderscore");
            }
        }
    }
}
