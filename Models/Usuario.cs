using System.ComponentModel.DataAnnotations;

namespace Uc_10_kauane_FandomWorld.Models
{
    public class Usuario
    {
        [Key]


        public int UsuarioId { get; set; }

        public string NomeUsuario { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

    }
}
