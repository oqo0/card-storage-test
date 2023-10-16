using FullSearchSamples.Entity;

namespace FullSearchSamples.Services;

internal interface IDocsRepository
{
    public void LoadDocuments(string fileName);
    public Document? SimpleSearch(string searchString);
}