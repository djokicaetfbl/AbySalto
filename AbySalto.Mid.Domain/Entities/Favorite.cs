using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Mid.Domain.Entities
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
