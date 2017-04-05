// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="VsPackage.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-16</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System.ComponentModel.Design;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [Guid(PackageGuids.guidPackageString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(
        typeof(OptionsPage),
        categoryName: "Make BDD Name",
        pageName: "General",
        categoryResourceID: 106,
        pageNameResourceID: 120,
        supportsAutomation: true)]
    [ProvideProfile(
        typeof(OptionsPage),
        categoryName: "MakeBDDName",
        objectName: "General",
        categoryResourceID: 106,
        objectNameResourceID: 107,
        isToolsOptionPage: true,
        DescriptionResourceID = 108)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
         Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class VsPackage : Package
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private DTE2 _dte;

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        internal ITextSelection GetActiveSelection()
        {
            var document = _dte.ActiveDocument?.Object("TextDocument") as TextDocument;
            TextSelection vsSelection = document?.Selection;
            return vsSelection != null ? new VsTextSelectionWrapper(vsSelection) : null;
        }

        internal IOptions GetOptionsPage()
        {
            var page = (OptionsPage)GetDialogPage(typeof(OptionsPage));
            return page;
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so
        /// this is the place where you can put all the initialization code that rely on services
        /// provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Logger.Initialize(this, Vsix.Name);

            // Get the required services.
            var menuCommandService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            _dte = GetService(typeof(DTE)) as DTE2;

            // Initialize the commands.
            MakeBddNameCommand.Initialize(menuCommandService, GetActiveSelection, GetOptionsPage);

            Logger.LogDebug("Initialized");
        }
    }
}
