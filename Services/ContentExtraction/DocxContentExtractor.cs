using System.IO;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PeekStudy.API.Services.ContentExtraction
{
    public class DocxContentExtractor : IStudyContentExtractor
    {
        public bool CanHandle(string extension)
        {
            return extension != null && extension.ToLowerInvariant() == ".docx";
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

                // Open the DOCX and extract text
                StringBuilder sb = new StringBuilder();
                using (var doc = WordprocessingDocument.Open(filePath, false))
                {
                    var body = doc.MainDocumentPart?.Document?.Body;
                    if (body == null)
                    {
                        result.Success = false;
                        result.ErrorCode = ExtractionErrorCode.NoTextFound;
                        result.ErrorMessage = "No document body found in DOCX file.";
                        return result;
                    }

                    foreach (var para in body.Elements<Paragraph>())
                    {
                        sb.AppendLine(para.InnerText);
                    }
                }

                string text = sb.ToString();
                if (string.IsNullOrWhiteSpace(text))
                {
                    result.Success = false;
                    result.ErrorCode = ExtractionErrorCode.NoTextFound;
                    result.ErrorMessage = "No text could be extracted from the DOCX file.";
                    return result;
                }

                result.Success = true;
                result.ExtractedText = text;
                // COPILOT CHANGE: DOCX page information is not reliable; do not invent page numbers.
                // Do not populate PageTexts or PageCount for DOCX since accurate page information is not available.
                result.PageCount = 0;
                result.PageTexts = null;
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
