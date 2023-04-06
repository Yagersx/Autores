using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/book/{BookId:int}/commentaries")]
    public class CommentaryController :ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public CommentaryController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}",Name = "GetCommentaryById")]
        public async Task<ActionResult<CommentaryDTO>> Get(int id)
        {
            var commentary = await context.Commentaries.FirstOrDefaultAsync(x => x.Id==id);

            if(commentary == null) { return NotFound(); }

            return mapper.Map<CommentaryDTO>(commentary);
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentaryDTO>>> GetCommentariesByBookId(int bookId)
        {
            List<Commentary> commentaries = await context.Commentaries.Where(
                commentary => commentary.bookId == bookId).ToListAsync();

            return mapper.Map<List<CommentaryDTO>>(commentaries);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int bookId, CommentaryCreationDTO commentaryCreationDTO)
        {
            bool bookExists = await context.Books.AnyAsync(book => book.Id == bookId);

            if (!bookExists)
                return BadRequest("El Book al cual quieres asociar este comentario, no existe.");

            Commentary newCommentary = mapper.Map<Commentary>(commentaryCreationDTO);

            newCommentary.bookId = bookId;

            context.Commentaries.Add(newCommentary);

            await context.SaveChangesAsync();

            CommentaryDTO createdCommentaryDTO = mapper.Map<CommentaryDTO>(newCommentary);

            return CreatedAtRoute("GetCommentaryById",  new { id = newCommentary.Id, bookId = bookId }, createdCommentaryDTO);
        }
    }
}
