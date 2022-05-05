using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoUsuarios.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "Bairro obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Bairro deve ter pelo menos 5 caracteres")]
        [Column(TypeName = "varchar(50)")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Número obrigatório")]
        [Display(Name = "Número")]
        [Range(1, 99999, ErrorMessage = "Número Inválido 1 - 99.999")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Cidade obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Cidade deve ter pelo menos 5 caracteres")]
        [Column(TypeName = "varchar(50)")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Estado deve ter pelo menos 5 caracteres")]
        [Column(TypeName = "varchar(50)")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Logradouro obrigatório")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "Bairro deve ter pelo menos 5 caracteres")]
        [Column(TypeName = "varchar(120)")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Cep obrigatório")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Bairro deve ter pelo menos 8 números")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{3})?[-. ]?([0-9]{3})$", ErrorMessage = "Número Inválido")]
        [Column(TypeName = "varchar(50)")]
        [DataType(DataType.PostalCode)]
        public string CEP { get; set; }

        public int? UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
