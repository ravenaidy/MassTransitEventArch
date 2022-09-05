using System;

namespace MassTransit.LoginService.Models
{
    public class Login
    {
        public Guid LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}