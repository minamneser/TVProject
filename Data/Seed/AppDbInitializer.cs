using TVProject.Data.DataBase;
using TVProject.Data.Enums;
using TVProject.Models;

namespace TVProject.Data.Seed
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema1",
                            Logo = "https://dotnethow.net/images/cinemas/cinema-4.jpeg",
                            Description= "Description of 2st Cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema2",
                            Logo = "https://dotnethow.net/images/cinemas/cinema-5.jpeg",
                            Description= "Description of 2st Cinema"
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Producer1",
                            ProfilePicUrl = "https://dotnethow.net/images/Actors/Actor-5.jpeg",
                            Bio= "bio of 1st Producer",


                        },
                        new Producer()
                        {
                            FullName = "Producer2",
                            ProfilePicUrl = "https://dotnethow.net/images/actors/actor-5.jpeg",
                            Bio= "bio of 2nd Producer",
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Comedy",
                            ImageUrl = "https://dotnethow.net/images/movies/movie-4.jpeg",
                            Description= "Description of 2st Cinema",
                            Price = 120,
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Comedy,

                        },
                        new Movie()
                        {
                            Name = "Action",
                            ImageUrl = "https://dotnethow.net/images/movies/movie-5.jpeg",
                            Description= "Description of 2nd Cinema",
                            Price = 140,
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action,
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor1",
                            ProfilePicUrl = "https://dotnethow.net/images/Actors/Actor-4.jpeg",
                            Bio= "bio of 1st Actor",


                        },
                        new Actor()
                        {
                            FullName = "Actor2",
                            ProfilePicUrl = "https://dotnethow.net/images/actors/actor-5.jpeg",
                            Bio= "bio of 2nd Actor",
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 5,
                        },
                        new Actor_Movie()
                        {
                            ActorId= 2,
                            MovieId= 6,
                        }
                    });
                    context.SaveChanges();
                }

            }
        }
    }
}
