using System.Diagnostics.CodeAnalysis;

namespace Lingos.Generator.Json
{
    [ExcludeFromCodeCoverage]
    public class ResultTranslation
    {
        public string Key { get; set; }
        public string Scope { get; set; }
        public string Locale { get; set; }
        public string Variant { get; set; }
        public string Text { get; set; }
    }
}