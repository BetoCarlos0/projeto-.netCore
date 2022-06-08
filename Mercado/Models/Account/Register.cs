using System.ComponentModel.DataAnnotations;

namespace Mercado.Models.Account
{
    public class Register
    {
        [Required]
        [Display(Name = "Nome de Usuário")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senhas informadas diferentes")]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmPassword { get; set; }
    }
}
