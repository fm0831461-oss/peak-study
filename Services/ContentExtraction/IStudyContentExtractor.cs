using System.Threading.Tasks;

namespace PeekStudy.API.Services.ContentExtraction
{
    public interface IStudyContentExtractor
    {
        bool CanHandle(string extension);
        Task<ExtractionResult> ExtractAsync(string filePath);
    }
}
