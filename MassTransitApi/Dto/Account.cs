using System.ComponentModel.DataAnnotations;
namespace MassTransitApi.Dto
{
    public class Account 
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int PostalCode { get; set; }
    }
}