using Newtonsoft.Json;

namespace TVProject.Models
{
    public class Actor_Movie
    {
        public int MovieId { get; set; }
        [JsonIgnore]
        public Movie? Movie { get; set; }
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }
    }
}
