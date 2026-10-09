using System.ComponentModel.DataAnnotations;

namespace Uc_10_kauane_FandomWorld.Models
{
    public class Fandom
    {
        [Key]
        public int FandomId { get; set; }

        public string? NomeFandom { get; set; }

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public virtual ICollection<Personagem>? Personagens { get; set; }

        public virtual ICollection<Livro>? Livros { get; set; }

        public virtual ICollection<Fanfic>? Fanfics { get; set; }

    }
}
