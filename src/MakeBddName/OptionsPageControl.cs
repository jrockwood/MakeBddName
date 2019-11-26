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
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// User interface for the options page.
    /// </summary>
    public partial class OptionsPageControl : UserControl
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public OptionsPageControl()
        {
            InitializeComponent();
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        /// <summary>
        /// Gets or sets the options model object.
        /// </summary>
        public OptionsPage Options { get; private set; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        /// <summary>
        /// Binds to the specified options.
        /// </summary>
        /// <param name="options">The options to bind to.</param>
        public void BindToOptions(OptionsPage options)
        {
            Options = options;

            // Set the initial state in the UI.
            OnOptionsPagePropertyChanged(options, null);

            // Update the bound options whenever the checkboxes change.
            _underscoresPreserveCaseRadioButton.CheckedChanged += OnCheckedChanged;
            _underscoresSentenceCaseRadioButton.CheckedChanged += OnCheckedChanged;
            _pascalCaseRadioButton.CheckedChanged += OnCheckedChanged;

            // Update the state whenever the model changes.
            options.PropertyChanged += OnOptionsPagePropertyChanged;
        }

        private void OnOptionsPagePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (Options.NamingStyle)
            {
                case BddNameStyle.UnderscorePreserveCase:
                    _underscoresPreserveCaseRadioButton.Checked = true;
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
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            BddNameStyle namingStyle;

            if (_underscoresPreserveCaseRadioButton.Checked)
            {
                namingStyle = BddNameStyle.UnderscorePreserveCase;
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

            Options.NamingStyle = namingStyle;
        }
    }
}
