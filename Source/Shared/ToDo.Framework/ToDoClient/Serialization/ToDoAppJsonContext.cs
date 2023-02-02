using System.Text.Json.Serialization;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Framework.ToDoClient.Serialization;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(GetPaginatedToDosResponse))]
internal partial class ToDoAppJsonContext : JsonSerializerContext
{
}