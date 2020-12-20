#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Lingos.Common;
using Lingos.Source.Base;
using Microsoft.EntityFrameworkCore;

namespace Lingos.Source.Postgres
{
    public class SourcePostgres : ISource
    {
        public Response Initialize()
        {
            return Migrate();
        }

        public Response Migrate()
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Database.Migrate();
            }
            catch (Exception e)
            {
                return new Response
                {
                    Message = e.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = "Migrated the database with success!",
                Type = ResponseType.Success,
            };
        }

        public Response AddLocale(string name, bool required)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Locales.Add(new Locale
                {
                    Name = name,
                    Required = required
                });

                ctx.SaveChanges();
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
            try
            {
                using DatabaseContext ctx = new();
                ctx.Database
                    .ExecuteSqlInterpolated($"UPDATE locales SET name={newName} WHERE name={oldName}");
            }
            catch (Exception e)
            {
                return new Response
                {
                    Message = e.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = $"Changed the locale {oldName} to be {newName}",
                Type = ResponseType.Success,
            };
        }

        public Response DeprecateLocale(string name)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Locales.Update(new Locale
                {
                    Name = name,
                    Deprecated = true,
                });

                ctx.SaveChanges();
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
                Message = $"Successfully deprecated {name} locale!",
                Type = ResponseType.Error,
            };
        }

        public IEnumerable<Locale> GetLocales()
        {
            using DatabaseContext ctx = new();
            return ctx.Locales.ToList();
        }

        public Response AddKey(string name)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Keys.Add(new Key
                {
                    Name = name,
                });

                ctx.SaveChanges();
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
                Message = $"Successfully added {name} key!",
                Type = ResponseType.Error,
            };
        }

        public Response UpdateKey(string oldName, string newName)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Database
                    .ExecuteSqlInterpolated($"UPDATE keys SET name={newName} WHERE name={oldName}");
            }
            catch (Exception e)
            {
                return new Response
                {
                    Message = e.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = $"Changed the key {oldName} to be {newName}",
                Type = ResponseType.Success,
            };
        }

        public Response DeleteKey(string name)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Keys.Remove(new Key { Name = name });
                
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = $"Successfully deleted the {name} key!",
                Type = ResponseType.Success,
            };
        }

        public Response AddScope(string name)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Scopes.Add(new Scope
                {
                    Name = name,
                });
                
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return new Response
                {
                    Message = e.Message,
                    Type = ResponseType.Error,
                };
            }
            
            return new Response
            {
                Message = $"Successfully added {name} scope!",
                Type = ResponseType.Success,
            };
        }

        public Response UpdateScope(string oldName, string newName)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Database
                    .ExecuteSqlInterpolated($"UPDATE scopes SET name={newName} WHERE name={oldName}");
            }
            catch (Exception e)
            {
                return new Response
                {
                    Message = e.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = $"Changed the scope {oldName} to be {newName}",
                Type = ResponseType.Success,
            };
        }

        public Response DeprecateScope(string name)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Scopes.Update(new Scope
                {
                    Name = name,
                    Deprecated = true,
                });
                
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return new Response
                {
                    Message = e.Message,
                    Type = ResponseType.Error,
                };
            }
            
            return new Response
            {
                Message = $"Successfully deprecated {name} scope",
                Type = ResponseType.Success,
            };
        }

        public IEnumerable<Scope> GetScopes()
        {
            using DatabaseContext ctx = new();
            return ctx.Scopes.ToList();
        }

        public Response AddTranslation(string key, string scope, string locale, string text, string variant)
        {
            string actualVariant = string.IsNullOrEmpty(variant) ? "none" : variant;
            
            try
            {
                using DatabaseContext ctx = new();
                ctx.Translations.Add(new Translation
                {
                    KeyName = key,
                    ScopeName = scope,
                    LocaleName = locale,
                    Text = text,
                    Variant = actualVariant,
                });

                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = $"Successfully added a new translation for {scope}.{key}[{actualVariant}] ({locale})!",
                Type = ResponseType.Success,
            };
        }
        
        public Response UpdateTranslation(string key, string scope, string locale, string text, string? variant)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Translations.Update(new Translation
                {
                    KeyName = key,
                    ScopeName = scope,
                    LocaleName = locale,
                    Text = text,
                    Variant = variant,
                });

                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = $"Successfully updated {scope}.{key}[{variant}] ({locale}) translation!",
                Type = ResponseType.Success,
            };
        }

        public Response DeleteTranslation(Translation translation)
        {
            try
            {
                using DatabaseContext ctx = new();
                ctx.Translations.Remove(translation);
                
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    Type = ResponseType.Error,
                };
            }

            return new Response
            {
                Message = $"Successfully deleted the {translation.ScopeName}.{translation.KeyName}" +
                          $"[{translation.Variant}] ({translation.LocaleName}) translation!",
                Type = ResponseType.Success,
            };
        }

        public Translation GetTranslation(string key, string scope, string locale, string? variant)
        {
            using DatabaseContext ctx = new();
            return ctx.Translations.Find(new Translation
            {
                KeyName = key,
                ScopeName = scope,
                LocaleName = locale,
                Variant = variant,
            });
        }

        public IEnumerable<Translation> GetTranslations()
        {
            using DatabaseContext ctx = new();
            return ctx.Translations.ToList();
        }
    }
}