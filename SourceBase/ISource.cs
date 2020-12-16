#nullable enable
using System.Collections.Generic;
using Common;

namespace SourceBase
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
        Response CreateTranslation(string key, string scope, string locale, string text, string? variant);
        
        Response UpdateTranslation(string key, string scope, string locale, string text, string? variant);
        Response DeleteTranslation(Translation translation);
        Translation GetTranslation(string key, string scope, string locale, string? variant);
        IEnumerable<Translation> GetTranslations();
    }
}