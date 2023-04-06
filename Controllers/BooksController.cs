using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private ApplicationDBContext context;
        private readonly IMapper mapper;

        public BooksController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/{id:int}", Name = "GetBookById")]
        public async Task<ActionResult<BookDTO>> Get(int id)
        {
            Book newBook = await context.Books
                .Include(bookDB => bookDB.Commentaries)
                .Include(bookDB => bookDB.AuthorsBooks)
                .ThenInclude(autorLibroDB => autorLibroDB.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<BookDTO>(newBook);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAll()
        {
            List<Book> books = await context.Books.ToListAsync();

            return mapper.Map<List<BookDTO>>(books);
        }

        [HttpPost]
        public async Task<ActionResult> Post(BookCreationDto bookCreationDTO)
        {
            var autoresExists = await context.Authors
                .Where(autorDB => bookCreationDTO.AuthorsIds.Contains(autorDB.Id))
                .Select(x => x.Id)
                .ToListAsync();

            if(autoresExists.Count!= bookCreationDTO.AuthorsIds.Count)
            {
                return BadRequest("No existe uno de los autores enviados.");
            }

            Book newBook = mapper.Map<Book>(bookCreationDTO);

            context.Add(newBook);

            await context.SaveChangesAsync();

            if (newBook.AuthorsBooks != null)
            {
                for (int i = 0; i < newBook.AuthorsBooks.Count; i++)
                {
                    var autorId = book.AuthorsBooks[i].AuthorId;
                    var autor = autores.FirstOrDefault(x => x.Id == autorId);
                    libro.AutoresLibros[i].Autor = autor;
                }
            }

            BookDTO createdBook = mapper.Map<BookDTO>(newBook);

            return CreatedAtRoute("GetBookById", new { Id = createdBook.Id }, createdBook);
        }

    }
}
