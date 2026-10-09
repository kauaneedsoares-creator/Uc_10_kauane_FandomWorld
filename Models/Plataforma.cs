using System.ComponentModel.DataAnnotations;

namespace Uc_10_kauane_FandomWorld.Models
{
    public class Plataforma
    {
        [Key]

        public int PlataformaId { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<Fanfic>? Fanfics { get; set; }

    }
}
