using System.ComponentModel.DataAnnotations;

namespace Uc_10_kauane_FandomWorld.Models
{
    public class Personagem
    {
        [Key]

        public int PersonagemId { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public virtual Fandom? Fandom { get; set; }

        public int FandomId { get; set; }

    }
}
