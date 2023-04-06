using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener más de 120 carácteres.")]
        public string Title { get; set; }
        public List<Commentary> Commentaries { get; set; }
        public List<AuthorsBooks> AuthorsBooks { get; set; }
    }
}
