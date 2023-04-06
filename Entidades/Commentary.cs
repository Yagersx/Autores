using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Commentary
    {

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int bookId { get; set; }

        public Book Book { get; set; }


    }
}
