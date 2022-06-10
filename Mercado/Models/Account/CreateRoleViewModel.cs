using System.ComponentModel.DataAnnotations;

namespace Mercado.Models.Account
{
    public class CreateRoleViewModel
    {
        [Required, Display(Name = "Função")]
        public string RoleName { get; set; }
    }
}
