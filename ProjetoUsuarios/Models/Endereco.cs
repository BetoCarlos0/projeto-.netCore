using System.ComponentModel.DataAnnotations;

namespace ProjetoUsuarios.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Endereço Completo")]
        public string EnredecoCompleto { get; set; }

        [Required]
        [Display(Name = "Número")]
        [Range(1, 99999, ErrorMessage = "Número Inválido")]
        public int Numero { get; set; }

        public Usuario Usuario { get; set; }
    }
}
