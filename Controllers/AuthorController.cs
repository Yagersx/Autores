using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public AuthorController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get()
        {
            List<Author> authors = await context.Authors.ToListAsync();

            return mapper.Map<List<AuthorDTO>>(authors);
        }

        [HttpGet("{id:int}", Name ="GetAuthorById")]
        public async Task<ActionResult<AuthorDTO>> Get(int id){

            Author author = await context.Authors
                .Include(author => author.AuthorsBooks)
                .ThenInclude(authorsBooks => authorsBooks.Book)
                .FirstOrDefaultAsync(author => author.Id == id);

            if (author==null)
            {
                return NotFound("No existe algun autor con el id dado.");
            }


            return mapper.Map<AuthorDTO>(author);
        }

        [HttpGet("name:string")]
        public async Task<ActionResult<List<AuthorDTO>>> GetByName(string name)
        {
            List<Author> authors = await context.Authors.Where(author => author.Name.Contains(name)).ToListAsync();

            return mapper.Map<List<AuthorDTO>>(authors);
        }

        [HttpPost]

        public async Task<ActionResult> Post(AuthorCreationDTO authorCreationDTO)
        {
            bool authorExists = await context.Authors.AnyAsync(author => author.Name == authorCreationDTO.Name);

            if (authorExists)
            {
                return BadRequest($"Ya existe un autor con el nombre {authorCreationDTO.Name}");
            }

            Author newAuthor = mapper.Map<Author>(authorCreationDTO);

            context.Add(newAuthor);

            await context.SaveChangesAsync();

            var createdAutor = mapper.Map<AuthorDTO>(newAuthor);

            return CreatedAtRoute("GetAuthorById", new { Id = createdAutor.Id }, createdAutor);
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult>Put (Author author, int id)
        {
            if (author.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la url");
            }

            context.Update(author);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            bool authorExists = await context.Authors.AnyAsync(autor => autor.Id == id);

            if (!authorExists)
            {
                return NotFound();
            }

            context.Remove(new Author() { Id = id });

            await context.SaveChangesAsync();

            return Ok();
        }
    }

    
}