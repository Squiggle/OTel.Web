# Observability demo

Using
- aspnet minimal APIs
- honeycomb.io

For each valid request, this minimal API will produce a trace and export it to Honeycomb.io.

This trace should include
- a span for the outbound HTTP request to wikipedia.org
- a custom span for the parsing of the JSON result

# Getting Started

1. Sign up for a free account with [Honeycomb.io](https://www.honeycomb.io/)
2. Get your API Key
3. Rename `appsettings-example.json` to `appsettings.json` and enter your API Key

# Running

```sh
dotnet run
curl https://localhost:5001/wikipedia/London
```

## Certificates

On OSX you may be prompted to authorise honeycomb. Do so.

ASPNet local webserver may also require you to configure your development HTTPS certiicates. You can provision these certificates via the dotnet cli:

```sh
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```
