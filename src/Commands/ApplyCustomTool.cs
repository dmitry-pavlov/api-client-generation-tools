using System;
using System.ComponentModel.Design;
using CodingMachine.VisualStudio.ApiClientGenerationTools.Generators;
using EnvDTE;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace CodingMachine.VisualStudio.ApiClientGenerationTools.Commands
{
    internal sealed class ApplyCustomTool
    {
        private const int CommandId = 0x0100;
        private static readonly Guid CommandSet = new Guid("13263952-2c77-4b24-acae-06a2fc715b7b");
        private static DTE _dte;
        
        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            _dte = (DTE) await package.GetServiceAsync(typeof(DTE));
            Assumes.Present(_dte);

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as IMenuCommandService;

            commandService?.AddCommand(new OleMenuCommand(OnExecute, new CommandID(CommandSet, CommandId))
            {
                // This will defer visibility control to the VisibilityConstraints section in the .vsct file
                Supported = false
            });
        }

        private static void OnExecute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ProjectItem item = _dte.SelectedItems.Item(1).ProjectItem;

            if (item != null)
            {
                item.Properties.Item("CustomTool").Value = NswagCodeGenerator.Name;
            }
        }
    }
}
