using System.ComponentModel.DataAnnotations;

namespace NZWalks.Api.Models.DTO
{
    public class UpdateDtoRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be of 3 characters")]
        [MaxLength(3, ErrorMessage = "Higher than 3 is not acceptable")]
        public string Code { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Exceeding the Max. Value")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
