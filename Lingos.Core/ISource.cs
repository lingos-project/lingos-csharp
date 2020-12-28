#nullable enable
using System.Collections.Generic;
using Lingos.Core.Models;
using Lingos.Core.Utilities;

namespace Lingos.Core
{
    public interface ISource
    {
        // general
        Response Initialize();
        Response Migrate();

        // locale
        Response AddLocale(string name, bool required);
        Response UpdateLocale(string oldName, string newName);
        Response DeprecateLocale(string name);
        IEnumerable<Locale> GetLocales();
        
        // key
        Response AddKey(string name);
        Response UpdateKey(string oldName, string newName);
        Response DeleteKey(string name);

        // scope
        Response AddScope(string name);
        Response UpdateScope(string oldName, string newName);
        Response DeprecateScope(string name);
        IEnumerable<Scope> GetScopes();

        // translation
        Response AddTranslation(string key, string scope, string locale, string text, string variant = "none");
        
        Response UpdateTranslation(string key, string scope, string locale, string text, string? variant);
        Response DeleteTranslation(Translation translation);
        Translation GetTranslation(string key, string scope, string locale, string? variant);
        IEnumerable<Translation> GetTranslations();
    }
}