using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace TransferMarkt.Models
{
    public class Negotiate
    {

        public int Id { get; set; }
        public string Ask { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public int? ListingId { get; set; }
        [ForeignKey("ListingId")]
        public Players? Listing { get; set; }
    }
}
