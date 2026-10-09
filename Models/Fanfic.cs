using System;
using System.ComponentModel.DataAnnotations;

namespace Uc_10_kauane_FandomWorld.Models
{
    public class Fanfic
    {
        [Key]

        public int FanficId { get; set; }

        [Required]

        public string Titulo { get; set; }

        public string? Sinopse { get; set; }

        [Required]

        public string Status { get; set; }

        public int AnoPublicacao { get; set; }

        public virtual Fandom? Fandom { get; set; }

        public int FandomId { get; set; }

        public virtual Plataforma? Plataforma { get; set; }

        public int PlataformaId { get; set; }

        public string? Link { get; set; }

    }
}
