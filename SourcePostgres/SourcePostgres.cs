#nullable enable
using System;
using System.Collections.Generic;
using Common;
using Microsoft.EntityFrameworkCore;
using SourceBase;

namespace SourcePostgres
{
    public class SourcePostgres : ISource
    {
        public Response Initialize()
        {
            throw new NotImplementedException();
        }

        public Response Migrate()
        {
            throw new NotImplementedException();
        }

        public Response AddLocale(string name, bool required)
        {
            try
            {
                using Database db = new();
                db.Locales.Add(new Locale
                {
                    Name = name,
                    Required = required
                });

                db.SaveChanges();
            }
            catch (Exception e)
            {
                return new Response
                {
                    Message = e.Message,
                    Type = ResponseType.Error
                };
            }

            return new Response
            {
                Message = $"Successfully added {name} locale!",
                Type = ResponseType.Success
            };
        }

        public Response UpdateLocale(string oldName, string newName)
        {
            const string query = @"
                UPDATE Locales
                SET Name = @NewName
                WHERE Name = @OldName
            ";

            using Database db = new();
            db.Database.ExecuteSqlRaw(query, new
            {
                OldName = oldName,
                NewName = newName
            });

            return new Response
            {
                Message = "",
                Type = ResponseType.Info,
            };
        }

        public Response DeprecateLocale(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Locale> GetLocales()
        {
            throw new NotImplementedException();
        }

        public Response AddScope(string name)
        {
            throw new NotImplementedException();
        }

        public Response UpdateScope(string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public Response DeprecateScope(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Scope> GetScopes()
        {
            throw new NotImplementedException();
        }

        public Response AddKey(string name, string scope)
        {
            throw new NotImplementedException();
        }

        public Response UpdateKeyName(string scope, string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public Response UpdateKeyScope(string name, string oldScope, string newScope)
        {
            throw new NotImplementedException();
        }

        public Response DeleteKey(string name, string scope)
        {
            throw new NotImplementedException();
        }

        public Response UpsertTranslation(string key, string scope, string locale, string text, string? variant)
        {
            throw new NotImplementedException();
        }

        public Response DeleteTranslation(string key, string scope, string locale, string? variant)
        {
            throw new NotImplementedException();
        }

        public Translation GetTranslation(string key, string scope, string locale, string? variant)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Translation> GetTranslations()
        {
            throw new NotImplementedException();
        }
    }
}