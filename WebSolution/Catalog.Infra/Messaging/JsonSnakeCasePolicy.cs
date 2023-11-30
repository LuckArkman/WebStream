using System.Text.Json;
using Catalog.Infra.Messaging.Extensions;

namespace Catalog.Infra.Messaging;

public class JsonSnakeCasePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
        => name.ToSnakeCase();
}