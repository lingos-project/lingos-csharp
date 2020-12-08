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

        // scope
        Response AddScope(string name);
        Response UpdateScope(string oldName, string newName);
        Response DeprecateScope(string name);
        IEnumerable<Scope> GetScopes();

        // key
        Response AddKey(string name, string scope);
        Response UpdateKeyName(string scope, string oldName, string newName);
        Response UpdateKeyScope(string name, string oldScope, string newScope);
        Response DeleteKey(string name, string scope);

        // translation
        Response UpsertTranslation(string key, string scope, string locale, string text, string? variant);
        Response DeleteTranslation(string key, string scope, string locale, string? variant);
        Translation GetTranslation(string key, string scope, string locale, string? variant);
        IEnumerable<Translation> GetTranslations();
    }
}