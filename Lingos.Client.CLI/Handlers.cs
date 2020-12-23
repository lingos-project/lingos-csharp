using System;
using Lingos.Common;
using Lingos.Source.Base;

namespace Lingos.Client.CLI
{
    public class Handlers
    {
        private readonly ISource _source;

        public Handlers(ISource source)
        {
            _source = source;
        }

        internal void AddKey(string name)
        {
            Response response = _source.AddKey(name);
            
            Console.WriteLine(response.Message);
        }

        internal void UpdateKey(string oldName, string newName)
        {
            Response response = _source.UpdateKey(oldName, newName);
            
            Console.WriteLine(response.Message);
        }

        internal void AddLocale(string name, bool required)
        {
            Response response = _source.AddLocale(name, required);
            
            Console.WriteLine(response.Message);
        }

        internal void UpdateLocale(string oldName, string newName)
        {
            Response response = _source.UpdateLocale(oldName, newName);
            
            Console.WriteLine(response.Message);
        }

        internal void AddScope(string name)
        {
            Response response = _source.AddScope(name);
            
            Console.WriteLine(response.Message);
        }

        internal void UpdateScope(string oldName, string newName)
        {
            Response response = _source.UpdateScope(oldName, newName);
            
            Console.WriteLine(response.Message);
        }

        internal void AddTranslation(string key, string scope, string locale, string text, string variant)
        {
            Response response = _source.AddTranslation(key, scope, locale, text, variant);
            
            Console.WriteLine(response.Message);
        }

        internal void UpdateTranslation(string key, string scope, string locale, string text, string variant)
        {
            Response response = _source.UpdateTranslation(key, scope, locale, text, variant);
            
            Console.WriteLine(response.Message);
        }
    }
}