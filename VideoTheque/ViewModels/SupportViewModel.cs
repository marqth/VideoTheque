using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class SupportViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nom")]
        [Required]
        public string nom { get; set; }

        public SupportDto ToDto()
        {
            return new SupportDto
            {
                Id = this.Id,
                nom = this.nom
            };
        }

        public static SupportViewModel ToModel(SupportDto dto)
        {
            return new SupportViewModel
            {
                Id = dto.Id,
                nom = dto.nom
            };
        }
    }
}