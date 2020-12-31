using System;
using System.Collections.Generic;
using Lingos.Core.Models;
using Lingos.Generator.Json.Extensions;
using Lingos.Generator.Json.Test.Utilities;
using Moq;
using Xunit;

namespace Lingos.Generator.Json.Test.Extensions
{
    public class UnitTest1
    {
        [Fact]
        public void FormatTranslations_ReturnsCorrectly()
        {
            // arrange
            Dictionary<string, object> format = new()
            {
                ["scope"] = new Dictionary<object, object>
                {
                    ["locale"] = new Dictionary<object, object>
                    {
                        ["key"] = null,
                        ["variant"] = new[]
                        {
                            "key",
                            "locale",
                            "scope",
                            "variant",
                            "text"
                        }
                    }
                }
            };
            IEnumerable<Translation> translations = new[]
            {
                new Translation {KeyName = "k1", LocaleName = "l1", ScopeName = "s1", Variant = "v1", Text = "text"},
                new Translation {KeyName = "k1", LocaleName = "l1", ScopeName = "s1", Variant = "none", Text = "text"},
                new Translation {KeyName = "k1", LocaleName = "l2", ScopeName = "s1", Variant = "v1", Text = "text"},
                new Translation {KeyName = "k1", LocaleName = "l2", ScopeName = "s1", Variant = "none", Text = "text"},
            };
            Dictionary<string, object> expected = new()
            {
                ["s1"] = new Dictionary<string, object>
                {
                    ["l1"] = new Dictionary<string, object>
                    {
                        ["k1-v1"] = new ResultTranslation[]
                        {
                            new()
                            {
                                Key = "k1",
                                Locale = "l1",
                                Scope = "s1",
                                Variant = "v1",
                                Text = "text"
                            }
                        },
                        ["k1-none"] = new ResultTranslation[]
                        {
                            new ()
                            {
                                Key = "k1",
                                Locale = "l1",
                                Scope = "s1",
                                Variant = "none",
                                Text = "text"
                            }
                        }
                    },
                    ["l2"] = new Dictionary<string, object>
                    {
                        ["k1-v1"] = new ResultTranslation[]
                        {
                            new()
                            {
                                Key = "k1",
                                Locale = "l1",
                                Scope = "s1",
                                Variant = "v1",
                                Text = "text"
                            }
                        },
                        ["k1-none"] = new ResultTranslation[]
                        {
                            new ()
                            {
                                Key = "k1",
                                Locale = "l1",
                                Scope = "s1",
                                Variant = "none",
                                Text = "text"
                            }
                        }
                    }
                }
            };
            
            // act
            Dictionary<string, object> result = format.FormatTranslations(translations, ResultEnding.Default);
            
            // assert
            Assert.Equal(expected, result, new StringObjectDictionaryEqualityComparer());
        }
    }
}