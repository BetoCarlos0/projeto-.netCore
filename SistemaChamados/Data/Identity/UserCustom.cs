using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SistemaChamados.Models;

namespace SistemaChamados.Data.Identity
{
    public class UserCustom : IdentityUser
    {
        [Required(ErrorMessage = "Nome obrigatório")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Apenas letras")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ser maior que 2 letras")]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data de nascimento obrigatório"), Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Cargo obrigatório"), Display(Name = "Cargo")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "Departamento obrigatório"), Display(Name = "Departamento")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Departamento deve ser maior que 3 letras")]
        [Column(TypeName = "varchar(40)")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Supervisor obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Supervisor deve ser maior que 3 letras")]
        [Column(TypeName = "varchar(40)")]
        public string Supervisor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ramal obrigatório")]
        [Range(1, 100, ErrorMessage = "Ramal deve ser maior que 0 e menor que 100")]
        public int Ramal { get; set; }


        [Required(ErrorMessage = "Senha Vazia"), DataType(DataType.Password), Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha de Confirmação Vazia"), DataType(DataType.Password), Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "Senhas informadas diferentes")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Chamados")]
        public IEnumerable<Calls> Calls { get; set; } = Enumerable.Empty<Calls>();
    }
}
