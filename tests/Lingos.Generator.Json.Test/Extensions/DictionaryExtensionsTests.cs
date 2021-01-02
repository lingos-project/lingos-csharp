using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Lingos.Core.Models;
using Lingos.Generator.Json.Extensions;
using Xunit;

namespace Lingos.Generator.Json.Test.Extensions
{
    [ExcludeFromCodeCoverage]
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void GetWantedFormat_CorrectFormat_ReturnsCorrectly()
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
            IEnumerable<IEnumerable<TranslationValueType>> expectedWantedGrouping = new[]
            {
                new[] {TranslationValueType.Scope},
                new[] {TranslationValueType.Locale},
                new[] {TranslationValueType.Key, TranslationValueType.Variant}
            };
            IEnumerable<TranslationValueType> expectedWantedValues = new[]
            {
                TranslationValueType.Key,
                TranslationValueType.Locale,
                TranslationValueType.Scope,
                TranslationValueType.Variant,
                TranslationValueType.Text
            };
            
            // act
            (IEnumerable<IEnumerable<TranslationValueType>> actualGrouping, IEnumerable<TranslationValueType> actualValues) = format.Parse();
            
            // assert
            Assert.Equal(expectedWantedGrouping, actualGrouping);
            Assert.Equal(expectedWantedValues, actualValues);
        }

        [Fact]
        public void GetWantedFormat_WrongGrouping_ThrowsException()
        {
            // arrange
            Dictionary<string, object> format = new()
            {
                ["non existent"] = new Dictionary<object, object>
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
            
            // act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() => format.Parse());
            Assert.Contains("non existent", exception.Message);
        }

        [Fact]
        public void GetWantedFormat_WrongWantedValues_ThrowsException()
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
                            "wrong",
                            "values"
                        }
                    }
                }
            };
            
            // act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() => format.Parse());
            Assert.Contains("wrong", exception.Message);
        }
        
        [Fact]
        public void GetWantedFormat_WrongWantedValuesFormat_ThrowsException()
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
                            1,
                            2
                        }
                    }
                }
            };
            
            // act and assert
            Assert.Throws<ArgumentOutOfRangeException>(() => format.Parse());
        }
        
        [Fact (Skip = "Not done")]
        // [Fact]
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
            // Assert.Equal(expected, result, new StringObjectDictionaryEqualityComparer());
        }

        [Fact]
        public void InsertTranslation_WrongEnd_ThrowException()
        {
            // arrange
            IEnumerable<IEnumerable<TranslationValueType>> groupings = new[]
            {
                new[] {TranslationValueType.Scope},
                new[] {TranslationValueType.Key, TranslationValueType.Variant},
            };
            IEnumerable<TranslationValueType> wantedValueTypes = new[] {TranslationValueType.Text};
            IEnumerable<Translation> translations = new[]
            {
                new Translation {KeyName = "k1", LocaleName = "l1", ScopeName = "s1", Variant = "v1", Text = "text"},
                new Translation {KeyName = "k1", LocaleName = "l1", ScopeName = "s1", Variant = "none", Text = "text"},
                new Translation {KeyName = "k1", LocaleName = "l2", ScopeName = "s1", Variant = "v1", Text = "text"},
                new Translation {KeyName = "k1", LocaleName = "l2", ScopeName = "s1", Variant = "none", Text = "text"},
            };
            Dictionary<string, object> dictionary = new();
            
            
            // act

            Assert.Throws<InvalidDataException>(() =>
            {
                foreach (Translation translation in translations)
                {
                    dictionary.InsertTranslation(translation, groupings, wantedValueTypes, ResultEnding.Default);
                }
            });
            // assert
        }
    }
}