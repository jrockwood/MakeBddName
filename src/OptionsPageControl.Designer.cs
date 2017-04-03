// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsPageControl.Designer.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2017-04-02</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    partial class OptionsPageControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox namingStyleGroupBox;
            System.Windows.Forms.Label pascalCaseLabel;
            System.Windows.Forms.Label underscoresSentenceCaseLabel;
            System.Windows.Forms.Label underscoreLowerCaseLabel;
            this._pascalCaseRadioButton = new System.Windows.Forms.RadioButton();
            this._underscoresSentenceCaseRadioButton = new System.Windows.Forms.RadioButton();
            this._underscoresLowerCaseRadioButton = new System.Windows.Forms.RadioButton();
            namingStyleGroupBox = new System.Windows.Forms.GroupBox();
            pascalCaseLabel = new System.Windows.Forms.Label();
            underscoresSentenceCaseLabel = new System.Windows.Forms.Label();
            underscoreLowerCaseLabel = new System.Windows.Forms.Label();
            namingStyleGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // namingStyleGroupBox
            // 
            namingStyleGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            namingStyleGroupBox.Controls.Add(pascalCaseLabel);
            namingStyleGroupBox.Controls.Add(this._pascalCaseRadioButton);
            namingStyleGroupBox.Controls.Add(this._underscoresSentenceCaseRadioButton);
            namingStyleGroupBox.Controls.Add(underscoresSentenceCaseLabel);
            namingStyleGroupBox.Controls.Add(this._underscoresLowerCaseRadioButton);
            namingStyleGroupBox.Controls.Add(underscoreLowerCaseLabel);
            namingStyleGroupBox.Location = new System.Drawing.Point(3, 3);
            namingStyleGroupBox.Name = "namingStyleGroupBox";
            namingStyleGroupBox.Size = new System.Drawing.Size(460, 158);
            namingStyleGroupBox.TabIndex = 1;
            namingStyleGroupBox.TabStop = false;
            namingStyleGroupBox.Text = "Naming style";
            // 
            // pascalCaseLabel
            // 
            pascalCaseLabel.AutoSize = true;
            pascalCaseLabel.Location = new System.Drawing.Point(46, 129);
            pascalCaseLabel.Margin = new System.Windows.Forms.Padding(43, 0, 3, 0);
            pascalCaseLabel.Name = "pascalCaseLabel";
            pascalCaseLabel.Size = new System.Drawing.Size(156, 13);
            pascalCaseLabel.TabIndex = 5;
            pascalCaseLabel.Text = "Example: ShouldDoSomething()";
            // 
            // _pascalCaseRadioButton
            // 
            this._pascalCaseRadioButton.AutoSize = true;
            this._pascalCaseRadioButton.Location = new System.Drawing.Point(9, 106);
            this._pascalCaseRadioButton.Margin = new System.Windows.Forms.Padding(6);
            this._pascalCaseRadioButton.Name = "_pascalCaseRadioButton";
            this._pascalCaseRadioButton.Size = new System.Drawing.Size(81, 17);
            this._pascalCaseRadioButton.TabIndex = 2;
            this._pascalCaseRadioButton.TabStop = true;
            this._pascalCaseRadioButton.Text = "PascalCase";
            this._pascalCaseRadioButton.UseVisualStyleBackColor = true;
            // 
            // _underscoresSentenceCaseRadioButton
            // 
            this._underscoresSentenceCaseRadioButton.AutoSize = true;
            this._underscoresSentenceCaseRadioButton.Location = new System.Drawing.Point(9, 64);
            this._underscoresSentenceCaseRadioButton.Margin = new System.Windows.Forms.Padding(6);
            this._underscoresSentenceCaseRadioButton.Name = "_underscoresSentenceCaseRadioButton";
            this._underscoresSentenceCaseRadioButton.Size = new System.Drawing.Size(179, 17);
            this._underscoresSentenceCaseRadioButton.TabIndex = 1;
            this._underscoresSentenceCaseRadioButton.TabStop = true;
            this._underscoresSentenceCaseRadioButton.Text = "Underscores and sentence case";
            this._underscoresSentenceCaseRadioButton.UseVisualStyleBackColor = true;
            // 
            // underscoresSentenceCaseLabel
            // 
            underscoresSentenceCaseLabel.AutoSize = true;
            underscoresSentenceCaseLabel.Location = new System.Drawing.Point(46, 87);
            underscoresSentenceCaseLabel.Margin = new System.Windows.Forms.Padding(43, 0, 3, 0);
            underscoresSentenceCaseLabel.Name = "underscoresSentenceCaseLabel";
            underscoresSentenceCaseLabel.Size = new System.Drawing.Size(164, 13);
            underscoresSentenceCaseLabel.TabIndex = 4;
            underscoresSentenceCaseLabel.Text = "Example: Should_do_something()";
            // 
            // _underscoresLowerCaseRadioButton
            // 
            this._underscoresLowerCaseRadioButton.AutoSize = true;
            this._underscoresLowerCaseRadioButton.Location = new System.Drawing.Point(9, 22);
            this._underscoresLowerCaseRadioButton.Margin = new System.Windows.Forms.Padding(6);
            this._underscoresLowerCaseRadioButton.Name = "_underscoresLowerCaseRadioButton";
            this._underscoresLowerCaseRadioButton.Size = new System.Drawing.Size(160, 17);
            this._underscoresLowerCaseRadioButton.TabIndex = 0;
            this._underscoresLowerCaseRadioButton.TabStop = true;
            this._underscoresLowerCaseRadioButton.Text = "Underscores and lower case";
            this._underscoresLowerCaseRadioButton.UseVisualStyleBackColor = true;
            // 
            // underscoreLowerCaseLabel
            // 
            underscoreLowerCaseLabel.AutoSize = true;
            underscoreLowerCaseLabel.Location = new System.Drawing.Point(46, 45);
            underscoreLowerCaseLabel.Margin = new System.Windows.Forms.Padding(43, 0, 3, 0);
            underscoreLowerCaseLabel.Name = "underscoreLowerCaseLabel";
            underscoreLowerCaseLabel.Size = new System.Drawing.Size(162, 13);
            underscoreLowerCaseLabel.TabIndex = 3;
            underscoreLowerCaseLabel.Text = "Example: should_do_something()";
            // 
            // OptionsPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(namingStyleGroupBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OptionsPageControl";
            this.Size = new System.Drawing.Size(466, 319);
            namingStyleGroupBox.ResumeLayout(false);
            namingStyleGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton _underscoresLowerCaseRadioButton;
        private System.Windows.Forms.RadioButton _pascalCaseRadioButton;
        private System.Windows.Forms.RadioButton _underscoresSentenceCaseRadioButton;
    }
}
