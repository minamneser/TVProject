﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TVProject.Data.Enums;

namespace TVProject.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        [JsonIgnore]
        public List<Actor_Movie>? Actor_Movies { get; set; }
        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        [JsonIgnore]

        public Cinema? Cinema { get; set; }
        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
        [JsonIgnore]

        public Producer? Producer { get; set; }

        public List<CartItem>? CartItems { get; set; }
        public List<OrderItem>? OrderItems { get; set; }


    }
}
