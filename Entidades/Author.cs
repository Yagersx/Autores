using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Entidades
{
    public class Author
    {        
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener más de 120 carácteres.")]
        public string Name { get; set; }
        public List<AuthorsBooks> AuthorsBooks { get; set; }
    }
}