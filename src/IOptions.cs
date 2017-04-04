// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="IOptions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-04-03</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    /// <summary>
    /// Contains all of the options for the package.
    /// </summary>
    internal interface IOptions
    {
        /// <summary>
        /// Gets or sets a value indicating how to format the selected text.
        /// </summary>
        BddNameStyle NamingStyle { get; set; }
    }
}
