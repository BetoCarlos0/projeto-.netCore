using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VCommerce.ProductApi.Models;

namespace VCommerce.ProductApi.DTOs;

public class ProductDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Nome é obrigatório")]
    [MaxLength(100), MinLength(3, ErrorMessage = "Nome deve ser maior que 3 caracteres")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Campo Descrição é obrigatório")]
    [MaxLength(200), MinLength(5, ErrorMessage = "Descrição deve ser maior que 3 caracteres")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Preço é obrigatório")]
    public decimal Price { get; set; }

    [Range(1, 9999) ,Required(ErrorMessage = "Estoque é obrigatório")]
    public long Stock { get; set; }
    public string? ImageURL { get; set; }
    public string? CategoryName { get; set; }

    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
}
