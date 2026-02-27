using System.Text;
using System.Text.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var orderFile = Path.Combine(builder.Environment.ContentRootPath, "ocelot.order-service.json");
var deliveryFile = Path.Combine(builder.Environment.ContentRootPath, "ocelot.delivery-service.json");

var mergedOcelotJson = MergeOcelotConfigs(orderFile, deliveryFile);
builder.Configuration.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(mergedOcelotJson)));

builder.Services.AddOcelot();

var app = builder.Build();
await app.UseOcelot();
app.Run();

static string MergeOcelotConfigs(params string[] filePaths)
{
    var allRoutes = new List<JsonElement>();
    JsonElement? globalConfig = null;

    foreach (var filePath in filePaths)
    {
        using var doc = JsonDocument.Parse(File.ReadAllText(filePath));

        if (doc.RootElement.TryGetProperty("Routes", out var routes) && routes.ValueKind == JsonValueKind.Array)
        {
            foreach (var r in routes.EnumerateArray())
                allRoutes.Add(r.Clone());
        }

        if (globalConfig is null &&
            doc.RootElement.TryGetProperty("GlobalConfiguration", out var gc) &&
            gc.ValueKind == JsonValueKind.Object)
        {
            globalConfig = gc.Clone();
        }
    }

    using var output = new MemoryStream();
    using (var writer = new Utf8JsonWriter(output, new JsonWriterOptions { Indented = true }))
    {
        writer.WriteStartObject();

        writer.WritePropertyName("Routes");
        writer.WriteStartArray();
        foreach (var r in allRoutes) r.WriteTo(writer);
        writer.WriteEndArray();

        writer.WritePropertyName("GlobalConfiguration");
        if (globalConfig is null) writer.WriteStartObject();
        if (globalConfig is null) writer.WriteEndObject();
        else globalConfig.Value.WriteTo(writer);

        writer.WriteEndObject();
    }

    return Encoding.UTF8.GetString(output.ToArray());
}