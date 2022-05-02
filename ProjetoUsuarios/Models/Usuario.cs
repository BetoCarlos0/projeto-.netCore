using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoUsuarios.Models
{
    public class Usuario
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
        public Endereco Endereco { get; set; }
    }
}
