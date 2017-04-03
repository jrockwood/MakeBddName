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
            this.pascalCaseRadioButton = new System.Windows.Forms.RadioButton();
            this.underscoresSentenceCaseRadioButton = new System.Windows.Forms.RadioButton();
            this.underscoreLowerCaseRadioButton = new System.Windows.Forms.RadioButton();
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
            namingStyleGroupBox.Controls.Add(this.pascalCaseRadioButton);
            namingStyleGroupBox.Controls.Add(this.underscoresSentenceCaseRadioButton);
            namingStyleGroupBox.Controls.Add(underscoresSentenceCaseLabel);
            namingStyleGroupBox.Controls.Add(this.underscoreLowerCaseRadioButton);
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
            // pascalCaseRadioButton
            // 
            this.pascalCaseRadioButton.AutoSize = true;
            this.pascalCaseRadioButton.Location = new System.Drawing.Point(9, 106);
            this.pascalCaseRadioButton.Margin = new System.Windows.Forms.Padding(6);
            this.pascalCaseRadioButton.Name = "pascalCaseRadioButton";
            this.pascalCaseRadioButton.Size = new System.Drawing.Size(81, 17);
            this.pascalCaseRadioButton.TabIndex = 2;
            this.pascalCaseRadioButton.TabStop = true;
            this.pascalCaseRadioButton.Text = "PascalCase";
            this.pascalCaseRadioButton.UseVisualStyleBackColor = true;
            // 
            // underscoresSentenceCaseRadioButton
            // 
            this.underscoresSentenceCaseRadioButton.AutoSize = true;
            this.underscoresSentenceCaseRadioButton.Location = new System.Drawing.Point(9, 64);
            this.underscoresSentenceCaseRadioButton.Margin = new System.Windows.Forms.Padding(6);
            this.underscoresSentenceCaseRadioButton.Name = "underscoresSentenceCaseRadioButton";
            this.underscoresSentenceCaseRadioButton.Size = new System.Drawing.Size(179, 17);
            this.underscoresSentenceCaseRadioButton.TabIndex = 1;
            this.underscoresSentenceCaseRadioButton.TabStop = true;
            this.underscoresSentenceCaseRadioButton.Text = "Underscores and sentence case";
            this.underscoresSentenceCaseRadioButton.UseVisualStyleBackColor = true;
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
            // underscoreLowerCaseRadioButton
            // 
            this.underscoreLowerCaseRadioButton.AutoSize = true;
            this.underscoreLowerCaseRadioButton.Location = new System.Drawing.Point(9, 22);
            this.underscoreLowerCaseRadioButton.Margin = new System.Windows.Forms.Padding(6);
            this.underscoreLowerCaseRadioButton.Name = "underscoreLowerCaseRadioButton";
            this.underscoreLowerCaseRadioButton.Size = new System.Drawing.Size(160, 17);
            this.underscoreLowerCaseRadioButton.TabIndex = 0;
            this.underscoreLowerCaseRadioButton.TabStop = true;
            this.underscoreLowerCaseRadioButton.Text = "Underscores and lower case";
            this.underscoreLowerCaseRadioButton.UseVisualStyleBackColor = true;
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

        private System.Windows.Forms.RadioButton underscoreLowerCaseRadioButton;
        private System.Windows.Forms.RadioButton pascalCaseRadioButton;
        private System.Windows.Forms.RadioButton underscoresSentenceCaseRadioButton;
    }
}
