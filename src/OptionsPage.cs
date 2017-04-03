// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsPage.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-04-02</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System.ComponentModel;
    using Microsoft.VisualStudio.Shell;

    public class OptionsPage : DialogPage
    {
        [Category("Behavior")]
        [DisplayName("Naming Style")]
        [Description("How to convert the sentence to a BDD name.")]
        [DefaultValue(BddNameStyle.UnderscoreLowerCase)]
        public BddNameStyle NamingStyle { get; set; }
    }
}
