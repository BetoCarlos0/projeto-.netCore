using System.ComponentModel.DataAnnotations;

namespace SistemaChamados.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Cpf Vazio"), Display(Name = "CPF")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha Vazia"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
