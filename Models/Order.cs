using System.ComponentModel.DataAnnotations;

namespace TVProject.Models
{
    public class Order
    {
        [Key]
        
            public int Id { get; set; }
            public string? UserId { get; set; }
            public DateTime OrderDate { get; set; }
            public double TotalPrice { get; set; }
            public ICollection<OrderItem>? OrderItems { get; set; }

    }
}
