
using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Domain.Entities
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
