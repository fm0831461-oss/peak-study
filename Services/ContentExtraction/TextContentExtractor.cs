using System.IO;
using System.Threading.Tasks;

namespace PeekStudy.API.Services.ContentExtraction
{
    public class TextContentExtractor : IStudyContentExtractor
    {
        public bool CanHandle(string extension)
        {
            return extension != null && extension.ToLowerInvariant() == ".txt";
        }

        public async Task<ExtractionResult> ExtractAsync(string filePath)
        {
            var result = new ExtractionResult();
            try
            {
                if (!File.Exists(filePath))
                {
                    result.Success = false;
                    result.ErrorCode = ExtractionErrorCode.Other;
                    result.ErrorMessage = "File not found.";
                    return result;
                }

                string text = await File.ReadAllTextAsync(filePath);
                if (string.IsNullOrWhiteSpace(text))
                {
                    result.Success = false;
                    result.ErrorCode = ExtractionErrorCode.NoTextFound;
                    result.ErrorMessage = "No text could be extracted from the file.";
                    return result;
                }

                result.Success = true;
                result.ExtractedText = text;
                result.PageCount = 1;
                result.PageTexts = new System.Collections.Generic.Dictionary<int, string>
                {
                    { 1, text }
                };
                result.ErrorCode = ExtractionErrorCode.None;
                return result;
            }
            catch (System.Exception ex)
            {
                result.Success = false;
                result.ErrorCode = ExtractionErrorCode.Other;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }
    }
}
