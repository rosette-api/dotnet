using Rosette.Api.Client.Models;
using System.Text.Json;

namespace Rosette.Api.Tests.Models;

/// <summary>
/// Tests for FieldedNameRecord class
/// </summary>
public class FieldedNameRecordTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_NoArgs_CreatesRecordWithEmptyText()
    {
        // Act
        var record = new FieldedNameRecord { Text = string.Empty };

        // Assert
        Assert.Equal(string.Empty, record.Text);
        Assert.Null(record.Language);
        Assert.Null(record.LanguageOfOrigin);
        Assert.Null(record.Script);
        Assert.Null(record.EntityType);
    }

    [Fact]
    public void Constructor_WithTextOnly_SetsTextProperty()
    {
        // Arrange
        const string text = "John Smith";

        // Act
        var record = new FieldedNameRecord { Text = text };

        // Assert
        Assert.Equal(text, record.Text);
        Assert.Null(record.Language);
        Assert.Null(record.LanguageOfOrigin);
        Assert.Null(record.Script);
        Assert.Null(record.EntityType);
    }

    [Fact]
    public void Constructor_WithAllParameters_SetsAllProperties()
    {
        // Arrange
        const string text = "John Smith";
        const string language = "eng";
        const string languageOfOrigin = "eng";
        const string script = "Latn";
        const string entityType = "PERSON";

        // Act
        var record = new FieldedNameRecord
        {
            Text = text,
            Language = language,
            LanguageOfOrigin = languageOfOrigin,
            Script = script,
            EntityType = entityType
        };

        // Assert
        Assert.Equal(text, record.Text);
        Assert.Equal(language, record.Language);
        Assert.Equal(languageOfOrigin, record.LanguageOfOrigin);
        Assert.Equal(script, record.Script);
        Assert.Equal(entityType, record.EntityType);
    }

    #endregion

    #region Property Tests

    [Fact]
    public void Properties_CanBeSet_Individually()
    {
        // Arrange
        var record = new FieldedNameRecord { Text = "Test" };

        // Act
        record.Language = "eng";
        record.LanguageOfOrigin = "spa";
        record.Script = "Latn";
        record.EntityType = "PERSON";

        // Assert
        Assert.Equal("eng", record.Language);
        Assert.Equal("spa", record.LanguageOfOrigin);
        Assert.Equal("Latn", record.Script);
        Assert.Equal("PERSON", record.EntityType);
    }

    [Fact]
    public void EntityType_AcceptsPersonType()
    {
        // Arrange & Act
        var record = new FieldedNameRecord { Text = "John Smith", EntityType = "PERSON" };

        // Assert
        Assert.Equal("PERSON", record.EntityType);
    }

    [Fact]
    public void EntityType_AcceptsLocationType()
    {
        // Arrange & Act
        var record = new FieldedNameRecord { Text = "New York", EntityType = "LOCATION" };

        // Assert
        Assert.Equal("LOCATION", record.EntityType);
    }

    [Fact]
    public void EntityType_AcceptsOrganizationType()
    {
        // Arrange & Act
        var record = new FieldedNameRecord { Text = "Microsoft", EntityType = "ORGANIZATION" };

        // Assert
        Assert.Equal("ORGANIZATION", record.EntityType);
    }

    [Fact]
    public void Script_AcceptsISO15924Codes()
    {
        // Arrange
        var scripts = new[] { "Latn", "Cyrl", "Arab", "Hans", "Hant", "Jpan", "Kore" };

        // Act & Assert
        foreach (var script in scripts)
        {
            var record = new FieldedNameRecord { Text = "Test", Script = script };
            Assert.Equal(script, record.Script);
        }
    }

    [Fact]
    public void Language_AcceptsISO6393Codes()
    {
        // Arrange
        var languages = new[] { "eng", "spa", "fra", "deu", "jpn", "cmn", "ara" };

        // Act & Assert
        foreach (var language in languages)
        {
            var record = new FieldedNameRecord { Text = "Test", Language = language };
            Assert.Equal(language, record.Language);
        }
    }

    #endregion

    #region Equals Tests

    [Fact]
    public void Equals_SameProperties_ReturnsTrue()
    {
        // Arrange
        var record1 = new FieldedNameRecord
        {
            Text = "John Smith",
            Language = "eng",
            LanguageOfOrigin = "eng",
            Script = "Latn",
            EntityType = "PERSON"
        };
        var record2 = new FieldedNameRecord
        {
            Text = "John Smith",
            Language = "eng",
            LanguageOfOrigin = "eng",
            Script = "Latn",
            EntityType = "PERSON"
        };

        // Act & Assert
        Assert.True(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentText_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedNameRecord { Text = "John Smith" };
        var record2 = new FieldedNameRecord { Text = "Jane Doe" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentLanguage_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedNameRecord { Text = "Test", Language = "eng" };
        var record2 = new FieldedNameRecord { Text = "Test", Language = "spa" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentLanguageOfOrigin_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedNameRecord { Text = "Test", LanguageOfOrigin = "eng" };
        var record2 = new FieldedNameRecord { Text = "Test", LanguageOfOrigin = "spa" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentScript_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedNameRecord { Text = "Test", Script = "Latn" };
        var record2 = new FieldedNameRecord { Text = "Test", Script = "Cyrl" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_DifferentEntityType_ReturnsFalse()
    {
        // Arrange
        var record1 = new FieldedNameRecord { Text = "Test", EntityType = "PERSON" };
        var record2 = new FieldedNameRecord { Text = "Test", EntityType = "LOCATION" };

        // Act & Assert
        Assert.False(record1.Equals(record2));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var record = new FieldedNameRecord { Text = "Test" };

        // Act & Assert
        Assert.False(record.Equals(null));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var record = new FieldedNameRecord { Text = "Test" };
        var differentType = "Test";

        // Act & Assert
        Assert.False(record.Equals(differentType));
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameProperties_ReturnsSameHashCode()
    {
        // Arrange
        var record1 = new FieldedNameRecord
        {
            Text = "John Smith",
            Language = "eng",
            LanguageOfOrigin = "eng",
            Script = "Latn",
            EntityType = "PERSON"
        };
        var record2 = new FieldedNameRecord
        {
            Text = "John Smith",
            Language = "eng",
            LanguageOfOrigin = "eng",
            Script = "Latn",
            EntityType = "PERSON"
        };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentProperties_ReturnsDifferentHashCode()
    {
        // Arrange
        var record1 = new FieldedNameRecord { Text = "John Smith" };
        var record2 = new FieldedNameRecord { Text = "Jane Doe" };

        // Act
        int hash1 = record1.GetHashCode();
        int hash2 = record2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsValidJson()
    {
        // Arrange
        var record = new FieldedNameRecord
        {
            Text = "John Smith",
            Language = "eng",
            LanguageOfOrigin = "eng",
            Script = "Latn",
            EntityType = "PERSON"
        };

        // Act
        string json = record.ToString();

        // Assert
        Assert.NotEmpty(json);
        Assert.Contains("\"text\"", json);
        Assert.Contains("John Smith", json);
    }

    [Fact]
    public void ToString_CanBeDeserialized()
    {
        // Arrange
        var original = new FieldedNameRecord
        {
            Text = "John Smith",
            Language = "eng",
            LanguageOfOrigin = "eng",
            Script = "Latn",
            EntityType = "PERSON"
        };

        // Act
        string json = original.ToString();
        var deserialized = JsonSerializer.Deserialize<FieldedNameRecord>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Text, deserialized.Text);
        Assert.Equal(original.Language, deserialized.Language);
        Assert.Equal(original.LanguageOfOrigin, deserialized.LanguageOfOrigin);
        Assert.Equal(original.Script, deserialized.Script);
        Assert.Equal(original.EntityType, deserialized.EntityType);
    }

    [Fact]
    public void ToString_NullOptionalFields_ProducesValidJson()
    {
        // Arrange
        var record = new FieldedNameRecord { Text = "Test" };

        // Act
        string json = record.ToString();
        var deserialized = JsonSerializer.Deserialize<FieldedNameRecord>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal("Test", deserialized.Text);
    }

    #endregion
}
