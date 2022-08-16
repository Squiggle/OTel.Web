using System.Text.Json.Nodes;
using Honeycomb.OpenTelemetry;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHoneycomb(builder.Configuration);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/wikipedia/{name}", async (string name) => await Lookup(name));

app.Run();

static async Task<string> Lookup(string name)
{
  using (HttpClient client = new HttpClient())
  {
    client.DefaultRequestHeaders.Add("User-Agent", "Learning-Demo-Client");
    var response = await client.GetStringAsync($"https://en.wikipedia.org/api/rest_v1/page/summary/{name}?redirect=false");

    using var span = TracerProvider.Default.GetTracer("my-app").StartActiveSpan("parse-response");
    System.Diagnostics.Trace.WriteLine("Testing System.Diagnostics.Trace");
    span.SetAttribute("wikipedialookup.name", name);
    return JsonNode.Parse(response)?["extract"]?.GetValue<string>() ?? "";
  }
}