using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mercado.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Vazio"), EmailAddress(ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha Vazia"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
