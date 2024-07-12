using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransferMarkt.Models
{
    public class PlayersVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public string Position { get; set; }
        public string currentClub { get; set; }
        public int contractYear { get; set; }
        public double Price { get; set; }
        public int Appearance { get; set; }
        public int CareerGoals { get; set; }
        public IFormFile ImagePath { get; set; }
        public bool IsSold { get; set; } = false;
        public bool IsFreeAgent { get; set; } = false;
        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }
    }
}
