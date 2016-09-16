// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="VsPackage.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All rights reserved. Licensed under the Apache License, Version 2.0.
//   See LICENSE in the project root for license information.
// </copyright>
// <created>2016-09-16</created>
// ---------------------------------------------------------------------------------------------------------------------

namespace MakeBddName
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [Guid(PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
         Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class VsPackage : Package
    {
        public const string PackageGuidString = "c6f71eb1-1c7f-4158-877b-7d4b8e6dc5cc";

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so
        /// this is the place where you can put all the initialization code that rely on services
        /// provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }
    }
}
