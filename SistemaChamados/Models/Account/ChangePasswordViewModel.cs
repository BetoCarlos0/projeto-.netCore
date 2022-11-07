using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaChamados.Models.Account
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("NewPassword", ErrorMessage =
            "A nova senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
