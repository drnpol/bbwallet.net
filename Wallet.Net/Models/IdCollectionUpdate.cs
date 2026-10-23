using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wallet.Net.Models;

/// <summary>A collection replacement (including an empty array), or an incremental add/remove operation.</summary>
[JsonConverter(typeof(IdCollectionUpdateConverter))]
public sealed record IdCollectionUpdate
{
    private IdCollectionUpdate() { }
    /// <summary>IDs replacing the existing collection; null for incremental updates.</summary>
    public IReadOnlyList<string>? Replacement { get; private init; }
    /// <summary>IDs to add during an incremental update.</summary>
    public IReadOnlyList<string>? Add { get; private init; }
    /// <summary>IDs to remove during an incremental update.</summary>
    public IReadOnlyList<string>? Remove { get; private init; }

    /// <summary>Replaces all IDs. An empty list clears the collection.</summary>
    /// <param name="ids">Replacement identifiers.</param>
    /// <returns>A replacement operation.</returns>
    public static IdCollectionUpdate Replace(IReadOnlyList<string> ids)
    {
        ArgumentNullException.ThrowIfNull(ids);
        return new() { Replacement = ids.ToArray() };
    }

    /// <summary>Adds and/or removes IDs without replacing the collection.</summary>
    /// <param name="add">IDs to add.</param>
    /// <param name="remove">IDs to remove.</param>
    /// <returns>An incremental operation.</returns>
    public static IdCollectionUpdate Modify(IReadOnlyList<string>? add = null, IReadOnlyList<string>? remove = null)
    {
        if (add is null && remove is null) throw new ArgumentException("Specify add or remove.");
        return new() { Add = add?.ToArray(), Remove = remove?.ToArray() };
    }
}

internal sealed class IdCollectionUpdateConverter : JsonConverter<IdCollectionUpdate>
{
    public override void WriteJson(JsonWriter writer, IdCollectionUpdate? value, JsonSerializer serializer)
    {
        if (value is null) { writer.WriteNull(); return; }
        if (value.Replacement is not null) { serializer.Serialize(writer, value.Replacement); return; }
        writer.WriteStartObject();
        if (value.Add is not null) { writer.WritePropertyName("add"); serializer.Serialize(writer, value.Add); }
        if (value.Remove is not null) { writer.WritePropertyName("remove"); serializer.Serialize(writer, value.Remove); }
        writer.WriteEndObject();
    }

    public override IdCollectionUpdate? ReadJson(JsonReader reader, Type objectType, IdCollectionUpdate? existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var token = JToken.Load(reader);
        if (token is JArray array) return IdCollectionUpdate.Replace(array.ToObject<string[]>(serializer)!);
        if (token is not JObject obj || obj.Properties().Any(p => p.Name is not ("add" or "remove")))
            throw new JsonSerializationException("Expected an ID array or an add/remove object.");
        return IdCollectionUpdate.Modify(obj["add"]?.ToObject<string[]>(serializer), obj["remove"]?.ToObject<string[]>(serializer));
    }
}
