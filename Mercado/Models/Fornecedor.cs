using System.Collections.Generic;

namespace Mercado.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Activity { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}
