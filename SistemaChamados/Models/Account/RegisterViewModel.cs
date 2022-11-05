using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SistemaChamados.Models.Account
{
    public class RegisterViewModel
    {
        [NotMapped]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF vazio"), Display(Name = "CPF")]
        [StringLength(14, MinimumLength = 13, ErrorMessage = "CPF Inválido")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome vazio")]
        [RegularExpression(@"^[a-zA-Z, ]*$", ErrorMessage = "Apenas letras")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ser maior que 2 letras")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Vazio"), EmailAddress]
        //[Remote(action: "IsEmailUse", controller: "Account")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data de nascimento obrigatório"), Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Telefone vazio"), Display(Name = "Telefone")]
        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cargo vazio"), Display(Name = "Cargo")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "Departamento vazio"), Display(Name = "Departamento")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Departamento deve ser maior que 3 letras")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Supervisor vazio")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Supervisor deve ser maior que 3 letras")]
        public string Supervisor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ramal vazio")]
        [Range(1,100, ErrorMessage = "Ramal deve ser maior que 0 e menor que 100")]
        public int Ramal { get; set; }


        [Required(ErrorMessage = "Senha Vazia"), DataType(DataType.Password), Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha de Confirmação Vazia"), DataType(DataType.Password), Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "Senhas informadas diferentes")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
