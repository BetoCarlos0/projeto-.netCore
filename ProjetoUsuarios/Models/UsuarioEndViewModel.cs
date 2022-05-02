using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoUsuarios.Models
{
    public class UsuarioEndViewModel
    {
        public int UsuarioId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public int EnderecoId { get; set; }

        [Required]
        public EnderecoViewModel Endereco { get; set; }
    }
    public class EnderecoViewModel
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

        public UsuarioEndViewModel Usuario { get; set; }
    }
}
