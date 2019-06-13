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
    [Guid("13263952-ae69-488d-a66f-9a3ebc510433")] // Must match the GUID in the .vsct file
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("API Client Generation Tools", "API Client Generation Tools for Visual Studio", "0.0.1")] // Keep the same with .vsixmanifest file 
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideCodeGenerator(typeof(NswagCodeGenerator), NswagCodeGenerator.Name, NswagCodeGenerator.Description, true)]
    [ProvideUIContextRule("13263952-c2ea-43fd-bd20-1ce4b3fe30f9", // Must match the GUID in the .vsct file
        name: "UI Context",
        expression: "nswag", // This will make the button only show on .nswag files
        termNames: new[] { "nswag" },
        termValues: new[] { "HierSingleSelectionName:.nswag?$" })]
    public sealed class VSPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await ApplyCustomTool.InitializeAsync(this);
        }
    }}
