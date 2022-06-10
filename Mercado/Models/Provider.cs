using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mercado.Models
{
    public class Provider
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Telefone Vazia"), Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Razão Social Vazio"), Display(Name = "Razão Social")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Email Vazio"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Atividade Vazio"), Display(Name = "Atividade")]
        public string Activity { get; set; }

        public IEnumerable<Product> Produtos { get; set; }
    }
}
