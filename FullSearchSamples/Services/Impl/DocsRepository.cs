using FullSearchSamples.Entity;

namespace FullSearchSamples.Services;

public class DocsRepository : IDocsRepository
{
    private readonly DocDbContext _docDbContext;

    public DocsRepository(DocDbContext docDbContext)
    {
        _docDbContext = docDbContext;
    }
    
    public void LoadDocuments(string fileName)
    {
        using var streamReader = new StreamReader(fileName);

        while (!streamReader.EndOfStream)
        {
            var currentLine = streamReader.ReadLine();
        
            if (string.IsNullOrEmpty(currentLine))
                continue;

            _docDbContext.Documents.Add(new Document
            {
                Content = currentLine
            });
        }
        
        _docDbContext.SaveChanges();
    }

    public Document? SimpleSearch(string searchString)
    {
        return _docDbContext.Documents
            .FirstOrDefault(document => document.Content.Contains(searchString));
    }
}