using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuth.WebApi.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class UserInfo
    {


        [Key]
   
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? DisplayName { get; set; }
        public string? UserName { get; set; }
        

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
