using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SistemaChamados.Models;

namespace SistemaChamados.Data.Identity
{
    public class UserCustom : IdentityUser
    {
        [Required(ErrorMessage = "Nome vazio")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Apenas letras")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ser maior que 2 letras")]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF vazio")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "CPF deve ser maior que 2 letras")]
        [Column(TypeName = "varchar(30)")]
        public string CpfNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data de nascimento obrigatório"), Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Departamento vazio"), Display(Name = "Departamento")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Departamento deve ser maior que 3 letras")]
        [Column(TypeName = "varchar(40)")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Supervisor vazio")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Supervisor deve ser maior que 3 letras")]
        [Column(TypeName = "varchar(40)")]
        public string Supervisor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ramal vazio")]
        [Range(1, 100, ErrorMessage = "Ramal deve ser maior que 0 e menor que 100")]
        public int Ramal { get; set; }

        [Display(Name = "Chamados")]
        public IEnumerable<Calls> Calls { get; set; } = Enumerable.Empty<Calls>();
    }
}
