// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="BddNamer.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-17</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class BddNamer
    {
        /// <summary>
        /// Converts a string from sentence-style to a valid C# identifier with underscores between words.
        /// </summary>
        /// <param name="sentence">The sentence-style string to convert.</param>
        /// <param name="makeSentence">
        /// If true, converts all but the first word to lower case; otherwise the casing of the words
        /// is not changed.
        /// </param>
        /// <returns>A valid C# identifier with underscores between words.</returns>
        /// <example>"This is my method name" becomes "This_is_my_method_name"</example>
        public static string ToUnderscoreName(string sentence, bool makeSentence = false)
        {
            IEnumerable<string> words = NormalizeSentence(sentence, nonAlphaNumbericReplacementChar: '_');
            if (makeSentence)
            {
                // ReSharper disable PossibleMultipleEnumeration
                words = words.Take(1).Concat(words.Skip(1).Select(word => word.ToLowerInvariant()));
                // ReSharper restore PossibleMultipleEnumeration
            }

            return string.Join("_", words);
        }

        /// <summary>
        /// Converts a string from sentence-style to a valid C# identifier using PascalCase, where
        /// each word begins with a capital letter and is concentenated together.
        /// </summary>
        /// <param name="sentence">The sentence-style string to convert.</param>
        /// <returns>A valid C# identifier using PascalCase.</returns>
        /// <example>"This is my method name" becomes "ThisIsMyMethodName"</example>
        public static string ToPascalCase(string sentence)
        {
            IEnumerable<string> words = NormalizeSentence(sentence, nonAlphaNumbericReplacementChar: null);
            words = words.Select(word => char.ToUpperInvariant(word[0]) + word.Substring(1)).ToArray();
            return string.Join("", words);
        }

        /// <summary>
        /// Normalizes a sentence into words.
        /// </summary>
        /// <param name="sentence">
        /// The raw sentence, which could be a combination of sentence-style (spaces between words),
        /// PascalCase, or underscore '_' delimited words.
        /// </param>
        /// <param name="nonAlphaNumbericReplacementChar">
        /// The character to use for substituting non-alphanumeric characters.
        /// </param>
        /// <returns>An array of words.</returns>
        private static IEnumerable<string> NormalizeSentence(string sentence, char? nonAlphaNumbericReplacementChar)
        {
            if (sentence == null) { throw new ArgumentNullException(nameof(sentence)); }

            // remove the whitespace and quotes from the beginning and the end
            string trimmed = sentence.Trim(' ', '\t', '\n', '\r', '\f', '"', '\'');

            // replace all non-alphanumeric characters
            var builder = new StringBuilder(trimmed);
            for (int i = 0; i < builder.Length; i++)
            {
                char c = builder[i];
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    builder[i] = nonAlphaNumbericReplacementChar.GetValueOrDefault(' ');
                }
            }

            trimmed = builder.ToString();

            // trim any non-alphanumeric character replacements
            if (nonAlphaNumbericReplacementChar.HasValue)
            {
                trimmed = trimmed.Trim(nonAlphaNumbericReplacementChar.Value);
            }

            // split the string into words
            var words = new List<string>();
            builder.Clear();
            foreach (char c in trimmed)
            {
                // we hit a word boundary, so add the word and start a new one
                if (char.IsWhiteSpace(c) || c == nonAlphaNumbericReplacementChar.GetValueOrDefault('\0'))
                {
                    words.Add(builder.ToString());
                    builder.Clear();
                }
                // an upper-case letter is also a word boundary
                else if (char.IsLetter(c) && char.IsUpper(c))
                {
                    if (builder.Length > 0)
                    {
                        words.Add(builder.ToString());
                        builder.Clear();
                    }
                    builder.Append(c);
                }
                else
                {
                    builder.Append(c);
                }
            }

            // add the last word
            if (builder.Length > 0)
            {
                words.Add(builder.ToString());
            }

            return words.Where(word => !string.IsNullOrWhiteSpace(word));
        }
    }
}
