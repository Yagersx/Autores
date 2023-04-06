using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AuthorCreationDTO, Author>();
            CreateMap<Author, AuthorDTO>().ForMember(autorDTO => autorDTO.Books, options => options.MapFrom(MapAutorDTOLibros));

            //Book
            CreateMap<BookCreationDto, Book>().ForMember(book => book.AuthorsBooks, options => options.MapFrom(MapAutoresLibros));
            CreateMap<Book, BookCreationDto>();

            CreateMap<Book, BookDTO>().ForMember(libroDTO => libroDTO.Authors, options => options.MapFrom(MapLibroDTOAutores));
            CreateMap<BookDTO, Book>();

            //Commentaries
            CreateMap<CommentaryCreationDTO, Commentary>();
            CreateMap<Commentary, CommentaryDTO>();
        }

        private List<AuthorsBooks> MapAutoresLibros(BookCreationDto libroCreacionDTO, Book libro)
        {
            var resultado = new List<AuthorsBooks>();

            if (libroCreacionDTO.AuthorsIds == null) { return resultado; }

            foreach (var id in libroCreacionDTO.AuthorsIds)
            {
                resultado.Add(new AuthorsBooks { AuthorId = id });
            }

            return resultado;
        }

        private List<AuthorDTO> MapLibroDTOAutores(Book book, BookDTO bookDTO)
        {
            var result = new List<AuthorDTO>();

            if (book.AuthorsBooks == null) { return result; }

            foreach (var authorBook in book.AuthorsBooks)
            {
                result.Add(new AuthorDTO()
                {
                    Id = authorBook.AuthorId,
                    Name = authorBook.Author.Name
                });
            }
            
            return result;
        }

        private List<BookDTO> MapAutorDTOLibros(Author autor, AuthorDTO autorDTO)
        {
            var resultado = new List<BookDTO>();

            if (autor.AuthorsBooks == null) { return resultado; } 

            foreach (var autoresLibros in autor.AuthorsBooks)
            {
                resultado.Add(new BookDTO()
                {
                    Id = autoresLibros.BookId,
                    Title = autoresLibros.Book.Title
                });
            }

            return resultado;
        }
    }
}
