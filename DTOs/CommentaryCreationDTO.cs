using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class CommentaryCreationDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
