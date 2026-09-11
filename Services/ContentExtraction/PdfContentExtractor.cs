using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace PeekStudy.API.Services.ContentExtraction
{
    public class PdfContentExtractor : IStudyContentExtractor
    {
        public bool CanHandle(string extension)
        {
            return extension != null && extension.ToLowerInvariant() == ".pdf";
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

                // PdfPig operations are synchronous; wrap in Task.Run
                return await Task.Run(() =>
                {
                    try
                    {
                        using (var document = PdfDocument.Open(filePath))
                        {
                            int pageCount = 0;
                            var pageTexts = new Dictionary<int, string>();
                            var sb = new StringBuilder();

                            foreach (var page in document.GetPages())
                            {
                                pageCount++;
                                string text = page.Text; // PdfPig page.Text gives the extracted text for the page
                                if (!string.IsNullOrWhiteSpace(text))
                                {
                                    pageTexts[pageCount] = text;
                                    sb.AppendLine(text);
                                }
                                else
                                {
                                    // empty page
                                    pageTexts[pageCount] = string.Empty;
                                }
                            }

                            if (pageCount == 0)
                            {
                                return new ExtractionResult
                                {
                                    Success = false,
                                    ErrorCode = ExtractionErrorCode.NoTextFound,
                                    ErrorMessage = "PDF has no extractable pages."
                                };
                            }

                            bool anyText = false;
                            foreach (var kv in pageTexts)
                            {
                                if (!string.IsNullOrWhiteSpace(kv.Value))
                                {
                                    anyText = true;
                                    break;
                                }
                            }

                            if (!anyText)
                            {
                                return new ExtractionResult
                                {
                                    Success = false,
                                    ErrorCode = ExtractionErrorCode.NoTextFound,
                                    ErrorMessage = "PDF appears to have no extractable text; OCR is required for scanned/image PDFs.",
                                    PageCount = pageCount,
                                    PageTexts = pageTexts
                                };
                            }

                            return new ExtractionResult
                            {
                                Success = true,
                                ExtractedText = sb.ToString(),
                                PageCount = pageCount,
                                PageTexts = pageTexts,
                                ErrorCode = ExtractionErrorCode.None
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        return new ExtractionResult
                        {
                            Success = false,
                            ErrorCode = ExtractionErrorCode.Other,
                            ErrorMessage = "PDF extraction failed: " + ex.Message
                        };
                    }
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorCode = ExtractionErrorCode.Other;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }
    }
}
