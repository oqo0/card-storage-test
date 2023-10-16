using FullSearchSamples;
using FullSearchSamples.Entity;
using FullSearchSamples.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddDbContext<DocDbContext>(options =>
    {
        options.UseMySql(
            "Server=127.0.0.1;Database=Documents;Uid=root;Pwd=Poi132poi_;",
            new MySqlServerVersion(new Version(8, 0, 34)));
    });

    services.AddTransient<IDocsRepository, DocsRepository>();
});

var host = builder.Build();

var docsRepo = host.Services.GetRequiredService<IDocsRepository>();

// docsRepo.LoadDocuments("sample-text.txt");

var searchResult = docsRepo.SimpleSearch("Moscow");

Console.WriteLine(searchResult.Content);

host.Run();