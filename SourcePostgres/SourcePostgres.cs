using System.Collections.Generic;
using Common;
using SourceBase;

namespace SourcePostgres
{
    public class SourcePostgres: ISource
    {
        public Response Initialize()
        {
            throw new System.NotImplementedException();
        }

        public Response Migrate()
        {
            throw new System.NotImplementedException();
        }

        public Response AddLocale(string name, bool required)
        {
            throw new System.NotImplementedException();
        }

        public Response UpdateLocale(string oldName, string newName)
        {
            throw new System.NotImplementedException();
        }

        public Response DeprecateLocale(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Locale> GetLocales()
        {
            throw new System.NotImplementedException();
        }

        public Response AddScope(string name)
        {
            throw new System.NotImplementedException();
        }

        public Response UpdateScope(string oldName, string newName)
        {
            throw new System.NotImplementedException();
        }

        public Response DeprecateScope(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Scope> GetScopes()
        {
            throw new System.NotImplementedException();
        }

        public Response AddKey(string name, string scope)
        {
            throw new System.NotImplementedException();
        }

        public Response UpdateKeyName(string scope, string oldName, string newName)
        {
            throw new System.NotImplementedException();
        }

        public Response UpdateKeyScope(string name, string oldScope, string newScope)
        {
            throw new System.NotImplementedException();
        }

        public Response DeleteKey(string name, string scope)
        {
            throw new System.NotImplementedException();
        }

        public Response UpsertTranslation(string key, string scope, string locale, string text, string? variant)
        {
            throw new System.NotImplementedException();
        }

        public Response DeleteTranslation(string key, string scope, string locale, string? variant)
        {
            throw new System.NotImplementedException();
        }

        public Translation GetTranslation(string key, string scope, string locale, string? variant)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Translation> GetTranslations()
        {
            throw new System.NotImplementedException();
        }
    }
}