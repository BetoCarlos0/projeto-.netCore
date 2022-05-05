using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoUsuarios.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Apenas letras")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ser maior que 2 letras")]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome obrigatório")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Apenas letras")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Sobrenome deve ser maior que 3 letras")]
        [Column(TypeName = "varchar(100)")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Data de nascimento obrigatório")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Email obrigatório")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "Email deve ser maior que 14 caracteres")]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone obrigatório")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{5})?([0-9]{4})?[-. ]?([0-9]{4})$", ErrorMessage = "Número Inválido")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11)]
        [Column(TypeName = "varchar(30)")]
        [Phone]
        [Display(Name = "Telefone com DDD")]
        public string Telefone { get; set; }

        public Endereco Endereco { get; set; }       
    }
}
