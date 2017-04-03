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
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.Shell;

    /// <summary>
    /// Represents the options for the package.
    /// </summary>
    [Guid("2e4f3764-6c16-42ec-8cc4-6a132f116944")]
    public class OptionsPage : DialogPage
    {
        [Category("Behavior")]
        [DisplayName("Naming Style")]
        [Description("How to convert the sentence to a BDD name.")]
        [DefaultValue(BddNameStyle.UnderscoreLowerCase)]
        public BddNameStyle NamingStyle { get; set; }

        protected override IWin32Window Window
        {
            get
            {
                var pageControl = new OptionsPageControl();
                return pageControl;
            }
        }
    }
}
