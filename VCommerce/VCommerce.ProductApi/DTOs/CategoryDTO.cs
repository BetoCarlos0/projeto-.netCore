using System.ComponentModel.DataAnnotations;
using VCommerce.ProductApi.Models;

namespace VCommerce.ProductApi.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Nome é obrigatório")]
    [MaxLength(100), MinLength(3, ErrorMessage = "Nome deve ser maior que 3 caracteres")]
    public string? Name { get; set; }
    public IEnumerable<Product>? Products { get; set; } = new List<Product>();
}
