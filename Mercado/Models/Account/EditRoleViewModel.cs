using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mercado.Models.Account
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Função Vazia")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
