
using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string BloodGroup { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string EyeColor { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
