using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class BookCreationDto
    {

        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener más de 120 carácteres.")]
        public string Title { get; set; }

        public List<int> AuthorsIds { get; set; }
    }
}
