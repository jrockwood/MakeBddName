// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BddNameStyle.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-04-02</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    /// <summary>
    /// Represents the different types of naming conventions for a BDD sentence.
    /// </summary>
    public enum BddNameStyle
    {
        /// <summary>
        /// Represents a sentence with underscores ('_') between words and where all of the words are
        /// converted to lower case. For example, "should_do_something".
        /// </summary>
        UnderscoreLowerCase,

        /// <summary>
        /// Represents a sentence with underscores ('_') between words and where all of the words
        /// except for the first word are converted to lower case and the first word is capitalized.
        /// For example, "Should_do_something".
        /// </summary>
        UnderscoreSentenceCase,

        /// <summary>
        /// Represents a sentence where each word is capitalized and concatenated together. For
        /// example, "ShouldDoSomething".
        /// </summary>
        PascalCase,
    }
}
