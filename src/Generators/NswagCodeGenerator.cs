using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace CodingMachine.VisualStudio.ApiClientGenerationTools.Generators
{
    [Guid("13263952-2444-4130-93a5-f9ee40e5c1ae")]
    public sealed class NswagCodeGenerator : BaseCodeGeneratorWithSite
    {
        public const string Name = nameof(NswagCodeGenerator);
        public const string Description = "Generates API client with NSwag.";

        public override string GetDefaultExtension()
        {
            var item = GetService(typeof(ProjectItem)) as ProjectItem;
            var ext = Path.GetExtension(item?.FileNames[1]);
            // TODO: -> TS | CS
            return ".Generated.cs";
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            return Encoding.UTF8.GetBytes($"{DateTime.Now}");
        }
    }
}