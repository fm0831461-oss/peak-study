using System.Collections.Generic;

namespace PeekStudy.API.Services.ContentExtraction
{
    public enum ExtractionErrorCode
    {
        None = 0,
        NoTextFound = 1,
        UnsupportedFormat = 2,
        Other = 99
    }

    public class ExtractionResult
    {
        public bool Success { get; set; }
        public string ExtractedText { get; set; } = string.Empty;
        public Dictionary<int, string>? PageTexts { get; set; }
        public int PageCount { get; set; }
        public ExtractionErrorCode ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
