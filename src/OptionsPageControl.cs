// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsPageControl.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-04-02</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// User interface for the options page.
    /// </summary>
    public partial class OptionsPageControl : UserControl
    {
        public OptionsPageControl()
        {
            InitializeComponent();
        }

        public WeakReference<OptionsPage> Options { get; private set; }

        public void BindToOptions(OptionsPage options)
        {
            Options = new WeakReference<OptionsPage>(options);

            switch (options.NamingStyle)
            {
                case BddNameStyle.UnderscoreLowerCase:
                    _underscoresLowerCaseRadioButton.Checked = true;
                    break;

                case BddNameStyle.UnderscoreSentenceCase:
                    _underscoresSentenceCaseRadioButton.Checked = true;
                    break;

                case BddNameStyle.PascalCase:
                    _pascalCaseRadioButton.Checked = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            _underscoresLowerCaseRadioButton.CheckedChanged += OnCheckedChanged;
            _underscoresSentenceCaseRadioButton.CheckedChanged += OnCheckedChanged;
            _pascalCaseRadioButton.CheckedChanged += OnCheckedChanged;
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            BddNameStyle namingStyle;

            if (_underscoresLowerCaseRadioButton.Checked)
            {
                namingStyle = BddNameStyle.UnderscoreLowerCase;
            }
            else if (_underscoresSentenceCaseRadioButton.Checked)
            {
                namingStyle = BddNameStyle.UnderscoreSentenceCase;
            }
            else if (_pascalCaseRadioButton.Checked)
            {
                namingStyle = BddNameStyle.PascalCase;
            }
            else
            {
                return;
            }

            OptionsPage options;
            if (Options.TryGetTarget(out options))
            {
                options.NamingStyle = namingStyle;
            }
        }
    }
}
