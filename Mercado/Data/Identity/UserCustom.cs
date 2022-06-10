using Microsoft.AspNetCore.Identity;

namespace Mercado.Data.Identity
{
    public class UserCustom : IdentityUser
    {
        public string Name { get; set; }
    }
}
