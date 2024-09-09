using System.ComponentModel.DataAnnotations;

namespace TVProject.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string? ProfilePicUrl { get; set; }
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public List<Actor_Movie>? Actor_Movies { get; set; }
    }
}
