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
            // TODO: -> TS | CS
            return $"{ext}.cs";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var document = NSwagDocumentBase.FromJson<NSwagDocument>(inputFileName, inputFileContent);
            var openApiDocumentExecutionResult = ThreadHelper.JoinableTaskFactory.Run(() => document.ExecuteAsync());
            return Encoding.UTF8.GetBytes(openApiDocumentExecutionResult.Output);
        }
    }
}