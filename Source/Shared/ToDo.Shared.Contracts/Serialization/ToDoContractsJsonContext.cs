using System.Text.Json.Serialization;
using ToDo.Shared.Contracts.V1.Requests;
using ToDo.Shared.Contracts.V1.Responses;

namespace ToDo.Shared.Contracts.Serialization;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(GetPaginatedToDosResponse))]
[JsonSerializable(typeof(CreateToDoItemRequest))]
public partial class ToDoContractsJsonContext : JsonSerializerContext
{
}