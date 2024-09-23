using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVProject.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
