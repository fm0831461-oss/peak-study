using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.OptionDTOs
{
    public class OptionDto
    {
        public int OptionId { get; set; }

        public string Text { get; set; } = string.Empty;
    }
}
