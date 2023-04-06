using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<AuthorDTO> Authors { get; set; }
        public List<CommentaryDTO> Commentaries { get; set; }
    }
}
