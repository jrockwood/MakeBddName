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
            string[] words = NormalizeSentence(sentence, nonAlphaNumbericReplacementChar: '_');
            if (makeSentence && words.Length > 0)
            {
                // capitalize the first letter of the first word
                string firstWord = words[0];
                firstWord = char.ToUpper(firstWord[0]) + firstWord.Substring(1);

                // change the case of the rest of the words
                words = words.Select(word => word.ToLower()).ToArray();
                words[0] = firstWord;
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
            string[] words = NormalizeSentence(sentence, nonAlphaNumbericReplacementChar: null);
            if (words.Length == 0)
            {
                return string.Empty;
            }

            words = words.Select(word => char.ToUpper(word[0]) + word.Substring(1)).ToArray();
            return string.Join("", words);
        }

        /// <summary>
        /// Normalizes a sentence into words.
        /// </summary>
        /// <param name="sentence">
        /// The raw sentence, which could be a either a sentence-style (spaces between words),
        /// PascalCase, or underscore sentence ('_' between words). Detection of the source style is
        /// done in the following order: space-delimited, underscore-delimited, UpperCase-delimited.
        /// </param>
        /// <param name="nonAlphaNumbericReplacementChar">
        /// The character to use for substituting non-alphanumeric characters.
        /// </param>
        /// <returns>An array of words.</returns>
        private static string[] NormalizeSentence(string sentence, char? nonAlphaNumbericReplacementChar)
        {
            if (sentence == null) { throw new ArgumentNullException(nameof(sentence)); }

            // remove the whitespace and quotes from the beginning and the end
            string trimmed = sentence.Trim(' ', '\t', '\n', '\r', '\f', '"', '\'');

            // detect the type of sentence in this order:
            // whitespace-delimited words
            // underscore-delimited words
            // uppercase-letter-delimited words
            string[] words;

            if (trimmed.Any(char.IsWhiteSpace))
            {
                words = trimmed.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            }
            else if (trimmed.Any(c => c == '_'))
            {
                words = trimmed.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                words = SplitPascalCaseIntoWords(trimmed);
            }

            // replace all non-alphanumeric characters
            words = words.Select(word => ReplaceNonAlphaNumericChars(word, nonAlphaNumbericReplacementChar)).ToArray();

            // don't return an array of empty strings
            return words.All(string.IsNullOrWhiteSpace) ? new string[0] : words;
        }

        /// <summary>
        /// Takes a sentence in PascalCase style and splits it into individual words by looking at
        /// capital letters as the word delimiter.
        /// </summary>
        /// <param name="pascalCaseSentence">The sentence to split.</param>
        /// <returns>An array of words.</returns>
        private static string[] SplitPascalCaseIntoWords(string pascalCaseSentence)
        {
            var words = new List<string>();
            var builder = new StringBuilder();

            foreach (char c in pascalCaseSentence)
            {
                if (char.IsNumber(c) || (char.IsLetter(c) && char.IsUpper(c)))
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

            return words.ToArray();
        }

        /// <summary>
        /// Replaces or removes all non-alphanumeric characters in the word.
        /// </summary>
        /// <param name="word">The word to analyze.</param>
        /// <param name="nonAlphaNumbericReplacementChar">
        /// If supplied, all non-alphanumeric characters are replaced with the specified character.
        /// Otherwise, non-alphanumeric characters are stripped from the word.
        /// </param>
        /// <returns>The word with non-alphanumeric characters either removed or replaced.</returns>
        private static string ReplaceNonAlphaNumericChars(string word, char? nonAlphaNumbericReplacementChar)
        {
            var builder = new StringBuilder();

            foreach (char c in word)
            {
                // keep the character if it's a letter, digit, or number
                if (char.IsLetterOrDigit(c) || char.IsNumber(c))
                {
                    builder.Append(c);
                }

                // replace the character
                else if (nonAlphaNumbericReplacementChar.HasValue)
                {
                    builder.Append(nonAlphaNumbericReplacementChar.Value);
                }

                // skip the character if the caller doesn't want it replaced
            }

            // trim any non-alphanumeric character replacements
            string newWord = builder.ToString();
            if (nonAlphaNumbericReplacementChar.HasValue)
            {
                newWord = newWord.Trim(nonAlphaNumbericReplacementChar.Value);
            }

            return newWord;
        }
    }
}
