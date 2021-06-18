using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using NSwag.Commands;

namespace CodingMachine.VisualStudio.ApiClientGenerationTools.Generators
{
    [Guid("13263952-2444-4130-93a5-f9ee40e5c1ae")]
    public sealed class NswagCodeGenerator : BaseCodeGeneratorWithSite
    {
        public const string Name = nameof(NswagCodeGenerator);
        public const string Description = "API Client Generator with NSwag - the Swagger/OpenAPI toolchain for .NET, ASP.NET Core and TypeScript.";

        public override string GetDefaultExtension()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var item = (ProjectItem) GetService(typeof(ProjectItem));
            var ext = Path.GetExtension(item?.FileNames[1]);
            return $"{ext}.txt";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var generationReport = new StringBuilder()
                .AppendLine($"Code Generation Report - {DateTime.Now}")
                .Append(Environment.NewLine);

            try
            {
                var document = ThreadHelper.JoinableTaskFactory.Run(() => NSwagDocument.LoadAsync(inputFileName));
                ThreadHelper.JoinableTaskFactory.Run(() => document.ExecuteAsync());

                generationReport = generationReport.AppendLine(
                        $"- C# client: {document.CodeGenerators.OpenApiToCSharpClientCommand?.OutputFilePath ?? "not selected"}")
                    .AppendLine(
                        $"- TS client: {document.CodeGenerators.OpenApiToTypeScriptClientCommand?.OutputFilePath ?? "not selected"}")
                    .AppendLine(
                        $"- C# controllers: {document.CodeGenerators.OpenApiToCSharpControllerCommand?.OutputFilePath ?? "not selected"}")
                    .Append(Environment.NewLine);
            }
            catch (Exception exception)
            {
                generationReport = generationReport
                    .AppendLine($"Generation failed: {exception.Message}")
                    .Append(Environment.NewLine)
                    .AppendLine("Error details:")
                    .AppendLine(exception.ToString())
                    .Append(Environment.NewLine);
            }

            generationReport = generationReport
                .AppendLine("Tips & Tricks:").Append(Environment.NewLine)
                .AppendLine($"- Use {Constants.NSwagStudioName} - a Windows desktop app for configuring .{Constants.NswagExt} settings visually.")
                .AppendLine($"  {Constants.NSwagStudioLink}")
                .AppendLine($"- Configure Visual Studio to automatically open .{Constants.NswagExt} files in {Constants.NSwagStudioName}: in right click on .{Constants.NswagExt} file in Solution Explorer -> Open With... -> Add -> extension to {Constants.NSwagStudioName} app.")
                .AppendLine($"- To regenerate code quickly just select .{Constants.NswagExt} file and press CTRL+S.")
                .AppendLine($"- If code is not generated check .{Constants.NswagExt} file `Custom Tool` in Property Window. There is should be `{Name}`. You can just add it manually or select `Generate API Client` in context menu.")
                .AppendLine($"  {Constants.NSwagStudioLink}").Append(Environment.NewLine);

            generationReport = generationReport
                .AppendLine("Support Development:").Append(Environment.NewLine)
                .AppendLine($"- Note a lovely `Sponsor` button with available options at the top of this page {Constants.ProductGitHib}. Your support is much appreciated!")
                .AppendLine($"- Please provide feedback on {Constants.ProductName} extension by raising new issue here {Constants.ProductGitHib}/issues");

            // # Support Development
            // Note a lovely :heart: `Sponsor` button with available options at the top of this page. Your support is much appreciated!

            return Encoding.UTF8.GetBytes(generationReport.ToString());
        }
    }
}