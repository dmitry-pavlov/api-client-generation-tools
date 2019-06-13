using System.IO;
using System.Runtime.InteropServices;
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
            // TODO: -> TS | CS
            return $"{ext}.cs";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var document = ThreadHelper.JoinableTaskFactory.Run(() => NSwagDocument.LoadAsync(inputFileName));
            ThreadHelper.JoinableTaskFactory.Run(() => document.ExecuteAsync());

            var generatedFilePath = document.CodeGenerators.OpenApiToCSharpClientCommand.OutputFilePath;
            var content = File.ReadAllBytes(generatedFilePath);
            return content;
        }

    }
}