// > nats sub bar.*
using Microsoft.Extensions.Logging;
using NATS.Client.Core;

var subject = "bar.xyz";
var options = NatsOpts.Default with { LoggerFactory = LoggerFactory.Create(builder => builder.AddConsole()) };

Print("[CON] Connecting...\n");

await using var connection = new NatsConnection(options);

for (var i = 0; i < 10; i++)
{
    Print($"[PUB] Publishing to subject ({i}) '{subject}'...\n");
    await connection.PublishAsync<Bar>(
        subject,
        new Bar { Id = i, Name = "Baz" },
        headers: new NatsHeaders { ["XFoo"] = $"bar{i}" });
}

void Print(string message)
{
    Console.Write($"{DateTime.Now:HH:mm:ss} {message}");
}

public record Bar
{
    public int Id { get; set; }

    public string? Name { get; set; }
}
