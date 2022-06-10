using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mercado.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nome Vazio"), Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Vazio"), EmailAddress]
        [Remote(action: "IsEmailUse", controller: "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha Vazia"), DataType(DataType.Password), Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Senha de Confirmação Vazia"), DataType(DataType.Password), Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "Senhas informadas diferentes")]
        public string ConfirmPassword { get; set; }
    }
}
