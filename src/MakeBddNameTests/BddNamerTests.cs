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
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("sentence");
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
        }
    }
}
