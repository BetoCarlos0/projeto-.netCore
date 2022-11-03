using System.ComponentModel.DataAnnotations;

namespace SistemaChamados.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "CPF vazio"), Display(Name = "CPF")]
        [StringLength(14, MinimumLength = 13, ErrorMessage = "CPF Inválido")]
        public string CpfNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha Vazia"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
