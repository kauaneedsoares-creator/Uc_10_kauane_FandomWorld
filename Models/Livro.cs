using System.ComponentModel.DataAnnotations;

namespace Uc_10_kauane_FandomWorld.Models
{
    public class Livro
    {

        [Key]

        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public string Autor {  get; set; }

        public int AnoPublicacao { get;  set; }

        public string Sinopse { get; set; }

        public virtual  Fandom? Fandom { get; set; }

        public int FandomId { get; set; }

    }
}
