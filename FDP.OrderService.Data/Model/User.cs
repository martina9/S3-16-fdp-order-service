using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FDP.OrderService.Data.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [Index(IsUnique = true)]
        public String Username { get; set; }

        [Required]
        [StringLength(128)]
        [EmailAddress]
        [Index(IsUnique = true)]
        public String Email { get; set; } 
    }
}
