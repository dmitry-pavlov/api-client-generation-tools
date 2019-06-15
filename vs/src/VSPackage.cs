using System;
using System.Runtime.InteropServices;
using System.Threading;
using CodingMachine.VisualStudio.ApiClientGenerationTools.Commands;
using CodingMachine.VisualStudio.ApiClientGenerationTools.Generators;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using Task = System.Threading.Tasks.Task;

namespace CodingMachine.VisualStudio.ApiClientGenerationTools
{
    internal static class Constants
    {
        public const string ContextGuid = "13263952-c2ea-43fd-bd20-1ce4b3fe30f9";
        public const string PackageGuid = "13263952-ae69-488d-a66f-9a3ebc510433";
        public const string ProductName = "API Client Generation Tools"; // keep the same with .resx, .vsixmanifest
        public const string ProductDescription = ProductName + "for Visual Studio";
        public const string NswagExt = "nswag";
        public const string Author = "Dmitry Pavlov";
        public const string Copyright = "© 2019 " + Author + " (aka Coding Machine)";
        public const string ProductVersion = "0.0.2";
        public const string ProductGitHib = "https://github.com/dmitry-pavlov/api-client-generation-tools"; // keep the same with .resx, .vsixmanifest

        public const string NSwagStudioName = "NSwagStudio"; 
        public const string NSwagStudioLink = "https://github.com/RicoSuter/NSwag/wiki/NSwagStudio"; 
    }
    // Must match the GUID in the .vsct file - TODO use VSIX Synchronizer extension
    [Guid(Constants.PackageGuid)] 

    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    
    // Keep the same with .vsixmanifest file 
    [InstalledProductRegistration(Constants.ProductName, Constants.ProductDescription, Constants.ProductVersion)] 
    
    [ProvideMenuResource("Menus.ctmenu", 1)]
    
    [ProvideCodeGenerator(typeof(NswagCodeGenerator), NswagCodeGenerator.Name, NswagCodeGenerator.Description, true)]
    
    // Must match the GUID in the .vsct file
    [ProvideUIContextRule(Constants.ContextGuid, 
        name: "UI Context",
        expression: Constants.NswagExt, // This will make the button only show on .nswag files
        termNames: new[] { Constants.NswagExt },
        termValues: new[] { "HierSingleSelectionName:."+Constants.NswagExt+"?$" })]
    public sealed class VSPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await ApplyCustomTool.InitializeAsync(this);
        }
    }}
