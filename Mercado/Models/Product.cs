using System.ComponentModel.DataAnnotations;

namespace Mercado.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do produto Vazio"), Display(Name = "Nome do Produto")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Quantidade Vazia"), Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Preço Vazio"), Display(Name = "Preço")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Forncedor Vazio")]
        public int ProviderId { get; set; }
        public Provider Fornecedor { get; set; }
    }
}
