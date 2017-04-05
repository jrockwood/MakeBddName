// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="MockOptions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-04-03</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddNameTests
{
    using MakeBddName;

    public class MockOptions : IOptions
    {
        public MockOptions(BddNameStyle namingStyle = BddNameStyle.UnderscorePreserveCase)
        {
            NamingStyle = namingStyle;
        }

        public BddNameStyle NamingStyle { get; set; }
    }
}
