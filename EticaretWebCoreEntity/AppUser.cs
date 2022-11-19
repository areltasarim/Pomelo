using Microsoft.AspNetCore.Identity;

namespace EticaretWebCoreEntity
{
    public class AppUser : IdentityUser<int>
    {
        public string AdSoyad { get; set; }
        
    }
}